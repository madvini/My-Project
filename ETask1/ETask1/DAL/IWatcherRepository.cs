using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ETask1.Models;

namespace ETask1.DAL
{
    public interface IWatcherRepository:IDisposable
    {
        IEnumerable<Watcher> GetWatchers();
        Watcher GetWatcherByID(string watcherID);
        void InsertWatcher(Watcher watcher);
        void UpdateWatcher(Watcher watcher);
        void Save();
    }
}