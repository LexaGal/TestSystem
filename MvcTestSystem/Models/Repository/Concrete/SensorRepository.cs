using MvcTestSystem.Models;
using MvcTestSystem.Repository.Abstract;

namespace MvcTestSystem.Repository.Concrete
{
    public class ResultRepository : Repository<Result>, IResultRepository
    {
        public override bool Edit(int id, Result value)
        {
            throw new System.NotImplementedException();
        }
    }
}