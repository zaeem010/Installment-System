using AR_IS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AR_IS.Controllers
{
    [SessionTimeout]
    public class ChatController : Controller
    {
       
        private ApplicationDbContext _context;

        public ChatController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Chat
        public ActionResult Chat()
        {
            var user = _context.tbl_GeneralUser.ToList();
            return View(user);
        }
    }
}