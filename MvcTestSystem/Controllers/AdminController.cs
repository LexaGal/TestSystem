using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcTestSystem.Authentication;
using MvcTestSystem.UsersInfoAccess;
using TestDatabase.Entities;
using TestDatabase.Repository.Abstract;

namespace MvcTestSystem.Controllers
{
    [Auth]
    public class AdminController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;

        public AdminController(ITaskRepository taskRepository, IUserRepository userRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        public ActionResult Index()
        {
            IList<User> users = _userRepository.GetAll().AsEnumerable().ToList();

            return View(users.Where(u => u.Role == Role.User.ToString())
                .OrderByDescending(u => u.GetSolvedTasks().Sum(t => t.Price)));
        }

        public ActionResult TaskDetails(int id)
        {
            return View(_taskRepository.Get(id));
        }

        public ActionResult CreateTask(int id = 0)
        {
            Task task = _taskRepository.Get(id) ?? new Task();
            return View(task);
        }

        [HttpPost]
        public ActionResult CreateTask(Task task)
        {
            _taskRepository.Add(task);
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult EditTask(int id)
        {
            return View("CreateTask", _taskRepository.Get(id));
        }


        [HttpPost]
        public ActionResult EditTask(Task task)
        {
            _taskRepository.Edit(task.Id, task);
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult DeleteTask(int id)
        {
            return View(_taskRepository.Get(id));
        }

        [HttpPost, ActionName("DeleteTask")]
        public ActionResult DeletedTask(int id)
        {
            _taskRepository.Delete(id);
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult UserDetails(int id)
        {
            return View(_userRepository.Get(id));
        }

        public ActionResult CreateUser()
        {
            return RedirectToAction("Register", "Account", new {returnUrl = Url.Action("Index", "Admin") });
        }

        public ActionResult DeleteUser(int id)
        {
            return View(_userRepository.Get(id));
        }

        [HttpPost, ActionName("DeleteUser")]
        public ActionResult DeletedUser(int id)
        {
            _userRepository.Delete(id);
            return RedirectToAction("Index", "Admin");
        }
    }
}
