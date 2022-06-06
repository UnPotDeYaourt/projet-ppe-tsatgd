using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSATGD.Classes;
using System.Data.SqlClient;

namespace TSATGD.ADO
{
    class AdoType
    {
        public static List<TypeJoueur> getAll()
        {
            List<TypeJoueur> c = new List<TypeJoueur>();
            try
            {
                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM typeJoueur";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TypeJoueur type = new TypeJoueur(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                    
                    c.Add(type);
                }
                    
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
