using MemberSys_SQLite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MemberSys_SQLite.Controllers
{
    public class HomeController : Controller
    {
        private readonly MembersContext _context;
        public HomeController(MembersContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Members"] = _context.Members.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Members([Bind("ID,Name,Age,Address,Province,Email,Phone")] Members members, string fnType)
        {
            if (ModelState.IsValid)
            {
                if (fnType.ToLower() == "insert")
                {
                    int id = 1;
                    var result = from c in _context.Members
                                 select c;

                    if (result != null)
                    {
                        id = result.Select(x => x.ID).Max() + 1;
                    }

                    members.ID = id;

                    _context.Add(members);
                    await _context.SaveChangesAsync();
                }
                else if (fnType.ToLower() == "update")
                {
                    _context.Update(members);
                    await _context.SaveChangesAsync();
                }
                else if (fnType.ToLower() == "delete")
                {

                    _context.Remove(members);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> DelMembers([Bind("ID,Name,Age,Address,Province,Email,Phone")] Members memberse)
        {
            if (ModelState.IsValid)
            {
                _context.Remove(memberse);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        public JsonResult MembersData([FromBody] string id)
        {
            var result = (from c in _context.Members
                          where c.ID == int.Parse(id)
                          select c).FirstOrDefault();

            Members members = new Members
            {
                ID = result.ID,
                Name = result.Name,
                Age = result.Age,
                Address = result.Address,
                Province = result.Province,
                Email = result.Email,
                Phone = result.Phone
            };

            return Json(members);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
