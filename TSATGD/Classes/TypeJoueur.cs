using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSATGD.Classes
{
    public class TypeJoueur
    {
        public int IdType { get; set; }
        public string Libelle { get; set; }
        public string Code { get; set; }
        public List<Joueur> PlayerList { get; set; }

        public TypeJoueur(int _idType, string _libelle, string _code)
        {
            this.IdType = _idType;
            this.Libelle = _libelle;
            this.Code = _code;
            this.PlayerList = new List<Joueur>();
        }

        public void AddJoueur(Joueur _joueur)
        {
            this.PlayerList.Add(_joueur);
        }

        public override string ToString()
        {
            return this.Libelle;
        }
    }
}
