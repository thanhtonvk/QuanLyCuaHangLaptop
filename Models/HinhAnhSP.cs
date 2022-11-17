namespace QuanLyCuaHangLaptop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HinhAnhSP")]
    public partial class HinhAnhSP
    {
        public int Id { get; set; }

        public int SanPham { get; set; }

        public string HinhAnh { get; set; }

        public bool? IsActive { get; set; }

        public virtual SanPham SanPham1 { get; set; }
    }
}
