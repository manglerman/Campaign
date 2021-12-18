using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign.Core.Services
{
    public interface ICoreModel
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
