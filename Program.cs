using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseProProject.BLL;
using WarehouseProProject.DAL.Repository;


namespace WarehouseManagement
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var context = new WarehouseContext();
            var userRepository = new UserRepository(context);

            var userBLL = new UserBLL(userRepository);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Users(userBLL));
        }
    }
}
