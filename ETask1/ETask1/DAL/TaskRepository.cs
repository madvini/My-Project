using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ETask1.Models;
using System.Web.Mvc;
using System.Net;

namespace ETask1.DAL
{
    public class TaskRepository : ITaskRepository, IDisposable
    {
        private ETaskContext context;

        public TaskRepository(ETaskContext context)
        {
            this.context = context;
        }
        public IEnumerable<Task> GetTasks()
        {
            return context.Tasks.ToList();
        }
        public Task GetTaskByID(string taskID)
        {
            return context.Tasks.Find(taskID);
        }
        public void InsertTask(Task task)
        {
            string str = Convert.ToString(context.Tasks.Max(t => t.TaskID));
            if (str == null || str == "")
            {
                task.TaskID = "TSK01";
            }
            else
            {

                int i = Convert.ToInt32(str.Substring(3));
                i = i + 1;
                if (i > 9)
                {
                    task.TaskID = "TSK" + i.ToString();
                }
                else
                {
                    task.TaskID = "TSK0" + i.ToString();
                }
            }
            context.Tasks.Add(task);
        }
        public void UpdateTask(Task task)
        {
            context.Entry(task).State = EntityState.Modified;
        }
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
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
        public SelectList GetProjects()
        {
            return new SelectList(context.Projects, "ProjectID", "ProjectName");

        }
        public SelectList GetProjects(Task task)
        {
            return new SelectList(context.Projects, "ProjectID", "ProjectName", task.ProjectID);

        }

        public SelectList GetUsers()
        {
            return new SelectList(context.Users, "UserID", "UserName");

        }
        public SelectList GetUsers(Task task)
        {
            return new SelectList(context.Users, "UserID", "UserName", task.AssignedTo);

        }


        public SelectList GetUsers(string projectID)
        {
            var query = from u in context.UserProjectDetails
                        join user in context.Users 
                        on u.UserID equals user.UserID
                        where u.ProjectID == projectID
                        select user;

            return new SelectList(query.ToList(), "UserID", "UserName");
        }

        //public SelectList GetWatchers(Task task)
        //{
        //    var query = from u in context.Watchers
        //                join user in context.Users
        //                on u.UserID equals user.UserID
        //                where u.TaskID == task.TaskID
        //                select user;

        //    return new SelectList(query.ToList(), "UserID", "UserName");
        //}

        public IEnumerable<object> GetTaskByStatus(string status, string projectid)
        {
            if (status == "Closed")
            {
                var query = (from task in GetTasks()
                             where task.Status == status
                             where task.ProjectID == projectid
                             select new
                             {
                                 TaskID = task.TaskID,
                                 Subject = task.Subject,
                                 StartDate = task.StartDate.Date.ToShortDateString(),
                                 Priority = task.Priority,
                                 DueDate = task.DueDate.Date.ToShortDateString()
                             });
                return query;
            }
            else 
            {
                var query = (from task in GetTasks()
                             where task.Status == status
                             where task.ProjectID == projectid
                             select new
                             {
                                 TaskID = task.TaskID,
                                 Subject = task.Subject,
                                 StartDate = task.StartDate.Date.ToShortDateString(),
                                 Priority = task.Priority,
                                 DueDate = task.DueDate.Date.ToShortDateString()
                             });
                return query;
            }
            
        }


        public IEnumerable<object> GetTaskByDate(string status, string projectid, string assignee, DateTime fromdate, DateTime todate)
        {
            if (status == "Closed")
            {
                var query = (from task in GetTasks()
                             where task.Status == status
                             where task.ProjectID == projectid
                             where task.AssignedTo==assignee
                             where task.StartDate>=fromdate
                             where task.DueDate<=todate
                             select new
                             {
                                 TaskID = task.TaskID,
                                 Subject = task.Subject,
                                 StartDate = task.StartDate.Date.ToShortDateString(),
                                 Priority = task.Priority,
                                 DueDate = task.DueDate.Date.ToShortDateString()
                             });
                return query;
            }

            else
            {
                var query = (from task in GetTasks()
                             where task.Status == status
                             where task.ProjectID == projectid
                             where task.AssignedTo == assignee
                             where task.StartDate >=fromdate
                             where task.DueDate <= todate
                             select new
                             {
                                 TaskID = task.TaskID,
                                 Subject = task.Subject,
                                 StartDate = task.StartDate.Date.ToShortDateString(),
                                 Priority = task.Priority,
                                 DueDate = task.DueDate.Date.ToShortDateString()
                             });
                return query;
            }
        }

        public void AssignWatcherstoTask(Watcher watch)
        {

            string str = Convert.ToString(context.Watchers.Max(p => p.SerialNo));
            if (str == null || str == "")
            {
                watch.SerialNo = "WAT01";
            }
            else
            {

                int i = Convert.ToInt32(str.Substring(3));
                i = i + 1;
                if (i > 9)
                {
                    watch.SerialNo = "WAT" + i.ToString();
                }
                else
                {
                    watch.SerialNo = "WAT0" + i.ToString();
                }
            }
            context.Watchers.Add(watch);
        }

        public IEnumerable<object> GetUserTaskByStatus(string id,string status)
        {
            if (status == "Resolved")
            {
                var query = (from task in GetTasks()
                             where task.Status == status
                             where task.AssignedTo == id
                             select new
                             {
                                 TaskID = task.TaskID,
                                 Subject = task.Subject,
                                 StartDate = task.StartDate.Date.ToShortDateString(),
                                 Priority = task.Priority,
                                 DueDate = task.DueDate.Date.ToShortDateString()
                             });
                return query;
            }
            else
            {
                var query = (from task in GetTasks()
                             where task.Status == status
                             where task.AssignedTo == id
                             select new
                             {
                                 TaskID = task.TaskID,
                                 Subject = task.Subject,
                                 StartDate = task.StartDate.Date.ToShortDateString(),
                                 Priority = task.Priority,
                                 DueDate = task.DueDate.Date.ToShortDateString()
                             });
                return query;
            }

        }

        public IEnumerable<object> GetUserTaskByDate(string id, string status,DateTime fromdate, DateTime todate)
        {
            if (status == "Resolved")
            {
                var query = (from task in GetTasks()
                             where task.Status == status
                             where task.AssignedTo == id
                             where task.StartDate >= fromdate
                             where task.DueDate <= todate
                             select new
                             {
                                 TaskID = task.TaskID,
                                 Subject = task.Subject,
                                 StartDate = task.StartDate.Date.ToShortDateString(),
                                 Priority = task.Priority,
                                 DueDate = task.DueDate.Date.ToShortDateString()
                             });
                return query;
            }

            else
            {
                var query = (from task in GetTasks()
                             where task.Status == status
                             where task.AssignedTo == id
                             where task.StartDate >= fromdate
                             where task.DueDate <= todate
                             select new
                             {
                                 TaskID = task.TaskID,
                                 Subject = task.Subject,
                                 StartDate = task.StartDate.Date.ToShortDateString(),
                                 Priority = task.Priority,
                                 DueDate = task.DueDate.Date.ToShortDateString()
                             });
                return query;
            }
        }



       public IEnumerable<Watcher>GetWatchersByTask(string id)
        {
            var query = context.Watchers.Where(t => t.TaskID == id).ToList();
            return query;
        }

       

       //public void DownloadDoc(string id)
       //{
       //    string strURL = context.Tasks.Find(id).File;
       //    strURL = "~/Contents/Uploads";
       //    WebClient req = new WebClient();
       //    HttpResponse response = System.Web.HttpContext.Current.Response;
       //    response.Clear();
       //    response.ClearContent();
       //    response.ClearHeaders();
       //    response.Buffer = true;
       //    response.AddHeader("Content-Disposition", "attachment;filename=\"" + strURL + "\"");
       //    byte[] data = req.DownloadData(strURL);
       //    response.BinaryWrite(data);
       //    response.End();
       //}
       
    }
}
    
   
