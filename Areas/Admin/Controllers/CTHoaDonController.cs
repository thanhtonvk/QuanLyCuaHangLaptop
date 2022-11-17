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
    public class CTHoaDonController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/CTHoaDon
        public ActionResult Index(int? idHoaDon)
        {
            var cTHoaDons = db.CTHoaDons.Where(x => x.IdHoaDon == idHoaDon);
            return View(cTHoaDons.ToList());
        }
    }
}