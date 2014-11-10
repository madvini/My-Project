using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ETask1.Models;
using System.Data;

namespace ETask1.DAL
{
    public class WatcherRepository:IWatcherRepository,IDisposable
    {
        private ETaskContext context;

        public WatcherRepository(ETaskContext context)
        {
            this.context = context;
        }
        public IEnumerable<Watcher> GetWatchers()
        {
            return context.Watchers.ToList();
        }
        public Watcher GetWatcherByID(string watcherID)
        {
            return context.Watchers.Find(watcherID);
        }
        public void InsertWatcher(Watcher watcher)
        {
            context.Watchers.Add(watcher);
        }
        public void UpdateWatcher(Watcher watcher)
        {
            context.Entry(watcher).State = EntityState.Modified;
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