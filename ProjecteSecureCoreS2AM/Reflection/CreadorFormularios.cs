using System;
using System.Reflection;
using System.Windows.Forms;

namespace ProjecteSecureCoreS2AM
{
    /// <summary>
    /// Proporciona creació de formularis WinForms per reflexió, desacoblant el codi que
    /// llança pantalles del seu tipus concret.
    /// </summary>
    public static class CreadorFormularios
    {
        /// <summary>
        /// Crea una instància del formulari indicat passant-li els arguments al constructor.
        /// </summary>
        /// <param name="nombreClase">Nom complet de la classe (namespace.NomClasse) dins l'executable.</param>
        /// <param name="args">Arguments que es passen al constructor del formulari.</param>
        /// <returns>Instància del formulari creada per reflexió.</returns>
        /// <exception cref="System.NullReferenceException">Si la classe no existeix a l'ensamblat.</exception>
        public static Form Crear(string nombreClase, params object[] args)
        {
            Assembly ensamblado = Assembly.GetExecutingAssembly();
            Type tipo = ensamblado.GetType(nombreClase);
            return (Form)Activator.CreateInstance(tipo, args);
        }
    }
}
