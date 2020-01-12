using FinalLaboratory.Domain;
using FinalLaboratory.Repositories;
using FinalLaboratory.Validators;
using FinalLaboratory.Tests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace FinalLaboratory
{
    class Program
    {
        private static void TestAll()
        {
            Debug.Assert(Tests.TesteRepository.testRepo(), "Testele pe repository failed!");
        }

        static void Main(string[] args)
        {
            FileRepositoryEchipe _repoEchipe = new FileRepositoryEchipe(@"C:\Users\Cosmin\Desktop\Fin alLaboratory\Data\DataEchipe.txt");
            FileRepositoryJucatori _repoJucatori = new FileRepositoryJucatori(@"C:\Users\Cosmin\Desktop\FinalLaboratory\Data\DataJucatori.txt", _repoEchipe);
            FileRepositoryMeciuri _repoMeciuri = new FileRepositoryMeciuri(@"C:\Users\Cosmin\Desktop\FinalLaboratory\Data\DataMeciuri.txt", _repoEchipe);
            FileRepositoryJucatorActiv _repoJucatoriActivi = new FileRepositoryJucatorActiv(@"C:\Users\Cosmin\Desktop\FinalLaboratory\Data\DataJucatoriActivi.txt", _repoJucatori, _repoMeciuri);
            
            TestAll();

            Ui.UserInterface ui = new Ui.UserInterface(_repoJucatoriActivi);
            ui.Run();

        }
    }
}
