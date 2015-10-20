using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using MvcTestSystem.Unity;
using TestDatabase.Entities;
using TestDatabase.Repository.Abstract;

namespace MvcTestSystem.UsersInfoAccess
{
    public static class UsersInfo
    {
        private static readonly IAllCodeRepository CodeRepository = MvcUnityContainer.Container.Resolve<IAllCodeRepository>();
        private static readonly IResultRepository ResultRepository = MvcUnityContainer.Container.Resolve<IResultRepository>();
        private static readonly ITaskRepository TaskRepository = MvcUnityContainer.Container.Resolve<ITaskRepository>();

        public static IEnumerable<Task> GetSolvedTasks(this User user)
        {
            IEnumerable<Code> codes =
                CodeRepository
                    .GetAll()
                    .AsEnumerable()
                    .Where(c => c.UserId == user.Id)
                    .Where(c =>
                    {
                        if (ResultRepository.GetAll().Any(r => r.CodeId == c.Id && r.ResultState == "success"))
                        {
                            return true;
                        }
                        return false;
                    });

            return from code in codes
                   where TaskRepository.GetAll().ToList().Any(t => t.Id == code.TaskId)
                   select TaskRepository.GetAll().ToList().First(t => t.Id == code.TaskId);
        }

        public static IEnumerable<Task> GetTasks(this User user)
        {
            IEnumerable<Code> codes =
                CodeRepository
                    .GetAll()
                    .AsEnumerable()
                    .Where(c => c.UserId == user.Id);

            return from code in codes
                   where TaskRepository.GetAll().ToList().Any(t => t.Id == code.TaskId)
                   select TaskRepository.GetAll().ToList().First(t => t.Id == code.TaskId);
        }
    }
}