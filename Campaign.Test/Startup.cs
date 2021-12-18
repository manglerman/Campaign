using Campaign.Core.Services.Abstract;
using Campaign.Core.Services.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign
{
    public class Startup
    {
        public Startup()
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddTransient(typeof(IProductService), typeof(ProductService));
            services.AddTransient(typeof(IInvoicesService), typeof(InvoicesService));
        }

        public void Configure(IApplicationBuilder app)
        {
            
        }
    }
}
