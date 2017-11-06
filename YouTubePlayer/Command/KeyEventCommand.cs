using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using CefSharp.WinForms;
using CefSharp;
using System.Configuration;
using YouTubePlayer.Util;

namespace YouTubePlayer.Command
{
    public class KeyEventCommand : ICommand
    {
        public Int32 Id { get; protected set; }
        public Keys Key { get; protected set; }
        public Modifier Modifier { get; protected set; }
        public KeyEvent YouTubeKeyEvent { get; protected set; }
        public String NotifyText { get; protected set; }

        protected KeyEventCommand() { }

        public static KeyEventCommand New(Keys key, KeyEvent youTubeKeyEvent, String notifyText = "")
        {
            return New(key, Modifier.Control | Modifier.Shift, youTubeKeyEvent, notifyText);
        }

        public static KeyEventCommand New(Keys key, Modifier modifier, KeyEvent youTubeKeyEvent, String notifyText = "")
        {
            return new KeyEventCommand()
            {
                Id = (int)key * (int)modifier,
                Key = key,
                Modifier = modifier,
                YouTubeKeyEvent = youTubeKeyEvent,
                NotifyText = notifyText
            };
        }

        public void Execute(MainForm form)
        {
            form.ChromeBrowser.GetBrowser().GetHost().SendKeyEvent(YouTubeKeyEvent);
        }
    }

    public enum Modifier
    {
        Alt = 0x0001,
        Control = 0x0002,
        Shift = 0x0004,
        Win = 0x0008
    }
}
