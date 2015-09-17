using MvcTestSystem.Models;
using MvcTestSystem.Repository.Abstract;

namespace MvcTestSystem.Repository.Concrete
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public override bool Edit(int id, User value)
        {
            throw new System.NotImplementedException();
        }
    }
}