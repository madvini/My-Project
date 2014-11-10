using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ETask1.Models;

namespace ETask1.DAL
{
    public class DepartmentRepository:IDepartmentRepository,IDisposable
    {
        private ETaskContext context;

        public DepartmentRepository(ETaskContext context)
        {
            this.context = context;
        }
        public IEnumerable<Department> GetDepartments()
        {
            return context.Departments.ToList();
        }
        public Department GetDepartmentByID(string departmentID)
        {
            return context.Departments.Find(departmentID);
        }
        public void InsertDepartment(Department department)
        {
            string str = Convert.ToString(context.Departments.Max(d => d.DepartmentID));
            if (str == null || str == "")
            {
                department.DepartmentID = "DPT01";
            }
            else
            {

                int i = Convert.ToInt32(str.Substring(3));
                i = i + 1;
                if (i > 9)
                {
                    department.DepartmentID = "DPT" + i.ToString();
                }
                else
                {
                    department.DepartmentID = "DPT0" + i.ToString();
                }
            }
            context.Departments.Add(department);
        }
        public void UpdateDepartment(Department department)
        {
            context.Entry(department).State = EntityState.Modified;
        }
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed =false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
    
