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
    public class InvoiceCampaignTest
    {
        private readonly IInvoicesService _invoicesService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        private InvoicesModel _invoicesModel;
        private UserModel _userModel;
        private ProductModel _productModel;
        public InvoiceCampaignTest()
        {
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            _invoicesService = webHost.Services.GetService<IInvoicesService>();
            _productService = webHost.Services.GetService<IProductService>();
            _userService = webHost.Services.GetService<IUserService>();
        }


        [TestMethod]
        public async Task ControlRandomInvoices()
        {
            await AddRandomInvoices();
            var invoiceList = await _invoicesService.GetAll();
            foreach (InvoicesModel item in invoiceList)
            {
                string _discountDescription = item.DiscountDescription;
            }
            Assert.IsTrue(true);
        }


        [TestMethod]
        public async Task AddRandomInvoices()
        {
            await AddRandomProducts();
            await AddRandomUsers();
            Random rnd = new Random();
            for (int i = 1; i <= 20; i++)
            {
                _invoicesService.Add(
                    new InvoicesModel()
                    {
                        Id = i,
                        UserId = i,
                    }
                    ).Wait();

                for (int j = 0; j < 3; j++)
                {
                    _invoicesService.AddProduct
                        (new AddProductToInvoiceRequestModel()
                        {
                            InvoiceId = i,
                            ProductId = rnd.Next(1, 20)
                        }).Wait();
                }
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task AddRandomUsers()
        {
            Random rnd = new Random();
            for (int i = 1; i <= 20; i++)
            {
                _userService.Add(
                    new UserModel()
                    {
                        Id = i,
                        Name = "User " + i.ToString(),
                        IsAffiliate = (rnd.Next(0, 10) % 2 == 0),
                        IsWorker = (rnd.Next(0, 10) % 2 == 0),
                        RegisterDate = DateTime.Now.AddMonths(-1 * rnd.Next(12, 24))
                    }
                    ).Wait();
            }
            Assert.IsTrue(true);
        }


        [TestMethod]
        public async Task AddRandomProducts()
        {
            Random rnd = new Random();
            for (int i = 1; i <= 20; i++)
            {
                _productService.Add(
                    new ProductModel()
                    {
                        Id = i,
                        Name = "Product " + i.ToString(),
                        Price = rnd.Next(1, 100) * 10,
                        ProductCategory = (ProductCategory)rnd.Next(0, Enum.GetNames(typeof(ProductCategory)).Length - 1)
                    }
                    ).Wait();
            }
            Assert.IsTrue(true);
        }


    }
}
