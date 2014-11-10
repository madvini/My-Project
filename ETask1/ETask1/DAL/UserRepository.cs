using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ETask1.Models;
using System.Data;
using System.Web.Mvc;

namespace ETask1.DAL
{
    public class UserRepository:IUserRepository,IDisposable
    {
        private ETaskContext context;

        public UserRepository(ETaskContext context)
        {
            this.context = context;
        }

        public User VerifyUser(User user)
        {
            
            var query = (from emp in context.Users
                         where emp.UserName == user.UserName &&
                          emp.Password == user.Password &&
                          emp.Status=="Active"
                         select emp).SingleOrDefault();
            return query;
        }
        public IEnumerable<User> GetUsers()
        {
            return context.Users.ToList();
        }
        public User GetUserByID(string userID)
        {
            return context.Users.Find(userID);
        }
        public void InsertUser(User user)
        {
            string str = Convert.ToString(context.Users.Max(u => u.UserID));
            if (str == null || str == "")
            {
                user.UserID = "USR01";
            }
            else
            {

                int i = Convert.ToInt32(str.Substring(3));
                i = i + 1;
                if (i > 9)
                {
                    user.UserID = "USR" + i.ToString();
                }
                else
                {
                    user.UserID = "USR0" + i.ToString();
                }
            }
            user.Status = "Active";
            context.Users.Add(user);
        }
        public void UpdateUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;
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



        public SelectList GetDepartment()
        {
           return new SelectList(context.Departments, "DepartmentID", "DepartmentName");
        }

        public SelectList GetDepartment(User user)
        {
            return new SelectList(context.Departments, "DepartmentID", "DepartmentName", user.DepartmentID);
        }

        public SelectList GetDesignation()
        {
            return new SelectList(context.Designations, "DesignationID", "Title");
        }

        public SelectList GetDesignation(User user)
        {
            return new SelectList(context.Designations, "DesignationID", "Title", user.DesignationID);
        }


        public IEnumerable<object> GetUsersByDepartment(string id)
        {
            var query = (from emp in GetUsers()
                         where emp.DepartmentID == id
                         where emp.Status == "Active"
                         select new
                         {
                             UserId=emp.UserID,
                             UserName=emp.UserName,
                             Designation=emp.Designation.Title,
                             UserType=emp.UserType,
                             DOJ=emp.DOJ.Date.ToShortDateString(),
                             Email=emp.Email,
                             PhoneNo=emp.PhoneNo
                         });
            return query;
        }

        public User CheckPassword(string pswd, string empid)
        {
            var query = (from e in GetUsers()
                         where e.UserID == empid
                         where e.Password == pswd
                         where e.Status == "Active"
                         select e).SingleOrDefault();
            return query;
        }
    }
}
   