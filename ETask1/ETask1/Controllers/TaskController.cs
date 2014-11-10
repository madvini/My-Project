using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETask1.Models;
using ETask1.DAL;
using System.IO;
using PagedList;
using System.Net;

namespace ETask1.Controllers
{
    public class TaskController : Controller
    {
        private ITaskRepository taskRepository;
        private IProjectRepository projectRepository;

        /*****************CONSTRUCTOR****************/
        public TaskController()
        {
            this.taskRepository=new TaskRepository(new ETaskContext());
            this.projectRepository = new ProjectRepository(new ETaskContext());
        }
        public TaskController(ITaskRepository tasks)
        {
            this.taskRepository=tasks;
        }

        //
        // GET: /Task/

        public ActionResult Index(string id,int? page)
        {
            var tasks = taskRepository.GetTasks().Where(t=>t.Project.ProjectID==id);
            ViewBag.AssignedTo = taskRepository.GetUsers(id);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.Projid = id;
            return View(tasks.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Task/Details/5

        public ActionResult Details(string id = null)
        {
            Task task = taskRepository.GetTaskByID(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.Watchers = taskRepository.GetWatchersByTask(id);
           
            return View(task);
        }

        //
        // GET: /Task/Create

        public ActionResult Create(string id=null)
        {
           // ViewBag.ProjectID = taskRepository.GetProjects();
            ViewBag.AssignedTo = taskRepository.GetUsers(id);
            Session["prjID"] = id;
            ViewBag.ProjectName = projectRepository.GetProjectByID(id).ProjectName;
           
            return View();
        }

        //
        // POST: /Task/Create

        [HttpPost]
        public ActionResult Create(Task task, HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    
                    var path = Path.Combine(Server.MapPath("~/Content/Uploads"), file.FileName);
                    file.SaveAs(path);
                    task.File = fileName;
                }
            }
            
            if (ModelState.IsValid)
            {
                task.ProjectID = Session["prjID"].ToString();
                taskRepository.InsertTask(task);
                taskRepository.Save();

                if (Request.Form["chkWatchers"] != null)
                {
                    string[] arr = Request["chkWatchers"].Split(',');
                    for (int i = 0; i < arr.Count(); i++)
                    {
                        Watcher watch = new Watcher();
                        watch.UserID = arr[i];
                        watch.TaskID = task.TaskID;
                        taskRepository.AssignWatcherstoTask(watch);
                        //ViewBag.WatcherName = taskRepository.GetWatchers(task);
                        taskRepository.Save();
                    }
                }
            }
            return RedirectToAction("ManagerProjectDetails","Project");
           
        }

        //
        // GET: /Task/Edit/5

        public ActionResult Edit(string id = null)
        {
            Task task = taskRepository.GetTaskByID(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectID = taskRepository.GetProjects(task);
            ViewBag.UserID = taskRepository.GetUsers(task);
            return View(task);
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        public ActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                taskRepository.UpdateTask(task);
                taskRepository.Save();
                return RedirectToAction("Index", "Task", new {id=task.ProjectID });
            }
            ViewBag.ProjectID = taskRepository.GetProjects(task);
            ViewBag.UserID = taskRepository.GetUsers(task);
            return View(task);
        }

        // Get all manager tasks according to the status 
        public JsonResult SearchTasksByStatus(string status,string projectid)
        {
            var tasks = taskRepository.GetTaskByStatus(status,projectid);
            return Json(tasks.ToList(), JsonRequestBehavior.AllowGet);
        }

        // Get all manager tasks according to the status ,assignee and date
        public JsonResult SearchTasksByDate(string status, string projectid, string assignee, DateTime fromdate, DateTime todate)
        {
            var tasks = taskRepository.GetTaskByDate(status, projectid,assignee,fromdate,todate);
            return Json(tasks.ToList(), JsonRequestBehavior.AllowGet);
        }

        //
        // GET:get all tasks of the user who logged in

        public ActionResult UserTasks(int? page)
        {
            User user = (User)Session["user"];
            var tasks = taskRepository.GetTasks().Where(p => p.AssignedTo == user.UserID);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(tasks.ToPagedList(pageNumber, pageSize));
        }


        //
        // GET:to view the details of the selected task 

        public ActionResult TaskDetails(string id = null)
        {
            Task task = taskRepository.GetTaskByID(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.Watchers = taskRepository.GetWatchersByTask(id);
            return View(task);
        }

        //
        // GET:  show  view to update user task

        public ActionResult UpdateTasks(string id = null)
        {
            Task task = taskRepository.GetTaskByID(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectID = taskRepository.GetProjects(task);
            ViewBag.UserID = taskRepository.GetUsers(task);
            return View(task);
        }

        //
        // POST: update user task

        [HttpPost]
        public ActionResult UpdateTasks(Task task)
        {
            if (ModelState.IsValid)
            {
                taskRepository.UpdateTask(task);
                taskRepository.Save();
                return RedirectToAction("UserTasks", "Task", new { id = task.TaskID });
            }

                
                return View(task);
            
        }

        // Get all user tasks of the user who logged in according to the status  
        public JsonResult SearchUserTasksByStatus(string status)
        {
            User user = (User)Session["user"];
            var tasks = taskRepository.GetUserTaskByStatus(user.UserID,status);
            return Json(tasks.ToList(), JsonRequestBehavior.AllowGet);
        }

        // Get all user tasks of the user who logged in according to the status and date
        public JsonResult SearchUserTasksByDate(string status, string projectid, string assignee, DateTime fromdate, DateTime todate)
        {
            User user = (User)Session["user"];
            var tasks = taskRepository.GetUserTaskByDate(user.UserID,status, fromdate, todate);
            return Json(tasks.ToList(), JsonRequestBehavior.AllowGet);
        }

        public void DownloadDoc(string id)
        {
            Task task = taskRepository.GetTaskByID(id);
            string strURL = Server.MapPath( "~/Content/Uploads/")+task.File;
            WebClient req = new WebClient();
            HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + strURL + "\"");
            byte[] data = req.DownloadData(strURL);
            response.BinaryWrite(data);
            response.End();
        }
        protected override void Dispose(bool disposing)
        {
            taskRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}