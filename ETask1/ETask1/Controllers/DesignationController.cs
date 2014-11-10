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
    public class DesignationController : Controller
    {
        private IDesignationRepository designationRepository;

        /*****************CONSTRUCTOR****************/
        public DesignationController()
        {
            this.designationRepository=new DesignationRepository(new ETaskContext());
        }
        public DesignationController(IDesignationRepository designations)
        {
            this.designationRepository=designations;
        }

        //
        // GET: /Designation/

        public ActionResult Index(int? page)
        {
            var designations = designationRepository.GetDesignations();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(designations.ToPagedList(pageNumber, pageSize));
            
        }

        //
        // GET: /Designation/Details/5

        public ActionResult Details(string id = null)
        {
            Designation designation = designationRepository.GetDesignationByID(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            return View(designation);
        }

        //
        // GET: /Designation/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Designation/Create

        [HttpPost]
        public ActionResult Create(Designation designation)
        {
            
            if (ModelState.IsValid)
            {
                designationRepository.InsertDesignation(designation);
                designationRepository.Save();
                return RedirectToAction("Index");
            }

            return View(designation);
        }

        //
        // GET: /Designation/Edit/5

        public ActionResult Edit(string id = null)
        {
            Designation designation = designationRepository.GetDesignationByID(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            return View(designation);
        }

        //
        // POST: /Designation/Edit/5

        [HttpPost]
        public ActionResult Edit(Designation designation)
        {
            if (ModelState.IsValid)
            {
                designationRepository.UpdateDesignation(designation);
                designationRepository.Save();
                return RedirectToAction("Index");
            }
            return View(designation);
        }

        
        protected override void Dispose(bool disposing)
        {
            designationRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}