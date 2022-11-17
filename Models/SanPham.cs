namespace QuanLyCuaHangLaptop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            CTHoaDons = new HashSet<CTHoaDon>();
            HinhAnhSPs = new HashSet<HinhAnhSP>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string TenSP { get; set; }

        public string HinhAnh { get; set; }

        public string MoTa { get; set; }

        public string ChiTiet { get; set; }

        public int? GiaBan { get; set; }

        public bool? BanChay { get; set; }

        public bool? SpMoi { get; set; }

        public int LoaiSP { get; set; }

        public int HangSX { get; set; }

        public bool? IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHoaDon> CTHoaDons { get; set; }

        public virtual HangSX HangSX1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HinhAnhSP> HinhAnhSPs { get; set; }

        public virtual LoaiSP LoaiSP1 { get; set; }
    }
}
