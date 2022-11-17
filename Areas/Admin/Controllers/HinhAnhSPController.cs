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
    public class HinhAnhSPController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/HinhAnhSP
        public ActionResult Index()
        {
            var hinhAnhSPs = db.HinhAnhSPs.Include(h => h.SanPham1);
            return View(hinhAnhSPs.ToList());
        }

        // GET: Admin/HinhAnhSP/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhAnhSP hinhAnhSP = db.HinhAnhSPs.Find(id);
            if (hinhAnhSP == null)
            {
                return HttpNotFound();
            }
            return View(hinhAnhSP);
        }

        // GET: Admin/HinhAnhSP/Create
        public ActionResult Create()
        {
            ViewBag.SanPham = new SelectList(db.SanPhams, "Id", "TenSP");
            return View();
        }

        // POST: Admin/HinhAnhSP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SanPham,HinhAnh,IsActive")] HinhAnhSP hinhAnhSP)
        {
            if (ModelState.IsValid)
            {
                db.HinhAnhSPs.Add(hinhAnhSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SanPham = new SelectList(db.SanPhams, "Id", "TenSP", hinhAnhSP.SanPham);
            return View(hinhAnhSP);
        }

        // GET: Admin/HinhAnhSP/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhAnhSP hinhAnhSP = db.HinhAnhSPs.Find(id);
            if (hinhAnhSP == null)
            {
                return HttpNotFound();
            }
            ViewBag.SanPham = new SelectList(db.SanPhams, "Id", "TenSP", hinhAnhSP.SanPham);
            return View(hinhAnhSP);
        }

        // POST: Admin/HinhAnhSP/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SanPham,HinhAnh,IsActive")] HinhAnhSP hinhAnhSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hinhAnhSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SanPham = new SelectList(db.SanPhams, "Id", "TenSP", hinhAnhSP.SanPham);
            return View(hinhAnhSP);
        }

        // GET: Admin/HinhAnhSP/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhAnhSP hinhAnhSP = db.HinhAnhSPs.Find(id);
            if (hinhAnhSP == null)
            {
                return HttpNotFound();
            }
            return View(hinhAnhSP);
        }

        // POST: Admin/HinhAnhSP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HinhAnhSP hinhAnhSP = db.HinhAnhSPs.Find(id);
            db.HinhAnhSPs.Remove(hinhAnhSP);
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
