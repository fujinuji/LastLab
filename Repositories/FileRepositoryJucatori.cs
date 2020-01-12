using FinalLaboratory.Domain;
using FinalLaboratory.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FinalLaboratory.Repositories
{
    class FileRepositoryJucatori : FileRepository<int, Jucator>
    {
        private FileRepositoryEchipe _repoEchipe;
        public FileRepositoryEchipe RepoEchipe
        {
            set { _repoEchipe = value; }
            get { return _repoEchipe; }
        }

        public FileRepositoryJucatori(string file, FileRepositoryEchipe repoEchipe) : base(file, new JucatorValidator())
        {
            this._repoEchipe = repoEchipe;
            ReadFile();
        }


        private string generateNumeJucator()
        {
            string[] first = new string[] {"Abby", "Abigail", "Adele", "Adrian", "Ioana" , "Ion", "Paula", "Daria", "Delia", 
                                            "Maria", "Marius", "Monica", "Lili" , "Andrei", "Andreea", "Paul" , "Dragos", "Dan", "Daniel",
                                            "Victor", "Vlad", "Bogdan", "Alex", "Alexandru", "Oana"};
            string[] last = new string[] {"Pop","Swann","Ionel","Gheorghescu", "Csiki", "Crisan", "Comanici", "Polinca", "Minc", "Lopal", "Fopac",
                                           "Print", "Olaru", "Cioban"};

            Random rnd = new Random();
            int x = rnd.Next(0, first.Length);
            int y = rnd.Next(0, last.Length);

            return first[x] + " " + last[y];
        }

        private Jucator[] ParseLineJucator(string line, int index)
        {
            string[] substring = line.Split(";");
            try
            {
                int curent = index;
                Echipa echipa = _repoEchipe.findOne(int.Parse(substring[1]));
                Jucator[] jucatori = new Jucator[16];
                for (int i = 1; i <= 5; i++)
                {
                    jucatori[i] = new Jucator(curent, generateNumeJucator(), substring[0], echipa);
                    curent++;
                }
                return jucatori;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override void ReadFile()
        {
            StreamReader stream = new StreamReader(base.FileName);
            string line = stream.ReadLine();
            int index = 1;
            while (line != null && line != "")
            {
                Jucator[] jucatori = ParseLineJucator(line, index);
                foreach (Jucator jucator in jucatori)
                    try
                    {
                        base.save(jucator);
                        index++;
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                line = stream.ReadLine();
            }
        }

    }
}
