using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubePlayer.Command
{
    public class FocusedKeyEventCommand : ICommand
    {
        public Int32 Id { get; protected set; }
        public Keys Key { get; protected set; }
        public Modifier Modifier { get; protected set; }
        public KeyEvent YouTubeKeyEvent { get; protected set; }
        public String NotifyText { get; protected set; }

        public static FocusedKeyEventCommand New(Keys key, KeyEvent youTubeKeyEvent, String notifyText = "")
        {
            return New(key, Modifier.Control | Modifier.Shift, youTubeKeyEvent, notifyText);
        }

        public static FocusedKeyEventCommand New(Keys key, Modifier modifier, KeyEvent youTubeKeyEvent, String notifyText = "")
        {
            return new FocusedKeyEventCommand()
            {
                Id = (int)key * ((int)modifier + 1),
                Key = key,
                Modifier = modifier,
                YouTubeKeyEvent = youTubeKeyEvent,
                NotifyText = notifyText
            };
        }

        public void Execute(MainForm form)
        {
            var script = @"(function() {
                    document.getElementsByClassName('html5-video-player')[0].focus();
                })();";

            form.ChromeBrowser.EvaluateScriptAsync(script).ContinueWith(x =>
            {
                var response = x.Result;
                if (x.Result.Success)
                {
                    form.ChromeBrowser.GetBrowser().GetHost().SendKeyEvent(this.YouTubeKeyEvent);
                }
            });
        }
    }
}
