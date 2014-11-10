using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ETask1.Models;
using System.Web.Mvc;

namespace ETask1.DAL
{
    public interface IDepartmentRepository:IDisposable
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartmentByID(string departmentID);
        void InsertDepartment(Department department);
        void UpdateDepartment(Department department);
        void Save();
        
    }
}