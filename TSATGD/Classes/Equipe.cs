using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSATGD.Classes
{
    public class Equipe
    {
        public int IdEquipe { get; set; }
        public string RangEquipe { get; set; }
        public string Division { get; set; }
        public byte IsSeniorPlus { get; set; }

        public Equipe() { }

        public Equipe (int _IdEquipe,string _RangEquipe,string _Division,byte _IsSeniorPlus)
        {
            this.IdEquipe = _IdEquipe;
            this.RangEquipe = _RangEquipe;
            this.Division = _Division;
            this.IsSeniorPlus = _IsSeniorPlus;
        }
            
    }
}
