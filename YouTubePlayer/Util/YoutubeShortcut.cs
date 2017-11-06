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

        public static KeyEvent Next = GenerateKey(KeyEventType.KeyDown, Keys.N, CefEventFlags.ShiftDown);
        public static KeyEvent Prev = GenerateKey(KeyEventType.KeyDown, Keys.P, CefEventFlags.ShiftDown);
        public static KeyEvent Pause = GenerateKey(KeyEventType.KeyDown, Keys.K, CefEventFlags.None);
        public static KeyEvent Forw10 = GenerateKey(KeyEventType.KeyDown, Keys.L, CefEventFlags.None);
        public static KeyEvent Back10 = GenerateKey(KeyEventType.KeyDown, Keys.J, CefEventFlags.None);
        public static KeyEvent Mute = GenerateKey(KeyEventType.KeyDown, Keys.M, CefEventFlags.None);
        public static KeyEvent Up = GenerateKey(KeyEventType.KeyDown, Keys.Up, CefEventFlags.None);
        public static KeyEvent Down = GenerateKey(KeyEventType.KeyDown, Keys.Down, CefEventFlags.None);
        public static KeyEvent Left = GenerateKey(KeyEventType.KeyDown, Keys.Left, CefEventFlags.None);
        public static KeyEvent Right = GenerateKey(KeyEventType.KeyDown, Keys.Right, CefEventFlags.None);

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
