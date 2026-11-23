using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProjecteSecureCoreS2AM
{
    /// <summary>
    /// Proporciona accés a les dades necessàries per a la pantalla principal: perfil de l'usuari
    /// autenticat i opcions de menú filtrades pel seu nivell d'accés.
    /// </summary>
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

        /// <summary>
        /// Obté el perfil complet de l'usuari indicat, incloent categoria, nivell d'accés i rang.
        /// </summary>
        /// <param name="idUser">Identificador numèric de l'usuari.</param>
        /// <returns>
        /// <see cref="DataRow"/> amb les columnes idUser, CodeUser, UserName, Login,
        /// CodeCategory, DescCategory, AccessLevel i DescRank, o <c>null</c> si no existeix.
        /// </returns>
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

        /// <summary>
        /// Obté les opcions de menú disponibles per al nivell d'accés indicat, ordenades
        /// alfabèticament per descripció.
        /// </summary>
        /// <param name="nivelAcceso">Nivell d'accés de l'usuari (columna <c>AccessLevel</c>).</param>
        /// <returns>
        /// <see cref="DataTable"/> amb les columnes idOption, description, image, formName,
        /// tableName, rangeOption i namespace de les opcions amb <c>rangeOption &lt;= nivelAcceso</c>.
        /// </returns>
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
