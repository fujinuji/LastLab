using FinalLaboratory.Domain;
using FinalLaboratory.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FinalLaboratory.Ui
{
    class UserInterface
    {
        private FileRepositoryJucatorActiv _repoJucatorActiv;
        private FileRepositoryJucatori _repoJucatori;
        private FileRepositoryMeciuri _repoMeciuri;
        private FileRepositoryEchipe _repoEchipe;

        public UserInterface(FileRepositoryJucatorActiv repoJucatorActiv)
        {
            _repoJucatorActiv = repoJucatorActiv;
            _repoJucatori = repoJucatorActiv.RepoJucatori;
            _repoMeciuri = repoJucatorActiv.RepoMeciuri;
            _repoEchipe = _repoJucatori.RepoEchipe;
        }

        private void PrintMainMenu()
        {
            Console.WriteLine("===================== MENU =====================");
            Console.WriteLine("1 : Sa se afiseze toti jucatorii unei echipe date");
            Console.WriteLine("2 : Sa se afiseze toti jucatorii activi ai unei echipe de la un anumit meci");
            Console.WriteLine("3 : Sa se afiseze toate meciurile dintr - o anumita perioada calendaristica");
            Console.WriteLine("4 : Sa se determine si sa se afiseze scorul de la un anumit meci");
            Console.WriteLine("0 : Iesire aplicatie");
            Console.Write(" > ");
        }

        private void PrintEchipe()
        {
            Console.WriteLine("");
            Console.WriteLine("Echipele existente sunt: ");
            foreach (Echipa echipa in _repoEchipe.findAll())
            {
                Console.WriteLine("Echipa " + echipa.Id + " : " + echipa.Nume);
            }
            Console.WriteLine("");

        }
        private void PrintMeciuri()
        {
            Console.WriteLine("");
            Console.WriteLine("Meciurile existente sunt: ");
            foreach (Meci meci in _repoMeciuri.findAll())
            {
                Console.WriteLine("Intre echipele: " + meci.Id + " in data de " + meci.Date.ToShortDateString());
            }
            Console.WriteLine("");

        }

        private void ExecuteCommand1(string nrEchipa)
        {
            try
            {
                Echipa echipa = _repoEchipe.findOne(int.Parse(nrEchipa));
                if (echipa == null)
                {
                    Console.WriteLine("Nu s-a gasit echipa respectiva!");
                    return;
                }
                Console.WriteLine("Jucatorii echipei " + echipa.Nume);

                _repoJucatori.findAll().Where(x => x.Echipa.Id == echipa.Id).ToList().ForEach(x => Console.WriteLine(x.Id + " " + x.Nume));
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Nu ai introdus nr echipa!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Trebuie sa introduci numarul corespunzator echipei!");
            }
        }

        private void ExecuteCommand2(string nrMeci, string nrEchipa)
        {
            try
            {
                if (nrMeci == null)
                {
                    Console.WriteLine("Nu ai introdus numarul meciului!");
                    return;
                }

                string regex = ".*([0-9]+).*-.*([0-9]+).*";
                string idMeci = nrMeci.Trim(' ');
                if (!Regex.IsMatch(idMeci, regex))
                {
                    Console.WriteLine("Numarul meciului trebuie sa fie de forma '[Nr echipa 1] - [Nr echipa 2]' !");
                    return;
                }
                Meci meci = _repoMeciuri.findOne(idMeci);
                if (meci == null)
                {
                    Console.WriteLine("Nu s-a gasit meciul respectiv!");
                    return;
                }


                Echipa echipa = _repoEchipe.findOne(int.Parse(nrEchipa));
                if(echipa == null)
                {
                    Console.WriteLine("Nu s-a gasit echipa respectiva!");
                    return;
                }

                string[] substringMeci = idMeci.Split('-');
                if(int.Parse(substringMeci[0]) != echipa.Id && int.Parse(substringMeci[1]) != echipa.Id)
                {
                    Console.WriteLine("Echipa respectiva nu face parte din meciul introdus!");
                    return;
                }

                _repoJucatorActiv.findAll()
                    .Where(x => x.IdMeci.Trim(' ') == meci.Id.Trim(' '))
                    .Where(x => _repoJucatori.findOne(x.IdJucator).Echipa.Id == echipa.Id)
                    .ToList() 
                    .ForEach(x => Console.WriteLine(
                        _repoJucatori.findOne(x.IdJucator).Nume + " a jucat ca " +
                        x.Tip + " si a inscris " +
                        x.NrPuncteInscrise + " puncte"
                        ));

                Console.WriteLine("");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Nu ai introdus numarul echipei!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Numarul echipei trebuie sa fie numar!");
            }
        }

        private void ExecuteCommand3(string dataStart, string dataFinish)
        {
            try
            {
                DateTime dataS = DateTime.Parse(dataStart);
                DateTime dataF = DateTime.Parse(dataFinish);

                List<Meci> meciuriValide = _repoMeciuri.findAll().Where(
                        x => x.Date.Date > dataS && x.Date.Date < dataF
                        ).ToList();
                
                if(meciuriValide.Count() == 0)
                {
                    Console.WriteLine("Nu s-au gasit meciuri in perioada respectiva!");
                    return;
                }

                Console.WriteLine("");
                meciuriValide.ForEach(x => Console.WriteLine(
                    "In data de: " + x.Date.ToShortDateString() + " exista meciul intre echipa " + x.Echipa1.Nume + " si echipa " + x.Echipa2.Nume)
                );

                Console.WriteLine("");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Nu ai introdus o data!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Nu ai introdus o data valida!");
            }
        }

        private void ExecuteCommand4(string nrMeci)
        {
            try
            {
                if (nrMeci == null)
                {
                    Console.WriteLine("Nu ai introdus numarul meciului!");
                    return;
                }

                string regex = ".*([0-9]+).*-.*([0-9]+).*";
                string idMeci = nrMeci.Trim(' ');
                if (!Regex.IsMatch(idMeci, regex))
                {
                    Console.WriteLine("Numarul meciului trebuie sa fie de forma '[Nr echipa 1] - [Nr echipa 2]' !");
                    return;
                }
                Meci meci = _repoMeciuri.findOne(idMeci);
                if (meci == null)
                {
                    Console.WriteLine("Nu s-a gasit meciul respectiv!");
                    return;
                }

                int scorEchipa1 = 0;
                int scorEchipa2 = 0;

                List<int> listaScorEchipa1 = _repoJucatorActiv.findAll()
                    .Where(x => x.IdMeci.Trim(' ') == meci.Id.Trim(' '))
                    .Where(x => _repoJucatori.findOne(x.IdJucator).Echipa.Id == meci.Echipa1.Id)
                    .Select(x => x.NrPuncteInscrise)
                    .ToList();

                List<int> listaScorEchipa2 = _repoJucatorActiv.findAll()
                    .Where(x => x.IdMeci.Trim(' ') == meci.Id.Trim(' '))
                    .Where(x => _repoJucatori.findOne(x.IdJucator).Echipa.Id == meci.Echipa2.Id)
                    .Select(x => x.NrPuncteInscrise)
                    .ToList();

                foreach (int scor in listaScorEchipa1)
                    scorEchipa1 += scor;
                foreach (int scor in listaScorEchipa2)
                    scorEchipa2 += scor;

                Console.WriteLine("Meciul dintre "+
                    meci.Echipa1.Nume + " si " + meci.Echipa2.Nume + " s-a terminat cu scorul "
                    + scorEchipa1 + " - " + scorEchipa2
                    );

                Console.WriteLine("");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Nu ai introdus numarul meciului!");
            }
        }

        public void Run()
        {
            string comanda = "";
            while (comanda != "0")
            {
                try
                {
                    PrintMainMenu();
                    comanda = Console.ReadLine();
                    switch (comanda)
                    {
                        case "0":
                            Console.WriteLine("Bye Bye!");
                            break;
                        case "1":
                            PrintEchipe();
                            Console.Write("Introdu numarul echipei > ");
                            
                            ExecuteCommand1(Console.ReadLine());
                            break;
                        case "2":
                            PrintEchipe();
                            PrintMeciuri();
                            Console.Write("Introdu meciul >");
                            string meci = Console.ReadLine();
                            Console.Write("Introdu echipa >");
                            string echipa = Console.ReadLine();

                            ExecuteCommand2(meci,echipa);
                            break;
                        case "3":
                            PrintMeciuri();
                            Console.Write("Introdu perioada de start > ");
                            string dateStart = Console.ReadLine();
                            Console.Write("Introdu perioada de sfarsit > ");
                            string dateFinish = Console.ReadLine();
                            
                            ExecuteCommand3(dateStart, dateFinish);
                            break;
                        case "4":
                            PrintMeciuri();
                            Console.Write("Introdu meciul >");

                            ExecuteCommand4(Console.ReadLine());
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return;
        }

    }
}
