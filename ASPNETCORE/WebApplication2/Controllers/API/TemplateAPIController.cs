using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using NLog;

namespace WebApplication2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TemplateAPIController<T>: Controller
    {
        private readonly IRepository<T> IRep;
        public TemplateAPIController(IRepository<T> _IRep) : base()
        {
            try
            {
                IRep = _IRep;
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
            }
        }

        //GET: api/Users
        [HttpGet]
        public async Task<IEnumerable<T>> Get()
        {
            try
            {
                return await IRep.Get();
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
                return null;
            }
        }
        //GET api/Users/1
        [HttpGet("{id}")]
        public async Task<ActionResult<T>> Get(int Id)
        {
            try
            {
                var u = await IRep.Get(Id);
                if (u is T)
                    return Ok(u);

                return NotFound();
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
                return null;
            }
        }

        //POST api/Users
        [HttpPost]
        public async Task<IActionResult> Post(T obj) 
        {
            try
            {
                var u = await IRep.Get(obj);
                if (u is T)
                    return Conflict(false);

                return Ok(await IRep.Post(obj));
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
                return null;
            }
        }

        //PUT api/Users/2
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] T obj)
        {
            try
            {
                var u = await IRep.Get(obj);
                if (u is T)
                    return Ok(await IRep.Put(obj));

                return NotFound();
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
                return null;
            }
        }

        //DELETE api/Users/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var u = await IRep.Get(Id);
                if (u is T)
                    return Ok(await IRep.Delete(u));

                return NotFound();
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
                return null;
            }
        }
    }
}
