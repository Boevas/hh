using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using NLog;
using WebApplication2.MiddleWare.LoggerManager;
//using Microsoft.Extensions.Logging;
namespace WebApplication2.Controllers
{
    public partial class DepartmentsController
    {
        [HttpGet("/[controller]")]
        public ViewResult Table()
        {
            try
            {
                return View("~/Views/Table/Table.cshtml", Get().Result);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }

        [HttpGet("/[controller]/[action]")]
        public IActionResult Add()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }
        [HttpPost("/[controller]/[action]")]
        public async Task<IActionResult> Add([FromForm] Department department)
        {
            try
            {
                return await base.Post(department);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }
        [HttpGet("/[controller]/[action]")]
        public ViewResult Edit()
        {
            try
            {
                return View(Get().Result);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }
        [HttpPost("/[controller]/[action]")]
        public async Task<IActionResult> Edit([FromForm] Department department)
        {
            try
            {
                return await base.Put(department);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return null;
            }
        }
    }
}
