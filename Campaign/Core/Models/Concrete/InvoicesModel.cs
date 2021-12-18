using Campaign.Core.Models.Abstract;
using Campaign.Core.Models.Enums;
using Campaign.Core.ValueObjescts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign.Core.Services
{
    public class InvoicesModel : IInvoicesModel, IEntity
    {
        public InvoicesModel()
        {
            InvoiceLines = new List<InvoiceLine>();
        }
        public int Id { get; set; }
        public List<InvoiceLine> InvoiceLines { get; }
        public int UserId { get; set; }
        public UserModel User
        {
            get
            {
                return (UserModel)Data.SeedData.Users.FirstOrDefault(x => x.Id == UserId);
            }
        }
        public decimal SubTotal
        {
            get
            {
                return InvoiceLines.Sum(x => x.Product.Price * x.Quantity);
            }
        }
        private string _discountDescription { get; set; }
        public string DiscountDescription { get { return _discountDescription; } }
        public decimal Discount
        {
            get
            {
                var productList = InvoiceLines.Select(x => x.Product);
                decimal discount = 0;
                decimal price = 0;
                _discountDescription += "User is " + System.Environment.NewLine + JsonConvert.SerializeObject(User, Formatting.Indented) + System.Environment.NewLine;
                _discountDescription += System.Environment.NewLine + "Invoice Products are " + System.Environment.NewLine + JsonConvert.SerializeObject(InvoiceLines, Formatting.Indented) + System.Environment.NewLine + System.Environment.NewLine;
                if (User.IsWorker)
                {
                    price = productList.
                        Where(x => x.ProductCategory != ProductCategory.Groceries).
                        Sum(x => x.Price);
                    discount = price * (decimal)0.3;
                    _discountDescription += "User is Worker=> 30% Discount (Without Groceries)" + System.Environment.NewLine;
                    _discountDescription += price + " * 0.3 =" + discount + System.Environment.NewLine;
                }
                else if (User.IsAffiliate)
                {
                    price = productList.Where(x => x.ProductCategory != ProductCategory.Groceries).Sum(x => x.Price);
                    _discountDescription += "User is IsAffiliate=> 10% Discount (Without Groceries)" + System.Environment.NewLine;
                    discount = price * (decimal)0.1;
                    _discountDescription += price + " * 0.1 =" + discount + System.Environment.NewLine;
                }
                else if (User.RegisterDate > DateTime.Now.AddYears(-2))
                {
                    price = productList.Where(x => x.ProductCategory != ProductCategory.Groceries).Sum(x => x.Price);
                    _discountDescription += "User is Older than 2 years=> 5% Discount (Without Groceries)" + System.Environment.NewLine;
                    discount = price * (decimal)0.05;
                    _discountDescription += price + " * 0.05 =" + discount + System.Environment.NewLine;
                }
                //A user can get multiple discounts (percentage base and amount base)
                //because of this i did not use ELSE
                if (SubTotal - discount > 100)
                {
                    _discountDescription += "For every $100 on the bill, there would be a $ 5 discount" + System.Environment.NewLine;
                    int amountBaseDiscount = ((int)(SubTotal - discount) / 100) * 5;
                    _discountDescription += (int)(SubTotal - discount) + " /100*5 =" + amountBaseDiscount + System.Environment.NewLine;
                    discount = discount + amountBaseDiscount;
                }
                _discountDescription += "Total Discount =" + discount + System.Environment.NewLine; ;
                return discount;
            }
        }

        public decimal Total
        {
            get
            {
                return SubTotal - Discount;
            }
        }
    }
}
