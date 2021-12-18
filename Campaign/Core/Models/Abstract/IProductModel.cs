using Campaign.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign.Core.Services
{
    public interface IProductModel : ICoreModel
    {
        decimal Price { get; set; }
        ProductCategory ProductCategory { get; set; }
    }
}
