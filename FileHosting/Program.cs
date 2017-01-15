using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileHostingModel;
using System.Data.Entity;

namespace FileHosting
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
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<FileHostingModelContainer>());

            MainForm mf = new MainForm();
            LogonForm lf = new LogonForm();
            lf.Owner = mf;

            //mf.IsMdiContainer = true;
            //lf.MdiParent = mf;
            lf.ShowDialog();

            
            Application.Run(mf);
            
            //Application.Run(new LogonForm());
        }
    }
}
