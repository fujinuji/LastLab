using FinalLaboratory.Domain;
using FinalLaboratory.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalLaboratory.Repositories
{
    class FileRepositoryEchipe : FileRepository<int, Echipa>
    {

        public FileRepositoryEchipe(string file) : base(file, new EchipaValidator())
        {
            ReadFile();
        }

        public override string OutputLine(Echipa entity)
        {
            return entity.Id + " ; " + entity.Nume;
        }

        public override Echipa ParseLine(string line)
        {
            try
            {
                string[] substring = line.Split(";");
                int id = int.Parse(substring[0]);
                return new Echipa(id, substring[1]);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
