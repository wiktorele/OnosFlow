using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnosFlow.Data;
using OnosFlow.Models;

namespace OnosFlow.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }
       
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateUser(User user)
        {
            if(!ModelState.IsValid)
            {
                return View(user);
            }

            await _context.AddAsync(user);

            int isSuccess = await _context.SaveChangesAsync();

            if(!(isSuccess > 0))
            {
                ModelState.AddModelError("", "Error");
            }

            return View(user);
        }


    }
}
