using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new ChatForm());
            }
            catch (Exception ex)
            {
                if (ex is ObjectDisposedException)
                    return;
                if (ex is NullReferenceException)
                    return;
                throw ex;
            }
        }
    }
}
