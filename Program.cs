using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpNetApp1
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1()); 
            /*******************************************************************/
            // 子窗口 exampleWindow 运行在一个子线程中
            // 主线程 Application 把持住，在持续运行
            Application.Run(ExampleWindow.createWindow());
            /*******************************************************************/
        }
    }
}
