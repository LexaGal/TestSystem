using System.Linq;
using TestDatabase.Entities;

namespace TestDatabase.Repository.Abstract
{
    public interface ITestRepository : IRepository<Test>
    {
        IQueryable<Test> GeTestsByTaskId(int taskId);
    }
}