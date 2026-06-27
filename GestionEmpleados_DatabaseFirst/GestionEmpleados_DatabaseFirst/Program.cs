using System;
using System.Windows.Forms;

namespace GestionEmpleados_DatabaseFirst
{
    internal static class Program
    {
        //Punto de inicio de la aplicación.
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmEmpleados());
        }
    }
}
