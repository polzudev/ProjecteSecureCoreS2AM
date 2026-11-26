using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProjecteSecureCoreS2AM
{
    public class AccesoDatos
    {
        private SqlConnection conn;

        public AccesoDatos()
        {
            Connectar();
        }

        private void Connectar()
        {
            ConnectionStringSettings conf = ConfigurationManager.ConnectionStrings["SecureCore"];
            if (conf == null)
            {
                throw new ConfigurationErrorsException("Falta la cadena de connexió 'SecureCore' a App.config");
            }
            conn = new SqlConnection(conf.ConnectionString);
        }

        public DataSet PortarTaula(string taula)
        {
            return PortarPerConsulta("select * from " + taula, taula);
        }

        public DataSet PortarPerConsulta(string consulta)
        {
            return PortarPerConsulta(consulta, "Table");
        }

        public DataSet PortarPerConsulta(string consulta, string nomDataTable)
        {
            return Omplir(consulta, new DataSet(), nomDataTable);
        }

        public DataTable PortarDataTable(string consulta)
        {
            return PortarPerConsulta(consulta).Tables[0];
        }

        public DataSet AfegirDataTable(string consulta, DataSet dts)
        {
            return Omplir(consulta, dts, "Table" + dts.Tables.Count);
        }

        public int Actualitzar(string consulta, DataSet dts)
        {
            int registresAfectats = 0;
            using (SqlDataAdapter adapter = new SqlDataAdapter(consulta, conn))
            using (SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter))
            {
                try
                {
                    conn.Open();
                    if (dts.HasChanges())
                    {
                        registresAfectats = adapter.Update(dts.Tables[0]);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
            return registresAfectats;
        }

        public int Executa(string consulta)
        {
            int registresAfectats;
            using (SqlCommand cmd = new SqlCommand(consulta, conn))
            {
                try
                {
                    conn.Open();
                    registresAfectats = cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
            return registresAfectats;
        }

        /// <summary>
        /// Executa un Stored Procedure i retorna el resultat com a <see cref="DataTable"/>.
        /// </summary>
        /// <param name="nomSP">Nom del Stored Procedure a executar.</param>
        /// <param name="parametres">Paràmetres del SP com a diccionari nom→valor. Pot ser null.</param>
        /// <returns><see cref="DataTable"/> amb el resultat retornat pel SP.</returns>
        public DataTable ExecutarSP(string nomSP, Dictionary<string, object> parametres = null)
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(nomSP, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (parametres != null)
                {
                    foreach (var p in parametres)
                    {
                        if (p.Value == null)
                            cmd.Parameters.AddWithValue(p.Key, DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue(p.Key, p.Value);
                    }
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    try
                    {
                        conn.Open();
                        adapter.Fill(dt);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return dt;
        }

        private DataSet Omplir(string consulta, DataSet dts, string nomDataTable)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(consulta, conn))
            {
                try
                {
                    conn.Open();
                    adapter.Fill(dts, nomDataTable);
                }
                finally
                {
                    conn.Close();
                }
            }
            return dts;
        }
        public void ExecutarTransaccio(List<string> consultes)
        {
            conn.Open();
            SqlTransaction transaccio = conn.BeginTransaction();
            try
            {
                foreach (string consulta in consultes)
                {
                    SqlCommand cmd = new SqlCommand(consulta, conn, transaccio);
                    cmd.ExecuteNonQuery();
                }
                transaccio.Commit();
            }
            catch
            {
                transaccio.Rollback(); 
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
