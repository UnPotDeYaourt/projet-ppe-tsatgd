using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSATGD.Classes;
using System.Data.SqlClient;

namespace TSATGD.ADO
{
    class AdoJournee
    {
        public static List<Journee> getAll()
        {
            List<Journee> j = new List<Journee>();
            try
            {
                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * from (journee j INNER JOIN recontre r ON j.id_journee = r.fk_journee) INNER JOIN participer p ON p.fk_recontre = r.id_recontre order by id_journee,fk_equipe"; //selectionner toute les journées journee j INNER JOIN rencontres r ON r.fk_journee=j.id_journee 
                reader = cmd.ExecuteReader();
                Boolean fin = reader.Read();
                int idEquipe;
                int idJournee;
                idJournee = reader.GetInt32(0);
                while (fin)
                {
                    
                    while (fin)
                    {
                        Journee jo = new Journee(reader.GetInt32(0), reader.GetDateTime(1), reader.GetByte(2));
                        while (jo.IdJournee == idJournee && fin)
                        {
                            Equipe e = MainWindow.equipes.First(x => x.IdEquipe == reader.GetInt32(5));
                            Rencontre r = new Rencontre(reader.GetInt32(3), reader.GetString(4),reader.GetString(7), e, jo);
                            idEquipe = reader.GetInt32(5);
                            while (e.IdEquipe == idEquipe && fin)
                            {
                                if (fin)
                                {
                                    Joueur joueur = MainWindow.joueurs.First(x => x.IdJoueur == reader.GetInt32(8));
                                    r.addJoueur(joueur);
                                    fin = reader.Read();
                                    if (fin)
                                    { idEquipe = reader.GetInt32(5); }

                                }


                            }
                            jo.addRencontre(r);
                            if (fin)
                            {
                                idJournee = reader.GetInt32(0);
                            }
                        }
                        j.Add(jo);
                    }
                }
                Ado.CloseSqlConnection();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return j;
        }
        public static Dictionary<Joueur, bool> getPlayersIsHereForOneDay(int idDay)
        {
            Dictionary<Joueur, bool> playersIsHere = new Dictionary<Joueur, bool>();

            try
            {
                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT id_joueur, nom_joueur, prenom_joueur, annee_naiss, licence, certif_medic, is_paye, p.is_plus, mail_joueur, is_here FROM(journee j INNER JOIN disponible d ON j.id_journee = d.fk_journee) INNER JOIN joueur p ON p.id_joueur = d.fk_joueur WHERE j.id_journee = @Id;";
                cmd.Parameters.Add("@Id", idDay);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Classement c = MainWindow.classements.First(x => x.IdClassement == 1);
                    TypeJoueur type = MainWindow.typeJoueurs.Where(x => x.IdType == 1).First();
                    Joueur j = new Joueur(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5), Convert.ToBoolean(reader.GetByte(6)), Convert.ToBoolean(reader.GetByte(7)), c, reader.GetString(8), type);

                    playersIsHere.Add(j, Convert.ToBoolean(reader.GetByte(9)));
                }
                Ado.CloseSqlConnection();
            } 
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return playersIsHere;
        }
        public static void addPlayerDisponibilityForOneDay(int idDay, int idPlayer, bool value)
        {
            try
            {
                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "INSERT INTO disponible(fk_joueur, fk_journee, is_here) VALUES(@IdPlayer, @IdDay, @Value)";
                cmd.Parameters.Add("@IdPlayer", idPlayer);
                cmd.Parameters.Add("@IdDay", idDay);
                cmd.Parameters.Add("@Value", value);
                cmd.ExecuteNonQuery();
                Ado.CloseSqlConnection();
            } catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void deletePlayerDisponibilityForOneDay(int idDay, int idPlayer)
        {
            try
            {
                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Ado.OpenSqlConnection(); 
                cmd.CommandText = "DELETE FROM disponible WHERE fk_joueur = @IdPlayer AND fk_journee = @IdDay;";
                cmd.Parameters.Add("@IdPlayer", idPlayer);
                cmd.Parameters.Add("@IdDay", idDay);
                cmd.ExecuteNonQuery();
                Ado.CloseSqlConnection();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void updatePlayerIsHereForOneDay(int idDay, int idPlayer, bool value)
        {
            try
            {
                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "UPDATE disponible SET is_here = @Value WHERE fk_joueur = @IdPlayer AND fk_journee = @IdDay;";
                cmd.Parameters.Add("@Value", value);
                cmd.Parameters.Add("@IdPlayer", idPlayer);
                cmd.Parameters.Add("@IdDay", idDay);
                cmd.ExecuteNonQuery();
                Ado.CloseSqlConnection();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
            public static List<Journee> getAllLite()
        {
            List<Journee> journees = new List<Journee>();
            try
            {
                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "SELECT * FROM journee";
                reader = cmd.ExecuteReader();
                while(reader.Read())
                { 
                    journees.Add(new Journee(reader.GetInt32(0), reader.GetDateTime(1), reader.GetByte(2)));
                }
                Ado.CloseSqlConnection();
            } catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return journees;
        }
        public static void create(Journee journeej)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Ado.OpenSqlConnection();
                cmd.CommandText = "INSERT INTO journee (date_journee,is_plus) VALUES(@Date, @IsPlus);";
                cmd.Parameters.Add("@Date", journeej.DateJournee);
                cmd.Parameters.Add("@IsPlus", journeej.IsSeniorPlus);
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
