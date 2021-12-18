using Campaign.Core.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuborgBurada.Core.Interfaces;

namespace Campaign.Core.Services.Abstract
{
    public interface IInvoicesService : ICoreService<InvoicesModel>
    {
        Task<bool> AddProduct(AddProductToInvoiceRequestModel model);
        Task<bool> RemoveProduct(RemoveProductFromInvoiceRequestModel model);

    }
}
