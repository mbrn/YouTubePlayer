using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YouTubePlayer.Command;
using YouTubePlayer.Util;

namespace YouTubePlayer
{
    public partial class MainForm : Form
    {
        public ChromiumWebBrowser ChromeBrowser { get; private set; }
        private KeyHandler _KeyHandler;

        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            _KeyHandler = new KeyHandler(this.Handle);

            CommandFactory.RegisterAllCommands(_KeyHandler);

            foreach (var key in YouTubeShortcut.AllKeys)
            {
                _KeyHandler.Register(key.Key, key.Value.Item1, key.Value.Item2);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CefSettings settings = new CefSettings();
            //settings.BrowserSubprocessPath = @"x86\CefSharp.BrowserSubprocess.exe";
            settings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\CEF";
            settings.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:68.0) Gecko/20100101 Firefox/68.0";
            settings.PersistSessionCookies = true;
            Cef.Initialize(settings);
            //Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);

            ChromeBrowser = new ChromiumWebBrowser(ConfigurationManager.AppSettings["defaultUrl"] ?? "https://www.youtube.com/");
            ChromeBrowser.TitleChanged += ((s2, e2) =>
            {
                var title = e2.Title;
                if (title.Length > 63)
                {
                    title = title.Substring(0, 63);
                }

                this.Text = title;
                notifyIcon1.Text = title;
            });
            ChromeBrowser.LoadError += ((s3, e3) => 
            {
                var x = e3.ErrorText;
            });

            panel1.Controls.Add(ChromeBrowser);
            ChromeBrowser.Dock = DockStyle.Fill;
        }

        private void HandleHotkey(int key)
        {
            try
            {
                
                if (backgroundWorker1.IsBusy == false)
                {
                    TaskbarProgress.SetState(this.Handle, TaskbarProgress.TaskbarStates.Paused);
                    backgroundWorker1.RunWorkerAsync();
                }

                var text = CommandFactory.RunKey(this, key);

                if (ConfigurationManager.AppSettings["showNotifications"] == "true")
                {
                    if (String.IsNullOrEmpty(text) == false)
                    {
                        notifyIcon1.ShowBalloonTip(3, "", text, ToolTipIcon.Info);
                    }
                }
            }
            catch (Exception ex)
            {
                notifyIcon1.ShowBalloonTip(10, "Error", ex.ToString(), ToolTipIcon.Error);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == KeyHandler.Constants.WM_HOTKEY_MSG_ID)
                HandleHotkey(m.WParam.ToInt32());
            base.WndProc(ref m);
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void toolStripOpen_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void toolStripExit_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(350);
            TaskbarProgress.SetState(this.Handle, TaskbarProgress.TaskbarStates.NoProgress);
        }
    }
}
