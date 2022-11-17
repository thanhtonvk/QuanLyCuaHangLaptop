using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QuanLyCuaHangLaptop.Models;

namespace QuanLyCuaHangLaptop.Controllers
{
    public class HomeController : Controller
    {
        private DBContext db = new DBContext();

        public ActionResult Index()
        {
            ViewBag.SPMoi =
                db.SanPhams.Where(x => x.IsActive == true && x.SpMoi == true).Take(6).ToList();
            ViewBag.SPBanChay = db.SanPhams.Where(x => x.IsActive == true && x.BanChay == true).Take(6).ToList();
            return View();
        }

        public ActionResult DanhSachSanPham(int? page)
        {
            if (page == null) page = 1;
            var model = db.SanPhams.Where(x => x.IsActive == true).ToList();
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string tenTK, string matKhau)
        {
            if (tenTK == "admin" && matKhau == "admin")
            {
                return RedirectToAction("Index", "SanPham", new {area = "Admin"});
            }
            var taiKhoan =
                db.TaiKhoans.FirstOrDefault(x =>
                    x.TenTK.Equals(tenTK) && x.MatKhau.Equals(matKhau) && x.IsActive == true);
            if (taiKhoan != null)
            {
                Session["TaiKhoan"] = taiKhoan;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ThongBao = "Tài khoản hoặc mật khẩu không chính xác";
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(string tenTK, string matKhau, string hoTen, string sdt, string diachi)
        {
            TaiKhoan taiKhoan = new TaiKhoan()
            {
                TenTK = tenTK,
                MatKhau = matKhau,
                HoTen = hoTen,
                SDT = sdt,
                DiaChi = diachi,
                IsActive = true
            };
            db.TaiKhoans.Add(taiKhoan);
            if (db.SaveChanges() > 0)
            {
                ViewBag.ThongBaoDangKy = "Đăng ký thành công, hãy đăng nhập";
            }
            else
            {
                ViewBag.ThongBaoDangKy = "Đăng ký không thành công, hãy kiểm tra lại";
            }

            return RedirectToAction("Login", "Home");
        }

        public ActionResult TimKiem(string tuKhoa)
        {
            var sanPham = db.SanPhams.Where(x => x.IsActive == true && x.TenSP.ToLower().Contains(tuKhoa.ToLower()));
            return View(sanPham);
        }

        public ActionResult Details(int? id)
        {
            ViewBag.SPBanChay = db.SanPhams.Where(x => x.IsActive == true && x.BanChay == true).Take(6).ToList();
            var sanPham = db.SanPhams.Find(id);
            return View(sanPham);
        }

        public ActionResult SanPhamTheoDanhMuc(int? id)
        {
            var sanPham = db.SanPhams.Where(x => x.LoaiSP == id);
            return View(sanPham);
        }

        public ActionResult SanPhamTheoHang(int? id)
        {
            var sanPham = db.SanPhams.Where(x => x.HangSX == id);
            return View(sanPham);
        }

        public ActionResult GioHang()
        {
            List<GioHang> gioHangList = (List<GioHang>) Session["GioHang"];
            if (gioHangList != null)
            {
                return View(gioHangList);
            }

            return View(new List<GioHang>());
        }

        public ActionResult ThemGioHang(int? id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            List<GioHang> gioHangList = (List<GioHang>) Session["GioHang"];

            if (gioHangList == null)
            {
                gioHangList = new List<GioHang>();
            }

            int idx = checkExist(sanPham.Id);
            if (idx > -1)
            {
                gioHangList[idx].SoLuong += 1;
            }
            else
            {
                GioHang gioHang = new GioHang()
                {
                    GiaBan = sanPham.GiaBan.Value,
                    HinhAnh = sanPham.HinhAnh,
                    IdSanPham = sanPham.Id,
                    SoLuong = 1,
                    TenSP = sanPham.TenSP
                };
                gioHangList.Add(gioHang);
            }

            Session["GioHang"] = gioHangList;
            return RedirectToAction("GioHang", "Home");
        }

        public ActionResult Delete(int idSanPham)
        {
            SanPham sanPham = db.SanPhams.Find(idSanPham);
            List<GioHang> gioHangList = (List<GioHang>) Session["GioHang"];

            if (gioHangList != null)
            {
                int idx = checkExist(sanPham.Id);
                gioHangList.RemoveAt(idx);
                Session["GioHang"] = gioHangList;
            }

            return RedirectToAction("GioHang", "Home");
        }


        public ActionResult DatHang(string diaChi, string sdt)
        {
            TaiKhoan taiKhoan = (TaiKhoan) Session["TaiKhoan"];
            if (taiKhoan != null)
            {
                HoaDon hoaDon = new HoaDon()
                {
                    Id = new Random().Next(),
                    DiaChi = diaChi,
                    TenTK = taiKhoan.TenTK,
                    SDT = sdt,
                    NgayMua = DateTime.Now
                };
                db.HoaDons.Add(hoaDon);
                List<GioHang> gioHangList = (List<GioHang>) Session["GioHang"];
                foreach (var gioHang in gioHangList)
                {
                    CTHoaDon cthd = new CTHoaDon()
                    {
                        Gia = gioHang.GiaBan,
                        IdHoaDon = hoaDon.Id,
                        SanPham = gioHang.IdSanPham,
                        SoLuong = gioHang.SoLuong
                    };
                    db.CTHoaDons.Add(cthd);
                }

                if (db.SaveChanges() > 0)
                {
                    Session["GioHang"] = null;
                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("GioHang", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public int checkExist(int idSanPham)
        {
            List<GioHang> gioHangList = (List<GioHang>) Session["GioHang"];
            int idx = -1;
            if (gioHangList != null)
            {
                for (int i = 0; i < gioHangList.Count; i++)
                {
                    if (gioHangList[i].IdSanPham == idSanPham) idx = i;
                }
            }

            return idx;
        }
    }
}