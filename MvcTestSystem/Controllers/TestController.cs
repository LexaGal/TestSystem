using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcTestSystem.UsersInfoAccess;
using TestDatabase.Entities;
using TestDatabase.Repository.Abstract;

namespace MvcTestSystem.Controllers
{
    public class TestController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICodeRepository _codeRepository;
        private readonly IResultRepository _resultRepository;
        private readonly IUserRepository _userRepository;

        public TestController(ITaskRepository taskRepository, ICodeRepository codeRepository,
            IResultRepository resultRepository, IUserRepository userRepository)
        {
            _taskRepository = taskRepository;
            _codeRepository = codeRepository;
            _resultRepository = resultRepository;
            _userRepository = userRepository;
        }
        
        private string SendToServer(string codeText)
        {
            return "Ok";
        }

        public ActionResult Index()
        {
            IList<Task> tasks = _taskRepository.GetAll().AsEnumerable().ToList();
           
            ((User)Session["user"]).SolvedTasks = 
                UsersInfo.GetUserSolvedTasks(((User) Session["user"]).Id, tasks).ToList();
            
            ((User)Session["user"]).Tasks = 
                UsersInfo.GetUserTasks(((User)Session["user"]).Id, tasks).ToList();

            return View(_taskRepository.GetAll().AsEnumerable().OrderByDescending(t => t.Price));
        }

        public ActionResult ChosenTask(int id)
        {
            return View(_taskRepository.Get(id));
        }

        [HttpPost]
        public string ChosenTask(int taskId, string codeText)
        {
            User user = ((User) Session["user"]);
            
            Task task = _taskRepository.Get(taskId);

            user.Tasks.Add(task);

            Code code = new Code(user.Id, taskId, codeText);
            _codeRepository.Add(code);
            
            string taskResult = SendToServer(codeText);
            Result result = new Result(code.Id, taskResult);
            _resultRepository.Add(result);

            if (taskResult == "Ok")
            {
                user.SolvedTasks.Add(task);
            }

            return taskResult;
        }
        
        public ActionResult Statistics()
        {
            IList<Task> tasks = _taskRepository.GetAll().AsEnumerable().ToList();
            IList<User> users = _userRepository.GetAll().AsEnumerable().ToList();
            
            foreach (var u in users)
            {
                u.SolvedTasks = UsersInfo.GetUserSolvedTasks(u.Id, tasks).ToList();
            }
            
            return View(users.Where(u => u.Role == Role.User.ToString())
                .OrderByDescending(u => u.SolvedTasks.Sum(t => t.Price)));
        }
        
    }
}
