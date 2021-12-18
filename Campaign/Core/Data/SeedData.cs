using Campaign.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign.Core.Data
{
    public static class SeedData
    {
        public static List<IProductModel> Products { get; set; }
        public static List<IUserModel> Users { get; set; }
        public static List<IInvoicesModel> Invoices { get; set; }
    }
}
