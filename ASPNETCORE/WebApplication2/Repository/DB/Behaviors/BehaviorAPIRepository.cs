using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using WebApplication2.Controllers;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApplication2.MiddleWare.LoggerManager;

namespace WebApplication2.Models
{
    public class BehaviorAPIRepository<T> : IRepository<T> where T : class
    {
        private readonly ILoggerManager log;

        private readonly DbContext db;
        private readonly DbSet<T> dbs;
        
        public BehaviorAPIRepository(DbContext _db, IServiceProvider serviceProvider)
        {
            try
            {
                db = _db;

                //dbs = _dbs;
                foreach (PropertyInfo prop in _db.GetType().GetProperties())
                    if (prop.PropertyType == typeof(DbSet<T>))
                    {
                        dbs = prop.GetValue(_db, null) as DbSet<T>;
                        break;
                    }

                this.log = (ILoggerManager)serviceProvider.GetService(typeof(ILoggerManager));
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
            }
        }

        #region IDisposable

        public void Dispose()
        {
            try
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            { 
                if (true == disposing)
                    db?.Dispose();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
            }
        }
        
        #endregion IDisposable

        public async Task<IEnumerable<T>> Get()
        {
            try
            {
                return await dbs.ToListAsync();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }
        public async Task<T> Get(int Id)
        {
            try
            {
                 return await dbs.FirstOrDefaultAsync(x => (x as ModelId).Id == Id);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }
        public async Task<T> Get(T obj)
        {
            try
            {
                return await dbs.FirstOrDefaultAsync(x => (x as ModelId).Id == (obj as ModelId).Id);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }
        public async Task<bool> Post(T obj)
        {
            try
            {
                dbs.Add(obj);
                return 1 == await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return false;
            }
        }
        public async Task<bool> Put(T obj)
        {
            try
            {
                T old = await Get(obj);

                foreach (PropertyInfo property in typeof(T).GetProperties().Where(p => p.CanWrite))
                    property.SetValue(old, property.GetValue(obj, null), null);

                dbs.Update(old);
                return 1 == await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return false;
            }
        }
        public async Task<bool>  Delete(T obj)
        {
            try
            {
                dbs.Remove(obj);
                return 1 == await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return false;
            }
        }
    }
}
