using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign.Core.Models.RequestModel
{
    public class AddProductToInvoiceRequestModel
    {
        public int ProductId { get; set; }
        public int InvoiceId { get; set; }
    }
}
