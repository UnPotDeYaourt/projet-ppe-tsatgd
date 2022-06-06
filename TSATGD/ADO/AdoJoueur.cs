using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TSATGD.Classes;

namespace TSATGD.ADO
{
    class AdoJoueur
    {
        public static List<Joueur> getAll()
        {
            List<Joueur> j = new List<Joueur>();
            try
            {
                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM joueur j INNER JOIN classement c on j.fk_classement = c.id_classement";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetInt32(3));
                    Classement c = MainWindow.classements.Where(x => x.IdClassement == reader.GetInt32(7)).First();
                    TypeJoueur type = MainWindow.typeJoueurs.Where(x => x.IdType == 1).First();
                    j.Add(new Joueur(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(6), reader.GetString(5), Convert.ToBoolean(reader.GetByte(8)),Convert.ToBoolean( reader.GetByte(4)), c,reader.GetString(9), type));
                }
                Ado.CloseSqlConnection();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return j;
        }
        public static Joueur getDernierJoueur()
        {
            Joueur joueur = new Joueur();
            try
            {
                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT TOP 1 * FROM joueur ORDER BY id_joueur DESC";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Classement c = MainWindow.classements.Where(x => x.IdClassement == reader.GetInt32(7)).First();
                    TypeJoueur type = MainWindow.typeJoueurs.Where(x => x.IdType == 1).First();
                    joueur = new Joueur(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(6), reader.GetString(5), Convert.ToBoolean(reader.GetByte(8)), Convert.ToBoolean(reader.GetByte(4)), c, reader.GetString(9), type);
                }
                Ado.CloseSqlConnection();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return joueur;
        }
        public static void create(Joueur joueurj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "INSERT INTO joueur (nom_joueur,prenom_joueur,annee_naiss,is_plus,fk_classement,mail_joueur,is_paye,certif_medic,licence, fk_typeJoueur)VALUES ('" + joueurj.NomJoueur + "','" + joueurj.PrenomJoueur + "','" + joueurj.AnneeNaissance + "','" + Convert.ToByte(joueurj.IsSeniorPlus) + "','" + joueurj.ClassementJoueur.IdClassement + "','" + joueurj.MailJoueur + "','" + 0 + "','" + joueurj.Certification+ "','" + joueurj.Licence +"'," + joueurj.TypeJoueur.IdType +")";
                cmd.ExecuteNonQuery();
                Ado.CloseSqlConnection();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ajouterLicence(Joueur joueurj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "UPDATE joueur SET licence = '" + joueurj.Licence + "' WHERE id_joueur = " + joueurj.IdJoueur;
                cmd.ExecuteNonQuery();
                Ado.CloseSqlConnection();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ajouterCertif(Joueur joueurj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "UPDATE joueur SET certif_medic = '" + joueurj.Licence + "' WHERE id_joueur = " + joueurj.IdJoueur;
                cmd.ExecuteNonQuery();
                Ado.CloseSqlConnection();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /*
        public static void mangemoilepoiro(Joueur _joueur)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM joueur j INNER JOIN classement c ON j.fk_classement = c.id WHERE c.valeur_classment "

            }
        }*/
    }
}
