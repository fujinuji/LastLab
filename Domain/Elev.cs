using System;
using System.Collections.Generic;
using System.Text;

namespace FinalLaboratory.Domain
{
    public class Elev : Entity<int>
    {

        public string Nume { set; get; }
        public string Scoala { set; get; }

        public Elev(int id, string nume, string scoala) : base(id)
        {
            Nume = nume;
            Scoala = scoala;
        }
    }
}
