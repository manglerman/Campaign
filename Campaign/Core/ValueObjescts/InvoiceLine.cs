using Campaign.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign.Core.ValueObjescts
{
    public class InvoiceLine
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [NotMapped]
        public ProductModel Product { get; set; }
    }
}
