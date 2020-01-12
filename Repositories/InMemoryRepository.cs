using FinalLaboratory.Domain;
using FinalLaboratory.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalLaboratory.Repositories
{
    public class InMemoryRepository<ID, E> : ICrudRepository<ID, E> where E : Entity<ID>
    {
        private List<E> _entities;
        public List<E> Entities
        {
            set { _entities = value; }
            get { return _entities; }
        }

        public IValidator<E> Validator { set; get; }


        public InMemoryRepository(IValidator<E> validator)
        {
            _entities = new List<E>();
            Validator = validator;
        }

        public virtual E delete(ID id)
        {
            if (id.Equals(null))
                throw new ArgumentNullException();

            if (_entities.Select(x => x.Id).Contains(id))
            {
                E entity = _entities.Where(x => x.Id.Equals(id)).First();
                _entities.Remove(entity);
                return entity;
            }

            return null;
        }

        public virtual List<E> findAll()
        {
            return _entities;
        }

        public virtual E findOne(ID id)
        {
            if (id.Equals(null))
                throw new ArgumentNullException();
            if (_entities.Select(x=> x.Id).Contains(id))
                return _entities.Where(x => x.Id.Equals(id)).First();
            return null;
        }

        public virtual E save(E entity)
        {
            if (entity.Equals(null))
                throw new ArgumentNullException();
            Validator.validate(entity);

            if ( !_entities.Contains(entity) )
            {
                _entities.Add(entity);
                return null;
            }
            return entity;
        }

        public virtual E update(E entity)
        {
            if (entity.Equals(null))
                throw new ArgumentNullException();
            Validator.validate(entity);

            if (_entities.Select(x => x.Id).Contains(entity.Id))
            {
                E entityOld = _entities.Where(x => x.Id.Equals(entity.Id)).First();
                _entities.Remove(entityOld);
                _entities.Add(entity);
                return null;
            }
            return entity;
        }
    }
}
