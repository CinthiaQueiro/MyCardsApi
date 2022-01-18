using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {

        Task<List<User>> Get();
        Task<User> Post(User user);
    }
}
