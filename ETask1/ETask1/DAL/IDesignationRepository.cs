using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ETask1.Models;

namespace ETask1.DAL
{
    public interface IDesignationRepository:IDisposable
    {
        IEnumerable<Designation> GetDesignations();
        Designation GetDesignationByID(string designationID);
        void InsertDesignation(Designation designation);
        void UpdateDesignation(Designation designation);
        void Save();
    }
}