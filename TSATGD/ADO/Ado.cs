using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TSATGD.ADO
{
    public class Ado
    {
        static SqlConnection connection;
        public static SqlConnection OpenSqlConnection()
        {
            string connectionString = GetConnectionString();
            connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            Console.WriteLine("State: {0}", connection.State);
            Console.WriteLine("ConnectionString: {0}", connection.ConnectionString);
            return connection;
        }
        public static void CloseSqlConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        static private string GetConnectionString()
        {
            return "Data Source=*******;Initial Catalog=*********;User ID=********;Password=********";
        }
    }
}
