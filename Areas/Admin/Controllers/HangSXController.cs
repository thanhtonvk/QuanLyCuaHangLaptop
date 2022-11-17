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
    public class HangSXController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/HangSX
        public ActionResult Index()
        {
            return View(db.HangSXes.Where(x=>x.IsActive==true).ToList());
        }

        // GET: Admin/HangSX/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangSX hangSX = db.HangSXes.Find(id);
            if (hangSX == null)
            {
                return HttpNotFound();
            }
            return View(hangSX);
        }

        // GET: Admin/HangSX/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/HangSX/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TenHang,HinhAnh,IsActive")] HangSX hangSX)
        {
            if (ModelState.IsValid)
            {
                hangSX.IsActive = true;
                db.HangSXes.Add(hangSX);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hangSX);
        }

        // GET: Admin/HangSX/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangSX hangSX = db.HangSXes.Find(id);
            if (hangSX == null)
            {
                return HttpNotFound();
            }
            return View(hangSX);
        }

        // POST: Admin/HangSX/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenHang,HinhAnh,IsActive")] HangSX hangSX)
        {
            if (ModelState.IsValid)
            {
                hangSX.IsActive = true;
                db.Entry(hangSX).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hangSX);
        }

        // GET: Admin/HangSX/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangSX hangSX = db.HangSXes.Find(id);
            if (hangSX == null)
            {
                return HttpNotFound();
            }
            return View(hangSX);
        }

        // POST: Admin/HangSX/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HangSX hangSX = db.HangSXes.Find(id);
            hangSX.IsActive = false;
            db.Entry(hangSX).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
