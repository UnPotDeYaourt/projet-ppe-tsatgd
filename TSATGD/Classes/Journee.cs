using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSATGD.Classes
{
    public class Journee
    {
        public int IdJournee { get; set; }
        public DateTime DateJournee { get; set; }
        public byte IsSeniorPlus { get; set; }
        public List<Rencontre> Recontres {get;set;}
        public Dictionary<Joueur, bool> isHere { get; set; }
        public String SeniorLabel { get; set; }

        public Journee() { }

        public Journee (int _IdJournee,DateTime _DateJournee,byte _IsSeniorPlus)
        {
            this.IdJournee = _IdJournee;
            this.DateJournee = _DateJournee;
            this.IsSeniorPlus = _IsSeniorPlus;
            this.Recontres = new List<Rencontre>();
            this.isHere = new Dictionary<Joueur, bool>();
            if (this.IsSeniorPlus == 0) this.SeniorLabel = "Senior";
            else this.SeniorLabel = "Senior +";
        }

        public Journee(int _IdJournee, DateTime _DateJournee, byte _IsSeniorPlus,Equipe e, List<Joueur> j)
        {
            this.IdJournee = _IdJournee;
            this.DateJournee = _DateJournee;
            this.IsSeniorPlus = _IsSeniorPlus;
            this.Recontres = new List<Rencontre>();
            this.isHere = new Dictionary<Joueur, bool>();
            if (this.IsSeniorPlus == 0) this.SeniorLabel = "Senior";
            else this.SeniorLabel = "Senior +";
            
        }

        public void addRencontre(Rencontre rencontre)
        {
            this.Recontres.Add(rencontre);
        }
    }
}
