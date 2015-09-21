using TestDatabase.Entities;
using TestDatabase.Repository.Abstract;

namespace TestDatabase.Repository.Concrete
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public override bool Edit(int id, Task value)
        {
            throw new System.NotImplementedException();
        }
    }
}