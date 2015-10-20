using TestDatabase.Entities;
using TestDatabase.Repository.Abstract;

namespace TestDatabase.Repository.Concrete
{
    public class AllCodeRepository : Repository<Code>, IAllCodeRepository
    {
        public override bool Edit(int id, Code value)
        {
            throw new System.NotImplementedException();
        }
    }
}