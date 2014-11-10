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

namespace ETask1.Controllers
{
    public class ProjectController : Controller
    {
        private IProjectRepository projectRepository;

        /*****************CONSTRUCTOR****************/
        public ProjectController()
        {
            this.projectRepository=new ProjectRepository(new ETaskContext());
        }
        public ProjectController(IProjectRepository projects)
        {
            this.projectRepository=projects;
        }

        //
        // GET: /Project/

        public ActionResult Index(int? page)
        {
            var projects = projectRepository.GetProjects();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(projects.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Project/Details/5

        public ActionResult Details(string id = null)
        {
            Project project = projectRepository.GetProjectByID(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        //
        // GET: /Project/Create

        public ActionResult Create()
        {
            ViewBag.UserID = projectRepository.GetManagerByProject(); 
            return View();
        }

        //
        // POST: /Project/Create

        [HttpPost]
        public ActionResult Create(Project project)
        {
           
            if (ModelState.IsValid)
            {
                projectRepository.InsertProject(project);
                projectRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = projectRepository.GetManagerByProject(project); 
            return View(project);
        }

        //
        // GET: /Project/Edit/5

        public ActionResult Edit(string id = null)
        {
            Project project = projectRepository.GetProjectByID(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectManager = projectRepository.GetManagerByProject(project);
            return View(project);
        }

        //
        // POST: /Project/Edit/5

        [HttpPost]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                projectRepository.UpdateProject(project);
                projectRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectManager = projectRepository.GetManagerByProject(project);
            return View(project);
        }
        //
        // GET: /Project/ManagerProjectDetails/5

        public ActionResult ManagerProjectDetails(int? page)
        {
            User user = (User)Session["user"];
            var projects = projectRepository.GetProjects().Where(p => p.UserID == user.UserID);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(projects.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Project/ManagerEditProject/5

        public ActionResult ManagerEditProject(string id = null)
        {
            Project project = projectRepository.GetProjectByID(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectManager = projectRepository.GetManagerByProject(project);
            return View(project);
        }

        //
        // POST: /Project/ManagerEditProject/5

        [HttpPost]
        public ActionResult ManagerEditProject(Project project)
        {
            if (ModelState.IsValid)
            {
                projectRepository.UpdateProject(project);
                projectRepository.Save();
                return RedirectToAction("ManagerProjectDetails");
            }
            ViewBag.ProjectManager = projectRepository.GetManagerByProject(project);
            return View(project);
        }
        // Get 
      //To add Users to projects
        public ActionResult AddUserProject(string id)
        {
            var dept = projectRepository.GetProjectByID(id).ProjectManager.DepartmentID;
            var users = projectRepository.GetUsersByDepartment(dept, id);
            Session["prjID"] = id;
            return View(users.ToList());
           
        }

        // Post 
        //To add Users to projects
        [HttpPost]
        public ActionResult AddUserProject(UserProjectDetail projectdetails)
        {

            string[] arr = Request["chkUser"].Split(',');
            for (int i = 0; i < arr.Count(); i++)
            {
                projectdetails = new UserProjectDetail();
                projectdetails.UserID = arr[i];
                projectdetails.ProjectID = Session["prjID"].ToString();
                projectRepository.AssignUserstoProject(projectdetails);
                projectRepository.Save();
            }
            return RedirectToAction("Index", "Project");
        }
        protected override void Dispose(bool disposing)
        {
            projectRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}