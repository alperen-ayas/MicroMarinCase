using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCase.Domain.SeedWorks
{
    public abstract class Entity : IEntity
    {
        public abstract string Id { get; set; }
    }


}
