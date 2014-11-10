using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETask1.Models;
using ETask1.DAL;
using PagedList;
using PagedList.Mvc;

namespace ETask1.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository userRepository;
        private IDepartmentRepository departmentRepository;
        private ITaskRepository taskRepository;

        /*****************CONSTRUCTOR****************/
        public UserController()
        {
            this.userRepository=new UserRepository(new ETaskContext());
            this.departmentRepository = new DepartmentRepository(new ETaskContext());
            this.taskRepository = new TaskRepository(new ETaskContext());
        }
        public UserController(IUserRepository users)
        {
            this.userRepository=users;
        }

        // GET: Show view to logon
        public ActionResult Login()
        {
            return View();
        }

        // POST: Log on
        [HttpPost]
        public ActionResult Login(User user)
        {
            var query = userRepository.VerifyUser(user);

            if (query != null)
            {
                Session["user"] = query;
                if (query.UserType == "admin")
                {
                    return RedirectToAction("Index", "User");
                }
                else
                    if (query.UserType == "manager")
                    {
                        return RedirectToAction("ManagerProjectDetails", "Project");
                    }
                    else
                        if (query.UserType == "employee")
                        {
                            return RedirectToAction("UserTasks", "Task");
                        }
            }
            else
               if((user.UserName !=null)&&(user.Password!=null))
            {
                ViewBag.message = "Invalid Username/Password";
            }
            return View();
        }
    

        //
        // GET: /User/

        public ActionResult Index(int? page)
        {
            var users = userRepository.GetUsers();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.DepartmentID = userRepository.GetDepartment();
            return View(users.ToPagedList(pageNumber, pageSize));
        }
        public JsonResult SearchUsersByDepartment(string id = null)
        {
            var users = userRepository.GetUsersByDepartment(id);
            return Json(users.ToList(), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(string id = null)
        {
            User user = userRepository.GetUserByID(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            ViewBag.DepartmentID = userRepository.GetDepartment();
            ViewBag.DesignationID = userRepository.GetDesignation();
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            
            if (ModelState.IsValid)
            {
                userRepository.InsertUser(user);
                userRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = userRepository.GetDepartment(user);
            ViewBag.DesignationID = userRepository.GetDesignation(user);
            return View(user);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(string id = null)
        {
            User user = userRepository.GetUserByID(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = userRepository.GetDepartment(user);
            ViewBag.DesignationID = userRepository.GetDesignation(user);
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                userRepository.UpdateUser(user);
                userRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = userRepository.GetDepartment(user);
            ViewBag.DesignationID = userRepository.GetDesignation(user);
            return View(user);
        }

        //
        // GET: /User/MyProfile/5

        public ActionResult MyProfile()
        {
            User user = (User)Session["user"];
            user.Department = departmentRepository.GetDepartmentByID(user.DepartmentID);
            return View(user);
        }

        //
        // POST: /User/MyProfile
        [HttpPost]
        public ActionResult MyProfile(User user)
        {

            if (Request["txtNewPassword"] != "")
            {
                string confirm = Request["txtNewPassword"];
                user.ConfirmPassword = confirm;
                user.Password = user.ConfirmPassword;
            }
            else
            {
                user.ConfirmPassword = user.Password;

            }

                    userRepository.UpdateUser(user);
                    userRepository.Save();
                    Session["user"] = user;
                    return RedirectToAction("MyProfile");

               
            
        }

        public JsonResult CheckPassword(string pswd)
        {
            User user = (User)Session["user"];
            var query = userRepository.CheckPassword(pswd, user.UserID);

            if (query != null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
        protected override void Dispose(bool disposing)
        {
            userRepository.Dispose();
            base.Dispose(disposing);
        }
       
    }
}