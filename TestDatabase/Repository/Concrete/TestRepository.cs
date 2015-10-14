using System.Linq;
using TestDatabase.Entities;
using TestDatabase.Repository.Abstract;

namespace TestDatabase.Repository.Concrete
{
    public class TestRepository : Repository<Test>, ITestRepository
    {
        public override bool Edit(int id, Test value)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Test> GetTestsByTaskId(int taskId)
        {
            return GetAll().Where(t => t.TaskId == taskId);
        }
    }
}