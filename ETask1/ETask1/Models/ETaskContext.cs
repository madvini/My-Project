using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ETask1.Models
{
    public class ETaskContext:DbContext
    {
        public DbSet<Department> Departments { get; set; }

        public DbSet<Designation> Designations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Watcher> Watchers { get; set; }

        public DbSet<UserProjectDetail> UserProjectDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }
    }

}