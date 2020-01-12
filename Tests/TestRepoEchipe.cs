using FinalLaboratory.Domain;
using FinalLaboratory.Repositories;
using FinalLaboratory.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalLaboratory.Tests
{
    class TestRepoEchipe
    {
        public bool test()
        {
            Echipa e1 = new Echipa(1, "The Misfits");
            Echipa e2 = new Echipa(2, "The Misfits2");
            Echipa e3 = new Echipa(3, "The Misfits3");
            Echipa e4 = new Echipa(3, "The Misfits4");

            InMemoryRepository<int, Echipa> repoEchipe = new InMemoryRepository<int, Echipa>(new EchipaValidator());
            repoEchipe.save(e1);
            repoEchipe.save(e2);
            repoEchipe.save(e3);


            Echipa f1 = new Echipa(0, "The Misfits");
            Echipa f2 = new Echipa(-9, "The Misfits");
            Echipa f3 = new Echipa(1, "");
            Echipa f4 = new Echipa(0, "");

            try
            {
                repoEchipe.save(f1);
                return false;
            }
            catch (ValidatorException) { }

            try
            {
                repoEchipe.save(f2);
                return false;
            }
            catch (ValidatorException) { }

            try
            {
                repoEchipe.save(f3);
                return false;
            }
            catch (ValidatorException) { }

            try
            {
                repoEchipe.save(f4);
                return false;
            }
            catch (ValidatorException) { }




            int size = 0;
            foreach (var elem in repoEchipe.findAll())
                size++;

            if (size != 3)
                return false;

            repoEchipe.delete(2);
            repoEchipe.update(e4);

            return false;
        }

        public TestRepoEchipe()
        {
           
        }
    }
}
