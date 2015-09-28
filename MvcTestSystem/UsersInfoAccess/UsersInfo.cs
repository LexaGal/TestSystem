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
        private static readonly ICodeRepository CodeRepository = MvcUnityContainer.Container.Resolve<ICodeRepository>();
        private static readonly IResultRepository ResultRepository = MvcUnityContainer.Container.Resolve<IResultRepository>();

        public static IEnumerable<Task> GetUserSolvedTasks(int id, IList<Task> tasks)
        {
            IEnumerable<Code> codes =
                CodeRepository
                    .GetAll()
                    .AsEnumerable()
                    .Where(c => c.UserId == id)
                    .Where(c =>
                    {
                        if (ResultRepository.GetAll().Any(r => r.CodeId == c.Id && r.ResultState == "OK"))
                        {
                            return true;
                        }
                        return false;
                    });

            foreach (var code in codes)
            {
                if (tasks.Any(t => t.Id == code.TaskId))
                {
                    yield return tasks.First(t => t.Id == code.TaskId);
                }
            }
        }

        public static IEnumerable<Task> GetUserTasks(int id, IList<Task> tasks)
        {
            IEnumerable<Code> codes =
                CodeRepository
                    .GetAll()
                    .AsEnumerable()
                    .Where(c => c.UserId == id);

            foreach (var code in codes)
            {
                if (tasks.Any(t => t.Id == code.TaskId))
                {
                    yield return tasks.First(t => t.Id == code.TaskId);
                }
            }
        }
    }
}