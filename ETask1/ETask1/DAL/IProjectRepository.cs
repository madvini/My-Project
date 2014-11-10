using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ETask1.Models;
using System.Web.Mvc;

namespace ETask1.DAL
{
    public interface IProjectRepository:IDisposable
    {
        IEnumerable<Project> GetProjects();
        Project GetProjectByID(string projectID);
        void InsertProject(Project project);
        void UpdateProject(Project project);
        SelectList GetManagerByProject();
        SelectList GetManagerByProject(Project project);

        IEnumerable<User> GetUsersByDepartment(string departmentID,string projID);
        void AssignUserstoProject(UserProjectDetail projectdetails);
       
        void Save();
    }
}