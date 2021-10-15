using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using WebApplication2.Controllers;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace WebApplication2.Models
{
    public class BehaviorAPIRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext db;
        private readonly DbSet<T> dbs;
        
        public BehaviorAPIRepository(DbContext _db)
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
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
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
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
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
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
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
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
                return null;
            }
        }
        public async Task<T> Get(int Id)
        {
            try
            {
                 return await dbs.FirstOrDefaultAsync(x => (x as Model).Id == Id);
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
                return null;
            }
        }
        public async Task<T> Get(T obj)
        {
            try
            {
                return await dbs.FirstOrDefaultAsync(x => (x as Model).Id == (obj as Model).Id);
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
                return null;
            }
        }
        public async Task<bool> Post(T obj)
        {
            try
            {
                dbs.Add(obj);
                await db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
                return false;
            }
        }
        public async Task<bool> Put(T obj)
        {
            try
            {
                dbs.Update(obj);
                await db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
                return false;
            }
        }
        public async Task<bool>  Delete(T obj)
        {
            try
            {
                dbs.Remove(obj);
                await db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
                return false;
            }
        }
    }
}
