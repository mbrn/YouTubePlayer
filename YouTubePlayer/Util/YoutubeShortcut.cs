using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubePlayer.Util
{
    public static class YouTubeShortcut
    {
        public static Dictionary<int, Tuple<Keys, int, KeyEvent, String>> AllKeys = new Dictionary<int, Tuple<Keys, int, KeyEvent, String>>();

        static YouTubeShortcut()
        {
            AllKeys.Add(1, Tuple.Create(Keys.N, KeyHandler.Constants.MOD_CONTROL | KeyHandler.Constants.MOD_SHIFT, Next, "Next"));
            AllKeys.Add(2, Tuple.Create(Keys.P, KeyHandler.Constants.MOD_CONTROL | KeyHandler.Constants.MOD_SHIFT, Prev, "Prev"));
            AllKeys.Add(3, Tuple.Create(Keys.K, KeyHandler.Constants.MOD_CONTROL | KeyHandler.Constants.MOD_SHIFT, Pause, "Play/Pause"));
            AllKeys.Add(4, Tuple.Create(Keys.L, KeyHandler.Constants.MOD_CONTROL | KeyHandler.Constants.MOD_SHIFT, Forw10, ""));
            AllKeys.Add(5, Tuple.Create(Keys.J, KeyHandler.Constants.MOD_CONTROL | KeyHandler.Constants.MOD_SHIFT, Back10, ""));
            AllKeys.Add(6, Tuple.Create(Keys.M, KeyHandler.Constants.MOD_CONTROL | KeyHandler.Constants.MOD_SHIFT, Mute, "Mute/Unmute"));
        }

        public static KeyEvent Next = GenerateKey(KeyEventType.KeyDown, Keys.N, CefEventFlags.ShiftDown);
        public static KeyEvent Prev = GenerateKey(KeyEventType.KeyDown, Keys.P, CefEventFlags.ShiftDown);
        public static KeyEvent Pause = GenerateKey(KeyEventType.KeyDown, Keys.K, CefEventFlags.None);
        public static KeyEvent Forw10 = GenerateKey(KeyEventType.KeyDown, Keys.L, CefEventFlags.None);
        public static KeyEvent Back10 = GenerateKey(KeyEventType.KeyDown, Keys.J, CefEventFlags.None);
        public static KeyEvent Mute = GenerateKey(KeyEventType.KeyDown, Keys.M, CefEventFlags.None);

        private static KeyEvent GenerateKey(KeyEventType type, Keys key, CefEventFlags flag)
        {
            return new KeyEvent()
            {
                Type = type,
                WindowsKeyCode = (int)key,
                Modifiers = flag
            };
        }
    }
}
