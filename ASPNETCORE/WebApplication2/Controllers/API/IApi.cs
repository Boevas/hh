using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NLog;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Models
{
    public interface IApi<T>
    {
        Task<IEnumerable<T>> Get();
        Task<ActionResult<T>> Get(int Id);
        Task<IActionResult> Post(T obj);
        Task<IActionResult> Put([FromBody] T obj);
        Task<IActionResult> Delete(int Id);
    }
}
