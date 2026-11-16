using System;
using System.Reflection;
using System.Windows.Forms;

namespace ProjecteSecureCoreS2AM
{
    public static class CreadorFormularios
    {
        public static Form Crear(string nombreClase, params object[] args)
        {
            Assembly ensamblado = Assembly.GetExecutingAssembly();
            Type tipo = ensamblado.GetType(nombreClase);
            return (Form)Activator.CreateInstance(tipo, args);
        }
    }
}
