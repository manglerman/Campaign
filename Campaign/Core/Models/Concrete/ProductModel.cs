using Campaign.Core.Models.Abstract;
using Campaign.Core.Models.Enums;

namespace Campaign.Core.Services
{
    public class ProductModel : CoreModel, IProductModel, IEntity
    {
        public decimal Price { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
