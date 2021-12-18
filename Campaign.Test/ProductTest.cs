using Campaign.Core.Data;
using Campaign.Core.Models.Enums;
using Campaign.Core.Services;
using Campaign.Core.Services.Abstract;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign.Test
{
    [TestClass]
    public class ProductTest
    {
        private readonly IProductService _productService;
        private ProductModel _productModel;
        public ProductTest()
        {
            var webHost = WebHost.CreateDefaultBuilder()
                     .UseStartup<Startup>()
                     .Build();

            _productService = webHost.Services.GetService<IProductService>();
            _productModel = new ProductModel()
            {
                Id = int.MaxValue,
                Name = "Test Product",
                Price = 100,
                ProductCategory = ProductCategory.Accessory
            };
        }
        
        [TestMethod]
        public async Task Add()
        {
            await _productService.Add(_productModel);
            var model = await _productService.Get(_productModel.Id);
            if (model == default) Assert.IsTrue(false);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task Update()
        {
            await Add();
            _productModel.Price = 500;
            await _productService.Update(_productModel);
            var model = await _productService.Get(_productModel.Id);
            Assert.IsTrue(model != default);
            Assert.IsTrue(model.Price == 500);
        }


        [TestMethod]
        public async Task Delete()
        {
            await Add();
            await _productService.Delete(_productModel.Id);
            var model = await _productService.Get(_productModel.Id);
            Assert.IsTrue(model == default);
        }
    }
}
