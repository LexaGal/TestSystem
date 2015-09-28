using Microsoft.Practices.Unity;
using MvcTestSystem.Authentication;
using TestDatabase.Repository.Abstract;
using TestDatabase.Repository.Concrete;

namespace MvcTestSystem.Unity
{
    public static class MvcUnityContainer
    {
        public static void Initialize()
        {
            Container = new UnityContainer();
            Container.RegisterType<ITaskRepository, TaskRepository>();
            Container.RegisterType<ICodeRepository, CodeRepository>();
            Container.RegisterType<IResultRepository, ResultRepository>();
            Container.RegisterType<IUserRepository, UserRepository>();
            Container.RegisterType<ITestRepository, TestRepository>();
            Container.RegisterType<IAuthProvider, AuthProvider>();
            Container.RegisterType<ICryptor, PasswordCryptor>();
        }

        public static IUnityContainer Container { get; set; }
    }
}