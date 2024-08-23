using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Troja.Views.Users
{
    public class Details : PageModel
    {
        private readonly ILogger<Details> _logger;

        public Details(ILogger<Details> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}