using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Campaign.Core.Models.Abstract;

namespace TuborgBurada.Core.Interfaces
{
    public interface ICoreService<T> where T : IEntity
    {
        Task<ICollection<T>> GetAll();
        Task<T> Get(int id);
        Task<bool> Add(T model);
        Task<bool> Delete(int id);
        Task<bool> Update(T model);

    }
}
