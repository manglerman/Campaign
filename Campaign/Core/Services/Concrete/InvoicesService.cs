using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Campaign.Core.Services.Abstract;
using Campaign.Core.Data;
using System.Linq;
using Campaign.Core.Exceptions;
using Campaign.Core.Models.RequestModel;
using Campaign.Core.ValueObjescts;

namespace Campaign.Core.Services.Concrete
{
    public class InvoicesService : IInvoicesService
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        public InvoicesService(IProductService productService, IUserService userService)
        {
            if (SeedData.Invoices == default)
                SeedData.Invoices = new List<IInvoicesModel>();

            _productService = productService;
            _userService = userService;
        }

        public async Task<bool> Add(InvoicesModel model)
        {
            try
            {
                if (model.Id == 0)
                    throw new CustomException(500, "Please Give Invoice Id Bigger Than 0");
                var controlModel = await Get(model.Id);
                if (controlModel != default)
                    throw new CustomException(500, "This InvoiceId Already Added");
                SeedData.Invoices.Add(model);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new CustomException(500, ex.Message);
            }
        }
        public async Task<bool> AddProduct(AddProductToInvoiceRequestModel requestModel)
        {
            try
            {
                var productModel = await _productService.Get(requestModel.ProductId);
                if (productModel == null)
                    throw new CustomException(404, requestModel.ProductId + " Product Not Found");

                var invoiceModel = await Get(requestModel.InvoiceId);
                if (invoiceModel == null)
                    throw new CustomException(404, requestModel.InvoiceId + " Invoice Not Found");

                if (invoiceModel.InvoiceLines.Any(x => x.ProductId == productModel.Id))
                {
                    var invoiceUpdate = invoiceModel.InvoiceLines.FirstOrDefault(x => x.ProductId == productModel.Id);
                    invoiceUpdate.Quantity++;
                }
                else
                {
                    invoiceModel.InvoiceLines.Add(
                        new InvoiceLine()
                        {
                            Product = productModel,
                            ProductId = productModel.Id,
                            Quantity = 1
                        });
                }
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new CustomException(500, ex.Message);
            }
        }

        public async Task<bool> RemoveProduct(RemoveProductFromInvoiceRequestModel requestModel)
        {
            try
            {
                var productModel = await _productService.Get(requestModel.ProductId);
                if (productModel == null)
                    throw new CustomException(404, requestModel.ProductId + " Product Not Found");

                var invoiceModel = await Get(requestModel.InvoiceId);
                if (invoiceModel == null)
                    throw new CustomException(404, requestModel.InvoiceId + " Invoice Not Found");

                if (invoiceModel.InvoiceLines.Any(x => x.ProductId == productModel.Id))
                {
                    var invoiceRemove = invoiceModel.InvoiceLines.FirstOrDefault(x => x.ProductId == productModel.Id);
                    invoiceModel.InvoiceLines.Remove(invoiceRemove);
                }
                else
                {
                    throw new CustomException(404, requestModel.InvoiceId + " Invoice Line Not Found");
                }
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
                    throw new CustomException(404, id + " Invoice Not Found");
                SeedData.Invoices.Remove(model);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new CustomException(500, ex.Message);
            }
        }

        public async Task<InvoicesModel> Get(int id)
        {
            try
            {
                var model = SeedData.Invoices.FirstOrDefault(x => x.Id == id);
                return await Task.FromResult((InvoicesModel)model);
            }
            catch (Exception ex)
            {
                throw new CustomException(500, ex.Message);
            }
        }

        public async Task<ICollection<InvoicesModel>> GetAll()
        {
            try
            {
                var result = SeedData.Invoices.ToList();
                return result.Cast<InvoicesModel>().ToList();
            }
            catch (Exception ex)
            {
                throw new CustomException(500, ex.Message);
            }
        }

        public async Task<bool> Update(InvoicesModel model)
        {
            try
            {
                var removed = await Get(model.Id);
                if (model == null)
                    throw new CustomException(404, model.Id + " Invoice Not Found");
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
