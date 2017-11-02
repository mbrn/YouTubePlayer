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
using YouTubePlayer.Util;

namespace YouTubePlayer
{
    public partial class MainForm : Form
    {
        private ChromiumWebBrowser _ChromeBrowser;
        private KeyHandler _KeyHandler;

        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            _KeyHandler = new KeyHandler(this.Handle);

            foreach (var key in YouTubeShortcut.AllKeys)
            {
                _KeyHandler.Register(key.Key, key.Value.Item1, key.Value.Item2);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CefSettings settings = new CefSettings();
            settings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\CEF";
            settings.PersistSessionCookies = true;
            Cef.Initialize(settings);

            _ChromeBrowser = new ChromiumWebBrowser(ConfigurationManager.AppSettings["defaultUrl"] ?? "https://www.youtube.com/");
            _ChromeBrowser.TitleChanged += ((s2, e2) =>
            {
                var title = e2.Title;
                if (title.Length > 63)
                {
                    title = title.Substring(0, 63);
                }

                this.Text = title;
                notifyIcon1.Text = title;
            });

            panel1.Controls.Add(_ChromeBrowser);
            _ChromeBrowser.Dock = DockStyle.Fill;
        }

        private void HandleHotkey(int key)
        {
            try
            {
                if (YouTubeShortcut.AllKeys.ContainsKey(key))
                {
                    if (backgroundWorker1.IsBusy == false)
                    {
                        TaskbarProgress.SetState(this.Handle, TaskbarProgress.TaskbarStates.Paused);
                        backgroundWorker1.RunWorkerAsync();
                    }

                    var command = YouTubeShortcut.AllKeys[key];
                    _ChromeBrowser.GetBrowser().GetHost().SendKeyEvent(command.Item3);
                    if (ConfigurationManager.AppSettings["showNotifications"] == "true")
                    {
                        if (String.IsNullOrEmpty(command.Item4) == false)
                        {
                            notifyIcon1.ShowBalloonTip(3, "", command.Item4, ToolTipIcon.Info);
                        }
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
