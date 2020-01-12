using System;
using System.Collections.Generic;
using System.Text;

namespace FinalLaboratory.Domain
{
    class JucatorActiv : Entity<string>
    {
        public int IdJucator { get; set; }
        public string IdMeci { get; set; }
        public int NrPuncteInscrise { get; set; }
        public string Tip { get; set; } // Rezerva, Partipicant

        public JucatorActiv(int idJucator, string idMeci, int nrPuncteInscrise, string tip) : base(idJucator + ";" + idMeci)
        {
            IdJucator = idJucator;
            IdMeci = idMeci;
            NrPuncteInscrise = nrPuncteInscrise;
            Tip = tip;
        }
    }
}
