using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using NLog;
using WebApplication2.MiddleWare.LoggerManager;

namespace WebApplication2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController<T> : Controller, IApi<T>
    {
        protected readonly IApi<T> Iapi;
        protected readonly ILoggerManager log;
        public TemplateController(IServiceProvider serviceProvider)
        {
            this.Iapi = (IApi<T>)serviceProvider.GetService(typeof(IApi<T>));
            this.log = (ILoggerManager)serviceProvider.GetService(typeof(ILoggerManager));
        }

        //GET: api/Users
        [HttpGet]
        public async Task<IEnumerable<T>> Get()
        {
            try
            {
                return await Iapi.Get();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }

        //GET api/Users/1
        [HttpGet("{id}")]
        public async Task<ActionResult<T>> Get(int Id)
        {
            try
            {
                return await Iapi.Get(Id);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }

        //POST api/Users
        [HttpPost]
        public async Task<IActionResult> Post(T obj)
        {
            try
            {
                return await Iapi.Post(obj);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }

        //PUT api/Users/2
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] T obj)
        {
            try
            {
                return await Iapi.Put(obj);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }

        //DELETE api/Users/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                return await Iapi.Delete(Id);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }
    }
}
