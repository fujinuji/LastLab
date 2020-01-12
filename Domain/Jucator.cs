using System;
using System.Collections.Generic;
using System.Text;

namespace FinalLaboratory.Domain
{
    public class Jucator : Elev
    {
        public Echipa Echipa { set; get; }

        public Jucator(int id, string nume, string scoala, Echipa echipa) : base(id, nume, scoala)
        {
            Echipa = echipa;
        }
    }
}
