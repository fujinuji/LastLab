using System;
using System.Collections.Generic;
using System.Text;

namespace FinalLaboratory.Domain
{
    public class Entity<ID>
    {
        private ID _id;
        public ID Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Entity(ID id)
        {
            this._id = id;
        }
    }
}
