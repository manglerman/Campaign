using Campaign.Core.ValueObjescts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign.Core.Services
{
    public interface IInvoicesModel
    {
        public int Id { get; set; }
        List<InvoiceLine> InvoiceLines { get; }
        int UserId { get; set; }
        UserModel User { get; }
        public decimal SubTotal { get; }
        public decimal Discount { get; }
        public string DiscountDescription { get; }
        public decimal Total { get; }
    }
}
