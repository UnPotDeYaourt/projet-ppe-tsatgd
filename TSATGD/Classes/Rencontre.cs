using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSATGD.Classes
{
    public class Rencontre
    {
        public int IdRencontre { get; set; }
        public string Adversaire { get; set; }
        public string Lieu { get; set; }
        //DateTime Heure { get; set; }
        public Equipe UneEquipe { get; set; }
        public List<Joueur> Joueurs { get; set; }
        public Journee Journee { get; set; }

        public Rencontre(){}

        public Rencontre(int _IdRencontre,string _Adversaire,string _Lieu,Equipe _Equipe, Journee _Journee)
        {
            this.IdRencontre = _IdRencontre;
            this.Adversaire = _Adversaire;
            this.Lieu = _Lieu;
            this.UneEquipe = _Equipe;
            this.Joueurs = new List<Joueur>();
            this.Journee = _Journee;
        }

        public Rencontre(string _Adversaire, string _Lieu)
        {
            this.IdRencontre = 0;
            this.Adversaire = _Adversaire;
            this.Lieu = _Lieu;
            this.UneEquipe = new Equipe();
            this.Joueurs = new List<Joueur>();
            this.Journee = new Journee();
        }
        public void addJoueur(Joueur joueur)
        {
            this.Joueurs.Add(joueur);
        }
    }
}
