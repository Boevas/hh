using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using WebApplication2.Controllers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace WebApplication2.Models
{
    public class UsersRepositoryDB : TemplateRepositoryDB<User>
    {
        public UsersRepositoryDB(AppContextDB db) : base(db, db.Users) { }
    }
}
