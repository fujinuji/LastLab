using System;
using System.Collections.Generic;
using System.Text;

namespace FinalLaboratory.Domain
{
    public class Meci : Entity<string>
    {
        public Echipa Echipa1 { set; get; }
        public Echipa Echipa2 { set; get; }
        public DateTime Date { set; get; }

        public Meci(Echipa echipa1, Echipa echipa2, DateTime date) : base(echipa1.Id + "-" + echipa2.Id)
        {
            Echipa1 = echipa1;
            Echipa2 = echipa2;
            Date = date.Date;
        }
    }
}
