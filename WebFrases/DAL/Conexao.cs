using System.Data;
using System.Data.SqlClient;

namespace WebFrases.DAL
{
    public class Conexao
    {

        SqlConnection con;

        public Conexao()
        {
            con = new SqlConnection();
            con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                     Initial Catalog=sisfrases;
                                     Integrated Security=true";
        }

        public SqlConnection Conectar()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();

            return con;
        }

        public void Desconectar()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }
}