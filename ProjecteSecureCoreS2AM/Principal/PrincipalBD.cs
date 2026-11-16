using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProjecteSecureCoreS2AM
{
    public class PrincipalBD
    {
        private string CadenaConexion
        {
            get
            {
                ConnectionStringSettings conf = ConfigurationManager.ConnectionStrings["SecureCore"];
                if (conf == null)
                {
                    throw new ConfigurationErrorsException("Falta la cadena de connexió 'SecureCore' a App.config");
                }
                return conf.ConnectionString;
            }
        }

        // Datos del usuario con su categoría (nivel de acceso) y su rango
        public DataRow ObtenerPerfil(int idUser)
        {
            string query = "select u.idUser, u.CodeUser, u.UserName, u.Login, c.CodeCategory, c.DescCategory, c.AccessLevel, r.DescRank "
                         + "from Users u "
                         + "left join UserCategories c on c.idUserCategory = u.idUserCategory "
                         + "left join UserRanks r on r.idUserRank = u.idUserRank "
                         + "where u.idUser = @idUser";
            DataTable perfiles = Consultar(query, "Perfil", "@idUser", idUser);
            return perfiles.Rows.Count == 1 ? perfiles.Rows[0] : null;
        }

        // Solo las opciones con nivel de acceso igual o menor al del usuario (control por elevación)
        public DataTable ObtenerOpciones(int nivelAcceso)
        {
            string query = "select idOption, description, image, formName, tableName, rangeOption, namespace "
                         + "from UserOptions where rangeOption <= @nivel order by description";
            return Consultar(query, "UserOptions", "@nivel", nivelAcceso);
        }

        private DataTable Consultar(string query, string tabla, string parametro, int valor)
        {
            using (SqlConnection conn = new SqlConnection(CadenaConexion))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
            {
                adapter.SelectCommand.Parameters.Add(parametro, SqlDbType.Int).Value = valor;
                DataSet dts = new DataSet();
                conn.Open();
                adapter.Fill(dts, tabla);
                conn.Close();
                return dts.Tables[tabla];
            }
        }
    }
}
