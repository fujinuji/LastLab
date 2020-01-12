using System;
using System.Collections.Generic;
using System.Text;

namespace FinalLaboratory.Domain
{
    public class Echipa : Entity<int>
    {
        public string Nume { set; get; }

        public Echipa(int id, string nume) : base(id)
        {
            Nume = nume;
        }
    }
}
