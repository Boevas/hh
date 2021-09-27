using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using WebApplication2.Controllers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace WebApplication2.Models
{
    public class DepartmentsRepositoryDB : TemplateRepositoryDB<Department>
    {
        public DepartmentsRepositoryDB(AppContextDB db) : base(db, db.Departments) { }
    }
}
