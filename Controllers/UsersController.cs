using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Troja.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        //Creación el constructor de User con referencia al DbContext
       private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: User
        public IActionResult Index()
        {
            return View(_context.Users.ToList());
        }
    }
}