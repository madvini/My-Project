using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ETask1.Models;

namespace ETask1.DAL
{
    public class DesignationRepository:IDesignationRepository,IDisposable
    {
        private ETaskContext context;

        public DesignationRepository(ETaskContext context)
        {
            this.context = context;
        }
        public IEnumerable<Designation> GetDesignations()
        {
            return context.Designations.ToList();
        }
        public Designation GetDesignationByID(string designationID)
        {
            return context.Designations.Find(designationID);
        }
        public void InsertDesignation(Designation designation)
        {
            string str = Convert.ToString(context.Designations.Max(d => d.DesignationID));
            if (str == null || str == "")
            {
                designation.DesignationID = "DES01";
            }
            else
            {

                int i = Convert.ToInt32(str.Substring(3));
                i = i + 1;
                if (i > 9)
                {
                    designation.DesignationID = "DES" + i.ToString();
                }
                else
                {
                    designation.DesignationID = "DES0" + i.ToString();
                }
            }
            context.Designations.Add(designation);
        }
        public void UpdateDesignation(Designation designation)
        {
            context.Entry(designation).State = EntityState.Modified;
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
    