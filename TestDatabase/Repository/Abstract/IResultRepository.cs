using TestDatabase.Entities;

namespace TestDatabase.Repository.Abstract
{
    public interface IResultRepository : IRepository<Result>
    {
        Result GetLastResultByCodeId(int id);
    }
}