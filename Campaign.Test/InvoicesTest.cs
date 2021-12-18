using Campaign.Core.Data;
using Campaign.Core.Models.Enums;
using Campaign.Core.Models.RequestModel;
using Campaign.Core.Services;
using Campaign.Core.Services.Abstract;
using Campaign.Core.ValueObjescts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign.Test
{
    [TestClass]
    public class InvoicesTest
    {
        private readonly IInvoicesService _invoicesService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        private InvoicesModel _invoicesModel;
        private UserModel _userModel;
        private ProductModel _productModel;
        public InvoicesTest()
        {
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            _invoicesService = webHost.Services.GetService<IInvoicesService>();
            _productService = webHost.Services.GetService<IProductService>();
            _userService = webHost.Services.GetService<IUserService>();

            _invoicesModel = new InvoicesModel()
            {
                Id = int.MaxValue
            };

            _userModel = new UserModel()
            {
                Id = int.MaxValue,
                IsAffiliate = true,
                IsWorker = true,
                Name = "Test User",
                RegisterDate = DateTime.Now.AddYears(-2)
            };

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
            await _invoicesService.Add(_invoicesModel);
            var model = await _invoicesService.Get(_invoicesModel.Id);
            if (model == default) Assert.IsTrue(false);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task Update()
        {
            await Add();
            await _productService.Add(_productModel);
            await _userService.Add(_userModel);
            await _invoicesService.AddProduct(
                new AddProductToInvoiceRequestModel()
                {
                    InvoiceId = int.MaxValue,
                    ProductId = int.MaxValue
                });
            await _invoicesService.Update(_invoicesModel);
            InvoicesModel _testInvoice = await _invoicesService.Get(_invoicesModel.Id);
            Assert.IsTrue(_testInvoice != default);
            Assert.IsTrue(_testInvoice.InvoiceLines.Count() == 1);

            await _invoicesService.RemoveProduct(
                new RemoveProductFromInvoiceRequestModel()
                {
                    InvoiceId = int.MaxValue,
                    ProductId = int.MaxValue
                });
            _testInvoice = await _invoicesService.Get(_invoicesModel.Id);
            Assert.IsTrue(_testInvoice.InvoiceLines.Count() == 0);
        }


        [TestMethod]
        public async Task Delete()
        {
            await Add();
            await _invoicesService.Delete(_invoicesModel.Id);
            var model = await _invoicesService.Get(_invoicesModel.Id);
            Assert.IsTrue(model == default);
        }
    }
}
