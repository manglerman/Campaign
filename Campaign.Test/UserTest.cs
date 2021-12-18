using Campaign.Core.Data;
using Campaign.Core.Models.Enums;
using Campaign.Core.Services;
using Campaign.Core.Services.Abstract;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign.Test
{
    [TestClass]
    public class UserTest
    {
        private readonly IUserService _userService;
        private UserModel _userModel;
        public UserTest()
        {
            var webHost = WebHost.CreateDefaultBuilder()
                     .UseStartup<Startup>()
                     .Build();

            _userService = webHost.Services.GetService<IUserService>();
            _userModel = new UserModel()
            {
                Id = int.MaxValue,
                Name = "Test User",
                IsAffiliate = true,
                IsWorker = true,
                RegisterDate = System.DateTime.Now.AddYears(-3)
            };
        }
        
        [TestMethod]
        public async Task Add()
        {
            await _userService.Add(_userModel);
            var model = await _userService.Get(_userModel.Id);
            if (model == default) Assert.IsTrue(false);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task Update()
        {
            await Add();
            _userModel.Name = "Example User";
            await _userService.Update(_userModel);
            var model = await _userService.Get(_userModel.Id);
            Assert.IsTrue(model != default);
            Assert.IsTrue(_userModel.Name == "Example User");
        }


        [TestMethod]
        public async Task Delete()
        {
            await Add();
            await _userService.Delete(_userModel.Id);
            var model = await _userService.Get(_userModel.Id);
            Assert.IsTrue(model == default);
        }
    }
}
