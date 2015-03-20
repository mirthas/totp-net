using System;
using System.Windows.Forms;

namespace TOTPGui
{
    internal static class TOTPGui
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}