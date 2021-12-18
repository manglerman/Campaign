using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Campaign.Core.Services.Abstract;
using Campaign.Core.Data;
using System.Linq;
using Campaign.Core.Exceptions;

namespace Campaign.Core.Services.Concrete
{
    public class UserService : IUserService
    {
        public UserService()
        {
            if (SeedData.Users == default)
                SeedData.Users = new List<IUserModel>();
        }

        public async Task<bool> Add(UserModel model)
        {
            try
            {
                if (model.Id == 0)
                    throw new CustomException(500, "Please Give User Id Bigger Than 0");
                if (SeedData.Users.Any(x => x.Id == model.Id)) 
                    throw new CustomException(500, "This User Id Already Added");

                SeedData.Users.Add(model);
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
                    throw new CustomException(404, id + " User Not Found");
                SeedData.Users.Remove(model);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new CustomException(500, ex.Message);
            }
        }

        public async Task<UserModel> Get(int id)
        {
            try
            {
                var model = SeedData.Users.FirstOrDefault(x => x.Id == id);
                return await Task.FromResult((UserModel)model);
            }
            catch (Exception ex)
            {
                throw new CustomException(500, ex.Message);
            }
        }

        public async Task<ICollection<UserModel>> GetAll()
        {
            try
            {
                var result = SeedData.Users.ToList();
                return result.Cast<UserModel>().ToList();
            }
            catch (Exception ex)
            {
                throw new CustomException(500, ex.Message);
            }
        }

        public async Task<bool> Update(UserModel model)
        {
            try
            {
                var removed = await Get(model.Id);
                if (model == null)
                    throw new CustomException(404, model.Id + " User Not Found");
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
