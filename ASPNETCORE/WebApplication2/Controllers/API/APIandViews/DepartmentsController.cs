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
    public class DepartmentsController : ViewTemplateAPIController<Department>
    {
        public DepartmentsController(IRepository<Department> _IRep, ILoggerManager _log) : base(_IRep, _log) { }
    }
}
