using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign.Core.Services
{
    public class CoreModel : ICoreModel
    {
        public int Id { get; set; }
        public virtual string Name { get; set; }
    }
}
