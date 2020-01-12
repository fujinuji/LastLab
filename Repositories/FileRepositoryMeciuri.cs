using FinalLaboratory.Domain;
using FinalLaboratory.Validators;
using System;

namespace FinalLaboratory.Repositories
{
    class FileRepositoryMeciuri : FileRepository<string, Meci>
    {
        private FileRepositoryEchipe _repoEchipe;

        public FileRepositoryMeciuri(string file, FileRepositoryEchipe repo) : base(file, new MeciValidator())
        {
            _repoEchipe = repo;
            ReadFile();
        }

        public override Meci ParseLine(string line)
        {
            try
            {
                string[] substring = line.Split(";");
                Echipa echipa1 = _repoEchipe.findOne(int.Parse(substring[0]));
                Echipa echipa2 = _repoEchipe.findOne(int.Parse(substring[1]));
                return new Meci(echipa1, echipa2, DateTime.Parse(substring[2]).Date);

            } catch (Exception)
            {
                return null;
            }
        }
    }
}
