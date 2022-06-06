using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSATGD.Classes
{
    public class Classement
    {
        public int IdClassement { get; set; }
        public int ValeurClassement { get; set; }
        public string LibelleClassement { get; set; }

        public Classement() { }

        public Classement (int _IdClassement,int _ValeurClassement,string _LibelleClassement)
        {
            this.IdClassement = _IdClassement;
            this.ValeurClassement = _ValeurClassement;
            this.LibelleClassement = _LibelleClassement;
        }

        public override string ToString()
        {
            return this.LibelleClassement;
        }
    }
}
