using TestDatabase.Entities;
using TestDatabase.Repository.Abstract;

namespace TestDatabase.Repository.Concrete
{
    public class ResultRepository : Repository<Result>, IResultRepository
    {
        public override bool Edit(int id, Result value)
        {
            throw new System.NotImplementedException();
        }
    }
}