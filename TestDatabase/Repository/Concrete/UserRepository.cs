using TestDatabase.Entities;
using TestDatabase.Repository.Abstract;

namespace TestDatabase.Repository.Concrete
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public override bool Edit(int id, User value)
        {
            throw new System.NotImplementedException();
        }
    }
}