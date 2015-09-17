using MvcTestSystem.Models;
using MvcTestSystem.Repository.Abstract;

namespace MvcTestSystem.Repository.Concrete
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public override bool Edit(int id, Task value)
        {
            throw new System.NotImplementedException();
        }
    }
}