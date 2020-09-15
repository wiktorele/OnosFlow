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
            var any= !_context.Users.Any();

            if (!_context.Users.Any())
            {
                return RedirectToAction("CreateUser");
            }
            else
            {
                var user =_context.Users.First();
                return View(user);
            }

        }

        public async Task<IActionResult> CreateUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if(!_context.Users.Any())
            {
                _context.Add(user);
            }
            else
            {
                var oldUser = _context.Users.First();
                
                oldUser.UserName = user.UserName;
                oldUser.Password = user.Password;
                oldUser.IpAddress = user.IpAddress;

                _context.Users.Update(oldUser);
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
