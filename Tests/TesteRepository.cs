using FinalLaboratory.Domain;
using FinalLaboratory.Repositories;
using FinalLaboratory.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalLaboratory.Tests
{
    class TesteRepository
    {
        public static Boolean testRepo()
        {
            TestRepoEchipe repoEchipe = new TestRepoEchipe();
            return repoEchipe.test();
        }
    }
}
