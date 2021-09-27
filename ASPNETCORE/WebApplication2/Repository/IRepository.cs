using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NLog;
using System.Collections.Concurrent;
namespace WebApplication2.Models
{
    public interface IRepository<T>: IDisposable
    {
        Task<IEnumerable<T>> Get();
        Task<T> Get(int Id);
        Task<T> Get(T obj);
        Task<bool> Post(T obj);
        Task<bool> Put(T obj);
        Task<bool> Delete(T obj);
    }
}
