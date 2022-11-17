namespace QuanLyCuaHangLaptop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTHoaDon")]
    public partial class CTHoaDon
    {
        public int Id { get; set; }

        public int IdHoaDon { get; set; }

        public int? SanPham { get; set; }

        public int? Gia { get; set; }

        public int? SoLuong { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual SanPham SanPham1 { get; set; }
    }
}
