using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProjecteSecureCoreS2AM
{
    /// <summary>
    /// Proporciona accés a la taula d'usuaris de la base de dades per a la validació i gestió de credencials.
    /// Totes les consultes utilitzen paràmetres per evitar injeccions SQL.
    /// </summary>
    public class UsuariosBD
    {
        /// <summary>
        /// Obté la cadena de connexió definida com a "SecureCore" a App.config.
        /// </summary>
        private string CadenaConexion
        {
            get
            {
                ConnectionStringSettings conf = ConfigurationManager.ConnectionStrings["SecureCore"];
                return conf.ConnectionString;
            }
        }

        /// <summary>
        /// Obté la fila de l'usuari corresponent al login indicat, incloent la seva categoria d'accés.
        /// </summary>
        /// <param name="login">Identificador d'accés de l'usuari (màxim 12 caràcters).</param>
        /// <returns>
        /// <see cref="System.Data.DataRow"/> amb les columnes idUser, CodeUser, UserName, Login,
        /// Password, Salt, idUserCategory, idUserRank i DescCategory, o <c>null</c> si no existeix.
        /// </returns>
        public DataRow ObtenerUsuario(string login)
        {
            string query = "select u.idUser, u.CodeUser, u.UserName, u.Login, u.Password, u.Salt, u.idUserCategory, u.idUserRank, uc.DescCategory from Users u left join UserCategories uc on u.idUserCategory = uc.idUserCategory where u.Login = @login";
            using (SqlConnection conn = new SqlConnection(CadenaConexion))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@login", login);
                DataSet dts = new DataSet();
                conn.Open();
                adapter.Fill(dts, "Users");
                conn.Close();
                DataTable usuarios = dts.Tables["Users"];
                return usuarios.Rows.Count == 1 ? usuarios.Rows[0] : null;
            }
        }

        /// <summary>
        /// Desa el salt i el hash de la nova paraula de pas de l'usuari a la base de dades.
        /// S'utilitza en el primer accés per substituir la paraula de pas inicial en text pla.
        /// </summary>
        /// <param name="idUser">Identificador numèric de l'usuari.</param>
        /// <param name="salt">Salt criptogràfic en format hexadecimal (generat per <see cref="GestorPasswords.GenerarSalt"/>).</param>
        /// <param name="hash">Hash PBKDF2 de la nova paraula de pas en format hexadecimal.</param>
        public void GuardarPassword(int idUser, string salt, string hash)
        {
            string query = "update Users set Salt = @salt, Password = @hash where idUser = @idUser";
            using (SqlConnection conn = new SqlConnection(CadenaConexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.Parameters.AddWithValue("@hash", hash);
                cmd.Parameters.AddWithValue("@idUser", idUser);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
