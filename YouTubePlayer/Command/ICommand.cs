using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubePlayer.Command
{
    public interface ICommand
    {
        Int32 Id { get; }
        Keys Key { get; }
        Modifier Modifier { get; }        
        String NotifyText { get; }

        void Execute(MainForm form);
    }
}
