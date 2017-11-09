using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YouTubePlayer.Util;

namespace YouTubePlayer.Command
{
    public static class CommandFactory
    {
        private static Dictionary<Int32, ICommand> _AllCommands = new Dictionary<int, ICommand>();

        public static void RegisterAllCommands(KeyHandler keyHandler)
        {
            KeyEventCommand.New(Keys.N, YouTubeShortcut.Next, "Next").AddToCommands(keyHandler);
            KeyEventCommand.New(Keys.P, YouTubeShortcut.Prev, "Prev").AddToCommands(keyHandler);
            KeyEventCommand.New(Keys.K, YouTubeShortcut.Pause, "Play / Pause").AddToCommands(keyHandler);
            KeyEventCommand.New(Keys.L, YouTubeShortcut.Forw10, "").AddToCommands(keyHandler);
            KeyEventCommand.New(Keys.J, YouTubeShortcut.Back10, "").AddToCommands(keyHandler);
            KeyEventCommand.New(Keys.M, YouTubeShortcut.Mute, "Mute/Unmute").AddToCommands(keyHandler);

            FocusedKeyEventCommand.New(Keys.Up, Modifier.Alt, YouTubeShortcut.Up).AddToCommands(keyHandler);
            FocusedKeyEventCommand.New(Keys.Down, Modifier.Alt, YouTubeShortcut.Down).AddToCommands(keyHandler);
            FocusedKeyEventCommand.New(Keys.Left, Modifier.Alt, YouTubeShortcut.Left).AddToCommands(keyHandler);
            FocusedKeyEventCommand.New(Keys.Right, Modifier.Alt, YouTubeShortcut.Right).AddToCommands(keyHandler);

            ActionCommand.New(Keys.Q, (MainForm form) =>
            {
                Process.GetCurrentProcess().Kill();
            }).AddToCommands(keyHandler);

            ActionCommand.New(Keys.S, (MainForm form) =>
            {
                var script = @"(function() {
                    document.getElementsByClassName('videoAdUiSkipButton')[0].click();
                })();";

                form.ChromeBrowser.EvaluateScriptAsync(script).ContinueWith(x =>
                {
                    var response = x.Result;
                });
            }).AddToCommands(keyHandler);

            ActionCommand.New(Keys.D, (MainForm form) =>
            {
                form.ChromeBrowser.ShowDevTools();
            }).AddToCommands(keyHandler);
        }

        private static void AddToCommands(this ICommand command, KeyHandler keyHandler)
        {
            keyHandler.Register(command.Id, command.Key, (int)command.Modifier);
            _AllCommands.Add(command.Id, command);
        }
        
        public static String RunKey(MainForm form , int id)
        {
            if (_AllCommands.ContainsKey(id))
            {
                var command = _AllCommands[id];
                command.Execute(form);
                return command.NotifyText;
            }

            return "";
        }
    }
}
