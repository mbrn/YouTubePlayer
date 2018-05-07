using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YouTubePlayer.Util;

namespace YouTubePlayer.Command
{
    public class ActionCommand : ICommand
    {
        public Int32 Id { get; private set; }
        public Keys Key { get; private set; }
        public Modifier Modifier { get; private set; }
        public Action<MainForm> Action { get; private set; }
        public String NotifyText { get; private set; }

        public static ActionCommand New(Keys key, Action<MainForm> action, String notifyText = "")
        {
            return New(key, Modifier.Control | Modifier.Shift, action, notifyText);
        }

        public static ActionCommand New(Keys key, Modifier modifier, Action<MainForm> action, String notifyText = "")
        {
            return new ActionCommand()
            {
                Id = (int)key * ((int)modifier + 1),
                Key = key,
                Modifier = modifier,
                Action = action,
                NotifyText = notifyText
            };
        }        

        public void Execute(MainForm form)
        {
            this.Action.Invoke(form);
        }
    }
}
