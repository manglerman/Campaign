using Campaign.Core.Models.Abstract;
using System;

namespace Campaign.Core.Services
{
    public interface IUserModel : ICoreModel, IEntity
    {
        bool IsAffiliate { get; set; }
        bool IsWorker { get; set; }
        DateTime RegisterDate { get; set; }
    }
}
