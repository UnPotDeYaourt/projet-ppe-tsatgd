using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSATGD.Classes;

namespace TSATGD.Classes
{
    public class Joueur
    {
        public int IdJoueur { get; set; }
        public string NomJoueur { get; set; }
        public string PrenomJoueur { get; set; }
        public int AnneeNaissance { get; set; }
        public string Licence { get; set; }
        public string Certification { get; set; }
        public Boolean IsPaye { get; set; }
        public Boolean IsSeniorPlus { get; set; }
        public Classement ClassementJoueur { get; set; }
        public string MailJoueur { get; set; }
        public TypeJoueur TypeJoueur { get; set; }

        public Joueur() {}
        public Joueur(int _id, string _NomJoueur,string _PrenomJoueur,int _AnneeNaissance,string _Licence,string _Certification, Boolean _IsPaye, Boolean _IsSeniorPlus,Classement _ClassementJoueur, string _MailJoueur, TypeJoueur _typeJoueur)
        {
            this.IdJoueur = _id;
            this.NomJoueur = _NomJoueur;
            this.PrenomJoueur  =  _PrenomJoueur;
            this.AnneeNaissance = _AnneeNaissance;
            this.Licence = _Licence;
            this.Certification = _Certification;
            this.IsPaye = _IsPaye;
            this.IsSeniorPlus = _IsSeniorPlus;
            this.ClassementJoueur = _ClassementJoueur;
            this.MailJoueur = _MailJoueur;
            this.TypeJoueur = _typeJoueur;
        }

        public Joueur(string _NomJoueur, string _PrenomJoueur, int _AnneeNaissance, string _Licence, string _Certification, Boolean _IsPaye, Boolean _IsSeniorPlus, Classement _ClassementJoueur, string _MailJoueur, TypeJoueur _typeJoueur)
        {
            this.IdJoueur = 0;
            this.NomJoueur = _NomJoueur;
            this.PrenomJoueur = _PrenomJoueur;
            this.AnneeNaissance = _AnneeNaissance;
            this.Licence = _Licence;
            this.Certification = _Certification;
            this.IsPaye = _IsPaye;
            this.IsSeniorPlus = _IsSeniorPlus;
            this.ClassementJoueur = _ClassementJoueur;
            this.MailJoueur = _MailJoueur;
            this.TypeJoueur = _typeJoueur;
        }

        public Joueur(string _NomJoueur, string _PrenomJoueur, int _AnneeNaissance, Boolean _IsSeniorPlus, Classement _ClassementJoueur, string _MailJoueur)
        {
            this.NomJoueur = _NomJoueur;
            this.PrenomJoueur = _PrenomJoueur;
            this.AnneeNaissance = _AnneeNaissance;
            this.IsSeniorPlus = _IsSeniorPlus;
            this.ClassementJoueur = _ClassementJoueur;
            this.MailJoueur = _MailJoueur;
        }
    }
}
