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
    public class DepartmentController : Controller
    {
        private IDepartmentRepository departmentRepository;

        /*****************CONSTRUCTOR****************/
        public DepartmentController()
        {
            this.departmentRepository=new DepartmentRepository(new ETaskContext());
        }
         public DepartmentController(IDepartmentRepository departments)
        {
            this.departmentRepository=departments;
        }
        //
        // GET: /Department/

         public ActionResult Index(int? page)
        {
            var departments = departmentRepository.GetDepartments();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(departments.ToPagedList(pageNumber, pageSize));
            
        }

        //
        // GET: /Department/Details/5

        public ActionResult Details(string id = null)
        {
            Department department = departmentRepository.GetDepartmentByID(id); ;
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        //
        // GET: /Department/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Department/Create

        [HttpPost]
        public ActionResult Create(Department department)
        {
           
            if (ModelState.IsValid)
            {
                departmentRepository.InsertDepartment(department);
                departmentRepository.Save();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        //
        // GET: /Department/Edit/5

        public ActionResult Edit(string id = null)
        {
            Department department = departmentRepository.GetDepartmentByID(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        //
        // POST: /Department/Edit/5

        [HttpPost]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                departmentRepository.UpdateDepartment(department);
                departmentRepository.Save();
                return RedirectToAction("Index");
            }
            return View(department);
        }

      

       

        protected override void Dispose(bool disposing)
        {
            departmentRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}