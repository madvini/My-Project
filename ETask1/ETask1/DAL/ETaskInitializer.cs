using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ETask1.Models;
using System.Data.Entity.Validation;

namespace ETask1.DAL
{
    public class ETaskInitializer : DropCreateDatabaseIfModelChanges<ETaskContext>
    {
        protected override void Seed(ETaskContext context)
        {
            try
            {

                var departments = new List<Department>
            {
                new Department{DepartmentID="DPT01",DepartmentName="Software"},
                new Department{DepartmentID="DPT02",DepartmentName="Networking"},
                new Department{DepartmentID="DPT03",DepartmentName="Testing"},
                new Department{DepartmentID="DPT04",DepartmentName="Accounts"}
            };
                departments.ForEach(s => context.Departments.Add(s));
                context.SaveChanges();


                var designations = new List<Designation>
            {
                new Designation{DesignationID="DES01",Title="Manager"},
                new Designation{DesignationID="DES02",Title="Junior Engineer"},
                new Designation{DesignationID="DES03",Title="Accountant"}
                
            };
                designations.ForEach(s => context.Designations.Add(s));
                context.SaveChanges();


                var users = new List<User>
            {
                new User{UserID="USR01",UserName="Aadidev",Password="aadidev123",ConfirmPassword="aadidev123", Email="aadi@gmail.com",PhoneNo=Convert.ToInt64("1234567890"),DepartmentID="DPT01", DesignationID="DES01",
                    UserType="admin",DOJ=DateTime.Parse("02/03/2000"),Status="Active"},
                new User{UserID="USR02",UserName="Anjaly",Password="anjaly123",ConfirmPassword="anjaly123",Email="anju@gmail.com",PhoneNo=Convert.ToInt64("9895856236"),DepartmentID="DPT03", DesignationID="DES02",
                    UserType="employee",DOJ=DateTime.Parse("12/02/2003"),Status="Active"},
                new User{UserID="USR03",UserName="Deepak",Password="deepak123",ConfirmPassword="deepak123",Email="deepu@gmail.com",PhoneNo=Convert.ToInt64("9745632896"),DepartmentID="DPT01", DesignationID="DES01",
                    UserType="employee",DOJ=DateTime.Parse("04/04/1998"),Status="Active"},
                new User{UserID="USR04",UserName="James",Password="james123",ConfirmPassword="james123", Email="james@gmail.com",PhoneNo=Convert.ToInt64("9963214586"),DepartmentID="DPT02", DesignationID="DES01",
                    UserType="manager",DOJ=DateTime.Parse("10/03/1996"),Status="Active"},
                new User{UserID="USR05",UserName="Devika",Password="devika123",ConfirmPassword="devika123", Email="devika@gmail.com",PhoneNo=Convert.ToInt64("9934114586"),DepartmentID="DPT02", DesignationID="DES02",
                    UserType="employee",DOJ=DateTime.Parse("03/03/1996"),Status="Active"}
                
            };
                users.ForEach(s => context.Users.Add(s));
                context.SaveChanges();




                var projects = new List<Project>
            {
                new Project{ProjectID="PRJ01",ProjectName="World Bank",Description="fdgt gfdagsdf gfdasg",ClientName="dsf",UserID="USR02", 
                    EstimatedStartDate=DateTime.Parse("10/30/2014"),EstimatedDueDate=DateTime.Parse("10/31/2015"),
                    ActualStartDate=DateTime.Parse("10/31/2014"),ActualDueDate=DateTime.Parse("10/31/2014")},
                new Project{ProjectID="PRJ02",ProjectName="Kannan Devan",Description="nghgj vdawf gfdasg",ClientName="Kannan Devan",UserID="USR03", 
                    EstimatedStartDate=DateTime.Parse("10/30/2014"),EstimatedDueDate=DateTime.Parse("10/31/2015"),
                    ActualStartDate=DateTime.Parse("10/30/2014"),ActualDueDate=DateTime.Parse("10/31/2014")},
                    new Project{ProjectID="PRJ03",ProjectName="hjgbvfds",Description="lkulo hgerw gfdasg",ClientName="dsf",UserID="USR02", 
                    EstimatedStartDate=DateTime.Parse("10/30/2014"),EstimatedDueDate=DateTime.Parse("10/31/2015"),
                    ActualStartDate=DateTime.Parse("10/30/2014"),ActualDueDate=DateTime.Parse("10/31/2014")},
                
            };
                projects.ForEach(s => context.Projects.Add(s));
                context.SaveChanges();



                var userprojectdetails = new List<UserProjectDetail>
                {
                    new UserProjectDetail{ProjectDetailID="UPD01",UserID="USR02",ProjectID="PRJ03"},
                    new UserProjectDetail{ProjectDetailID="UPD02",UserID="USR03",ProjectID="PRJ03"},
                    new UserProjectDetail{ProjectDetailID="UPD03",UserID="USR05",ProjectID="PRJ03"}
                };
                userprojectdetails.ForEach(s => context.UserProjectDetails.Add(s));
                context.SaveChanges();


                var tasks = new List<Task>
            {
                new Task{TaskID="TSK01",ProjectID="PRJ01",Subject="java",StartDate=DateTime.Parse("10/30/2014"),Priority="Urgent",
                    AssignedTo="USR02",DueDate=DateTime.Parse("10/31/2014"),Description="fdgt gfdagsdf gfdasg",
                    PercentageCompleted=12,Status="New"}, 
                new Task{TaskID="TSK02",ProjectID="PRJ02",Subject="php",StartDate=DateTime.Parse("10/30/2014"),Priority="Normal",
                    AssignedTo="USR03",DueDate=DateTime.Parse("10/31/2014"),Description="fdgt gfdagsdf gfdasg",
                    PercentageCompleted=98,Status="In Progress"}, 
                new Task{TaskID="TSK03",ProjectID="PRJ03",Subject=".net",StartDate=DateTime.Parse("10/30/2014"),Priority="High",
                    AssignedTo="USR02",DueDate=DateTime.Parse("10/31/2014"),Description="fdgt gfdagsdf gfdasg",
                    PercentageCompleted=50,Status="Completed"}, 
                
            };
                tasks.ForEach(s => context.Tasks.Add(s));
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
            }
        }
    }
}