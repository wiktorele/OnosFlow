using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using OnosFlow.Data;
using OnosFlow.Models;

namespace OnosFlow.Controllers
{
    public class HomeController : Controller
    {
        //private readonly Context _context;
        private IConfig _config;

        public HomeController(/*Context context*/ IConfig config)
        {
            _config = config;
            //_context = context;

        }

        public IActionResult Index()
        {
            //var any= !_context.Configs.Any();

            if (/*!_context.Configs.Any()*/ _config.IpAddress is null)
            {
                return RedirectToAction("CreateConfig");
            }
            else
            {
                var config = _config;//_context.Configs.First();
                return View(config);
            }



        }

        public IActionResult CreateConfig()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateConfig(Config config)
        {

            //if (!ModelState.IsValid)
            //{
            //    return View(config);
            //}
            if (/*!ModelState.IsValid*/config.IpAddress is null)
            {
                return View(config);
            }
            _config = config;
            //if(!_context.Configs.Any())
            //{
            //    _context.Add(config);
            //}
            //else
            //{
            //    var oldConfig = _context.Configs.First();

            //    oldConfig.UserName = config.UserName;
            //    oldConfig.Password = config.Password;
            //    oldConfig.IpAddress = config.IpAddress;

            //    _context.Configs.Update(oldConfig);
            //}

            //int isSuccess = _context.SaveChanges();

            //if (!(isSuccess > 0))
            //{
            //    ModelState.AddModelError("", "Error");
            //}

            //Ping p = new Ping();
            //string ipAddress = config.IpAddress;
            //PingReply pr = p.Send(ipAddress, 2000);
            //try
            //{
            //    PingReply pr = p.Send(ipAddress);
            //}
            //catch()
            //{
            //    e,
            //}
            //if (pr.Status != IPStatus.Success)
            //{
            //    return Content("No connection witch such ip");
            //}
            //else
            //{
            return RedirectToAction("Index");
            //}

        }

        }
}
