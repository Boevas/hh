using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using NLog;
//using Microsoft.Extensions.Logging;
namespace WebApplication2.Controllers
{
    public class UsersController : TemplateAPIController<User>
    {
        public UsersController(IRepository<User> _IRep) : base(_IRep) { }
        
    }
}
