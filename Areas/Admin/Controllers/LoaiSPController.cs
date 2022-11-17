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
    public class LoaiSPController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/LoaiSP
        public ActionResult Index()
        {
            return View(db.LoaiSPs.Where(x => x.IsActive == true).ToList());
        }

        // GET: Admin/LoaiSP/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LoaiSP loaiSP = db.LoaiSPs.Find(id);
            if (loaiSP == null)
            {
                return HttpNotFound();
            }

            return View(loaiSP);
        }

        // GET: Admin/LoaiSP/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiSP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TenLoai,HinhAnh,IsActive")]
            LoaiSP loaiSP)
        {
            if (ModelState.IsValid)
            {
                loaiSP.IsActive = true;
                db.LoaiSPs.Add(loaiSP);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiSP);
        }

        // GET: Admin/LoaiSP/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LoaiSP loaiSP = db.LoaiSPs.Find(id);
            if (loaiSP == null)
            {
                return HttpNotFound();
            }

            return View(loaiSP);
        }

        // POST: Admin/LoaiSP/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenLoai,HinhAnh,IsActive")]
            LoaiSP loaiSP)
        {
            if (ModelState.IsValid)
            {
                loaiSP.IsActive = true;
                db.Entry(loaiSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiSP);
        }

        // GET: Admin/LoaiSP/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LoaiSP loaiSP = db.LoaiSPs.Find(id);
            if (loaiSP == null)
            {
                return HttpNotFound();
            }

            return View(loaiSP);
        }

        // POST: Admin/LoaiSP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiSP loaiSP = db.LoaiSPs.Find(id);
            loaiSP.IsActive = false;
            db.Entry(loaiSP).State = EntityState.Modified;


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