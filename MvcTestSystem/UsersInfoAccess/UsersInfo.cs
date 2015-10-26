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
        private static readonly IResultRepository ResultRepository = MvcUnityContainer.Container.Resolve<IResultRepository>();
        private static readonly ITaskRepository TaskRepository = MvcUnityContainer.Container.Resolve<ITaskRepository>();

        public static IEnumerable<Task> GetSolvedTasks(this User user)
        {
            IEnumerable<Result> results =
                ResultRepository
                    .GetAll()
                    .AsEnumerable()
                    .Where(result => result.UserId == user.Id && result.ResultState == "success");

            return from result in results
                   where TaskRepository.GetAll().ToList().Any(t => t.Id == result.TaskId)
                   select TaskRepository.GetAll().ToList().First(t => t.Id == result.TaskId);
        }

        public static IEnumerable<Task> GetTasks(this User user)
        {
            IEnumerable<Result> results =
                ResultRepository
                    .GetAll()
                    .AsEnumerable()
                    .Where(c => c.UserId == user.Id);

            return from result in results
                   where TaskRepository.GetAll().ToList().Any(t => t.Id == result.TaskId)
                   select TaskRepository.GetAll().ToList().First(t => t.Id == result.TaskId);
        }
    }
}