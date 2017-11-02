using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubePlayer.Util
{
    public class KeyHandler
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private IntPtr _HWnd;

        public KeyHandler(IntPtr hWnd)
        {
            this._HWnd = hWnd;

        }

        public void Register(int id, Keys key, int modifier)
        {
            RegisterHotKey(this._HWnd, id, modifier, (int)key);
        }

        public bool Unregiser(int id)
        {
            return UnregisterHotKey(this._HWnd, id);
        }

        public static class Constants
        {
            //windows message id for hotkey
            public const int WM_HOTKEY_MSG_ID = 0x0312;

            public const int MOD_ALT = 0x0001;
            public const int MOD_CONTROL = 0x0002;
            public const int MOD_SHIFT = 0x0004;
            public const int MOD_WIN = 0x0008;
        }
    }
}
