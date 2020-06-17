namespace WebTraSua1.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTHoaDon")]
    public partial class CTHoaDon
    {
        [Key]
        public int MaCTHD { get; set; }

        public int? SoLuong { get; set; }

        public int? MaHD { get; set; }

        public int? MaSP { get; set; }

        public int? MaSize { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual Size Size { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
