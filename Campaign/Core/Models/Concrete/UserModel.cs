using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Campaign.Core.Services
{
    public class UserModel : CoreModel, IUserModel
    {
        public bool IsAffiliate { get; set; }
        public bool IsWorker { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
