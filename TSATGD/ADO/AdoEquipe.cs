using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSATGD.Classes;
using System.Data.SqlClient;

namespace TSATGD.ADO
{
    class AdoEquipe
    {
        public static List<Equipe> getAll()
        {
            List<Equipe> e = new List<Equipe>();
            try
            {
                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * from equipe";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    e.Add(new Equipe(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),reader.GetByte(3)));
                Ado.CloseSqlConnection();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return e;
        }
        public static void create(Equipe equipee)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "INSERT INTO equipe (rang_equipe,division,is_plus)('" + equipee.RangEquipe + "','" + equipee.Division + "','" + equipee.IsSeniorPlus + "')";
                cmd.ExecuteNonQuery();
                Ado.CloseSqlConnection();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
