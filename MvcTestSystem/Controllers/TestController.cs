using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcTestSystem.Authentication;
using MvcTestSystem.Models;
using MvcTestSystem.UsersInfoAccess;
using TestDatabase.Entities;
using TestDatabase.Repository.Abstract;

namespace MvcTestSystem.Controllers
{
    [Auth]
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
        
        public ActionResult Index()
        {
            return View(_taskRepository.GetAll().AsEnumerable().OrderByDescending(t => t.Price));
        }

        public ActionResult ChosenTask(int id)
        {
            return View(_taskRepository.Get(id));
        }

        [HttpPost]
        public string ChosenTask(CodeViewModel codeViewModel)
        {
            User user = ((User) Session["user"]);

            Code code = new Code(user.Id, Convert.ToInt32(codeViewModel.Id), codeViewModel.Code);
            _codeRepository.Add(code);
            Session["code"] = code;

            return "Testing";
        }

        public string CheckTask()
        {
            Code code = ((Code) Session["code"]);
            //User user = _userRepository.Get(code.UserId);
            //Task task = _taskRepository.Get(code.TaskId);

            //user.Tasks = user.GetTasks().ToList();
            //user.SolvedTasks = user.GetSolvedTasks().ToList();

            Result result = _resultRepository.GetLastResultByCodeId(code.Id);
            if (result != null)
            {
                return result.ResultState;
            }
            return "Testing";
        }

        public ActionResult Statistics()
        {
            //IList<Task> tasks = _taskRepository.GetAll().AsEnumerable().ToList();
            IList<User> users = _userRepository.GetAll().AsEnumerable().ToList();

            //foreach (User user in users)
            //{
            //    user.SolvedTasks = user.GetSolvedTasks().ToList();
            //}

            return View(users.Where(u => u.Role == Role.User.ToString())
                .OrderByDescending(u => u.GetSolvedTasks().Sum(t => t.Price)));
        }
        
    }
}
