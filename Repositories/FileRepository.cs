using FinalLaboratory.Domain;
using FinalLaboratory.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FinalLaboratory.Repositories
{
    class FileRepository<ID, E> : InMemoryRepository<ID, E> where E : Entity<ID>
    {
        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public FileRepository(string file, IValidator<E> validator) : base(validator)
        {
            this._fileName = file;
        }

        public virtual void ReadFile()
        {
            StreamReader stream = new StreamReader(_fileName);
            string line = stream.ReadLine();
            while (line != null && line !="")
            {
                E entity = ParseLine(line);
                try
                {
                    base.save(entity);
                }
                catch (Exception)
                {
                    continue;
                }
                line = stream.ReadLine();
            }

            stream.Close();
        }

        public virtual void WriteFile()
        {
            StreamWriter stream = new StreamWriter(_fileName);
            foreach (E entity in base.findAll())
            {
                string outputString = OutputLine(entity);
                stream.WriteLine(outputString);
            }
            stream.Close();
        }

        public virtual E ParseLine(string line) { return null; }
        public virtual string OutputLine(E entity) { return null; }

        public override E delete(ID id)
        {
            return base.delete(id);
        }

        public override E save(E entity)
        {
            return base.save(entity);
        }

        public override E update(E entity)
        {
            return base.update(entity);
        }

        public override List<E> findAll()
        {
            return base.findAll();
        }

        public override E findOne(ID id)
        {
            return base.findOne(id);
        }

    }
}
