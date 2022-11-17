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
    public class SanPhamController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/SanPham
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.HangSX1).Include(s => s.LoaiSP1);
            return View(sanPhams.Where(x=>x.IsActive==true).ToList());
        }

        // GET: Admin/SanPham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Admin/SanPham/Create
        public ActionResult Create()
        {
            ViewBag.HangSX = new SelectList(db.HangSXes, "Id", "TenHang");
            ViewBag.LoaiSP = new SelectList(db.LoaiSPs, "Id", "TenLoai");
            return View();
        }

        // POST: Admin/SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TenSP,HinhAnh,MoTa,ChiTiet,GiaBan,BanChay,SpMoi,LoaiSP,HangSX,IsActive")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                sanPham.IsActive = true;
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HangSX = new SelectList(db.HangSXes, "Id", "TenHang", sanPham.HangSX);
            ViewBag.LoaiSP = new SelectList(db.LoaiSPs, "Id", "TenLoai", sanPham.LoaiSP);
            return View(sanPham);
        }

        // GET: Admin/SanPham/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.HangSX = new SelectList(db.HangSXes, "Id", "TenHang", sanPham.HangSX);
            ViewBag.LoaiSP = new SelectList(db.LoaiSPs, "Id", "TenLoai", sanPham.LoaiSP);
            return View(sanPham);
        }

        // POST: Admin/SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenSP,HinhAnh,MoTa,ChiTiet,GiaBan,BanChay,SpMoi,LoaiSP,HangSX,IsActive")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                sanPham.IsActive = true;
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HangSX = new SelectList(db.HangSXes, "Id", "TenHang", sanPham.HangSX);
            ViewBag.LoaiSP = new SelectList(db.LoaiSPs, "Id", "TenLoai", sanPham.LoaiSP);
            return View(sanPham);
        }

        // GET: Admin/SanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            sanPham.IsActive = false;
            db.Entry(sanPham).State = EntityState.Modified;
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
