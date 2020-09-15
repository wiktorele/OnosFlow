using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using OnosFlow.Data;
using OnosFlow.Models;

namespace OnosFlow.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;
        //private readonly IUser _user;

        public HomeController(Context context)
        {
            _context = context;
        }
       
        public IActionResult Index()
        {
            var any= !_context.Configs.Any();

            if (!_context.Configs.Any())
            {
                return RedirectToAction("CreateConfig");
            }
            else
            {
                var config =_context.Configs.First();
                return View(config);
            }

        }

        public async Task<IActionResult> CreateConfig(Config config)
        {
            if (!ModelState.IsValid)
            {
                return View(config);
            }

            if(!_context.Configs.Any())
            {
                _context.Add(config);
            }
            else
            {
                var oldConfig = _context.Configs.First();
                
                oldConfig.UserName = config.UserName;
                oldConfig.Password = config.Password;
                oldConfig.IpAddress = config.IpAddress;

                _context.Configs.Update(oldConfig);
            }

            int isSuccess = _context.SaveChanges();

            if (!(isSuccess > 0))
            {
                ModelState.AddModelError("", "Error");
            }

            return RedirectToAction("Index");
        }


    }
}
