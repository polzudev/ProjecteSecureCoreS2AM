using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProjecteSecureCoreS2AM
{
    public class UsuariosBD
    {
        private string CadenaConexion
        {
            get
            {
                ConnectionStringSettings conf = ConfigurationManager.ConnectionStrings["SecureCore"];
                return conf.ConnectionString;
            }
        }

        public DataRow ObtenerUsuario(string login)
        {
            string query = "select idUser, CodeUser, UserName, Login, Password, Salt, idUserCategory, idUserRank from Users where Login = @login";
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
