using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ETask1.Models;
using System.Web.Mvc;

namespace ETask1.DAL
{
    public interface ITaskRepository:IDisposable
    {
        IEnumerable<Task> GetTasks();
        Task GetTaskByID(string taskID);
        void InsertTask(Task task);
        void UpdateTask(Task task);

        SelectList GetProjects();
        SelectList GetProjects(Task task);

        SelectList GetUsers(string projectID);

        //SelectList GetWatchers(Task task);

        SelectList GetUsers();
        SelectList GetUsers(Task task);

        void AssignWatcherstoTask(Watcher watch);

        IEnumerable<object> GetTaskByStatus(string status,string projectid);

        IEnumerable<object> GetUserTaskByStatus(string id,string status);

        IEnumerable<object> GetTaskByDate(string status, string projectid,string assignee,DateTime fromdate,DateTime todate);

        void Save();

        IEnumerable<Watcher> GetWatchersByTask(string id);



        IEnumerable<object>GetUserTaskByDate(string id, string status, DateTime fromdate, DateTime todate);

        
    }
}