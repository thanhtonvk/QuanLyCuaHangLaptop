using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHangLaptop.Models;

namespace QuanLyCuaHangLaptop.Areas.Admin.Controllers
{
    public class HoaDonController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/HoaDon
        public ActionResult Index()
        {
            var hoaDons = db.HoaDons.Include(h => h.TaiKhoan);
            return View(hoaDons.ToList());
        }

        

       
    }
}
