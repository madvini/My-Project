using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ETask1.Models;
using System.Web.Mvc;

namespace ETask1.DAL
{
    public class ProjectRepository:IProjectRepository,IDisposable
    {
        private ETaskContext context;

        public ProjectRepository(ETaskContext context)
        {
            this.context = context;
        }
        public IEnumerable<Project> GetProjects()
        {
            return context.Projects.ToList();
        }
        public Project GetProjectByID(string projectID)
        {
            return context.Projects.Find(projectID);
        }
        public void InsertProject(Project project)
        {
            string str = Convert.ToString(context.Projects.Max(p => p.ProjectID));
            if (str == null || str == "")
            {
                project.ProjectID = "PRJ01";
            }
            else
            {

                int i = Convert.ToInt32(str.Substring(3));
                i = i + 1;
                if (i > 9)
                {
                    project.ProjectID = "PRJ" + i.ToString();
                }
                else
                {
                    project.ProjectID = "PRJ0" + i.ToString();
                }
            }
            context.Projects.Add(project);
        }
        public void UpdateProject(Project project)
        {
            context.Entry(project).State = EntityState.Modified;
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


        public SelectList GetManagerByProject()
        {
		var query= from user in context.Users
		           where user.UserType=="manager"
				   select user;
            return new SelectList(query.ToList(), "UserID", "UserName");
        }

        public SelectList GetManagerByProject(Project project)
        {var query= from user in context.Users
		           where user.UserType=="manager"
				   select user;
            return new SelectList(query.ToList(), "UserID", "UserName", project.ProjectManager);
        }

        public IEnumerable<User> GetUsersByDepartment(string departmentID,string projID)
        {
            var assignedUsers = (from userproj in context.UserProjectDetails
                                join user in context.Users
                                on userproj.UserID equals user.UserID
                                where userproj.ProjectID==projID
                                where user.DepartmentID == departmentID
                                && user.UserType != "manager"
                                 select user.UserID).ToList();
            var query=from user in context.Users
                      where user.DepartmentID == departmentID
                      && user.UserType!="manager"
                      where !assignedUsers.Contains(user.UserID)
                      select user;
                       
            return query;
        }





        public void AssignUserstoProject(UserProjectDetail projectdetails)
        {
            
            string str = Convert.ToString(context.UserProjectDetails.Max(p => p.ProjectDetailID));
            if (str == null || str == "")
            {
                projectdetails.ProjectDetailID = "UPD01";
            }
            else
            {

                int i = Convert.ToInt32(str.Substring(3));
                i = i + 1;
                if (i > 9)
                {
                    projectdetails.ProjectDetailID = "UPD" + i.ToString();
                }
                else
                {
                    projectdetails.ProjectDetailID = "UPD0" + i.ToString();
                }
            }
            context.UserProjectDetails.Add(projectdetails);
        }
    }
}
    