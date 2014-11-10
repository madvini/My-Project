using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ETask1.Models;
using System.Web.Mvc;

namespace ETask1.DAL
{
    public interface IUserRepository:IDisposable
    {
        IEnumerable<User> GetUsers();
        User GetUserByID(string userID);
        void InsertUser(User user);
        void UpdateUser(User user);
        
        SelectList GetDepartment();
        SelectList GetDepartment(User user);

        SelectList GetDesignation();
        SelectList GetDesignation(User user);

        IEnumerable<object> GetUsersByDepartment(string id);

        User CheckPassword(string pswd, string empid);
        
        void Save();
        User VerifyUser(User user);
    }
}