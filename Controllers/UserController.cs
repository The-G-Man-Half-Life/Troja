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
    public class UserController : Controller
    {
        //Acción para mostrar el formulario de creación (GET)

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        
    }
}