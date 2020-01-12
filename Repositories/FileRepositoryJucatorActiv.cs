using FinalLaboratory.Domain;
using FinalLaboratory.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalLaboratory.Repositories
{
    class FileRepositoryJucatorActiv : FileRepository<string, JucatorActiv>
    {
        private FileRepositoryJucatori _repoJucatori;
        public FileRepositoryJucatori RepoJucatori
        {
            get { return _repoJucatori; }
            set { _repoJucatori = value; }
        }

        private FileRepositoryMeciuri _repoMeciuri;
        public FileRepositoryMeciuri RepoMeciuri
        {
            get { return _repoMeciuri; }
            set { _repoMeciuri = value; }
        }


        public FileRepositoryJucatorActiv(string file, FileRepositoryJucatori repoJ, FileRepositoryMeciuri repoM) : base(file, new JucatorActivValidator())
        {
            _repoJucatori = repoJ;
            _repoMeciuri = repoM;
            ReadFile();
        }

        public override JucatorActiv ParseLine(string line)
        {
            // line - > idJucator ; idMeci ; nrPuncteInscrise ; tip
            try
            {
                string[] substring = line.Trim(' ').Split(";");
                Jucator jucator = _repoJucatori.findOne(int.Parse(substring[0].Trim(' ')));
                Meci meci = _repoMeciuri.findOne(substring[1].Trim(' '));
                return new JucatorActiv(
                    jucator.Id,
                    meci.Id,
                    int.Parse(substring[2].Trim(' ')),
                    substring[3].Trim(' ')
                    );
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
