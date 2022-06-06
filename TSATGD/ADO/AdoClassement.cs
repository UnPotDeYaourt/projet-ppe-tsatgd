using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSATGD.Classes;
using System.Data.SqlClient;

namespace TSATGD.ADO
{
    class AdoClassement
    {
        public static List<Classement> getAll()
        {
            List<Classement> c = new List<Classement>();
            try
            {
                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM classement";
                reader = cmd.ExecuteReader();       
                while (reader.Read())
                    c.Add(new Classement(reader.GetInt32(0),reader.GetInt32(1),reader.GetString(2)));
                Ado.CloseSqlConnection();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return c;
        }
    }
}
