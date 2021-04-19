using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TB_Save_Editor
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
            Application.Run(new Form1());
            //Application.Run(new RouteGraph());

            /*string s = "";

            for(int i = 0; i < 250000; i++)
            {
                s += "AAAAA";
                if (i % 10000 == 0)
                {

                System.Console.WriteLine(i);
                }
            }
            System.Console.WriteLine("Finish....");*/
        }
    }
}
