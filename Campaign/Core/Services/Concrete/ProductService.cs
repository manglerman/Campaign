using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Campaign.Core.Services.Abstract;
using Campaign.Core.Data;
using System.Linq;
using Campaign.Core.Exceptions;

namespace Campaign.Core.Services.Concrete
{
    public class ProductService : IProductService
    {
        public ProductService()
        {
            if (SeedData.Products == default)
                SeedData.Products = new List<IProductModel>();
        }

        public async Task<bool> Add(ProductModel model)
        {
            try
            {
                if (model.Id == 0)
                    throw new CustomException(500, "Please Give Product Id Bigger Than 0");
                if (SeedData.Products.Any(x => x.Id == model.Id))
                    throw new CustomException(500, "This ProductId Already Added");

                SeedData.Products.Add(model);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new CustomException(500, ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var model = await Get(id);
                if (model == null)
                    throw new CustomException(404, id + " Product Not Found");
                SeedData.Products.Remove(model);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new CustomException(500, ex.Message);
            }
        }

        public async Task<ProductModel> Get(int id)
        {
            try
            {
                var model = SeedData.Products.FirstOrDefault(x => x.Id == id);
                return await Task.FromResult((ProductModel)model);
            }
            catch (Exception ex)
            {
                throw new CustomException(500, ex.Message);
            }
        }

        public async Task<ICollection<ProductModel>> GetAll()
        {
            try
            {
                var result = SeedData.Products.ToList();
                return result.Cast<ProductModel>().ToList();
            }
            catch (Exception ex)
            {
                throw new CustomException(500, ex.Message);
            }
        }

        public async Task<bool> Update(ProductModel model)
        {
            try
            {
                var removed = await Get(model.Id);
                if (model == null)
                    throw new CustomException(404, model.Id + " Product Not Found");
                await Delete(removed.Id);
                await Add(model);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new CustomException(500, ex.Message);
            }
        }
    }
}
