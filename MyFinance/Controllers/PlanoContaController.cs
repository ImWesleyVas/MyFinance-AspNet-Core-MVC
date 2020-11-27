using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Controllers
{
    public class PlanoContaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
