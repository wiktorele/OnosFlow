
using System.Linq;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
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

        [HttpGet]
        public IActionResult CreateConfig()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateConfig(Config config)
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

            if (PingAction(config))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("CannotConnectToController");
            }
        }

        public static bool PingAction(Config config)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(config.IpAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

        public IActionResult CannotConnectToController()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
