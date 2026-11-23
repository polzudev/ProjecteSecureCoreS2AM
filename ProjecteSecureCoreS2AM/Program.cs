using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjecteSecureCoreS2AM
{
    /// <summary>
    /// Classe d'arrencada de l'aplicació Secure Core.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Punt d'entrada principal de l'aplicació. Inicia la pantalla de presentació
        /// (<see cref="frmSplashScreen"/>) que gestiona tot el flux d'autenticació.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(CreadorFormularios.Crear("ProjecteSecureCoreS2AM.frmSplashScreen"));
        }
    }
}
