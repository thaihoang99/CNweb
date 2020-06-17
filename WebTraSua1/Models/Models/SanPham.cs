namespace WebTraSua1.Models.Models
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
            CTSPs = new HashSet<CTSP>();
        }

        [Key]
        public int MaSP { get; set; }

        [Required]
        [StringLength(255)]
        public string TenSP { get; set; }

        public string MoTaSP { get; set; }

        [StringLength(255)]
        public string AnhSP { get; set; }

        public decimal GiaSP { get; set; }

        public decimal? KhuyenMai { get; set; }

        public int? Rating { get; set; }

        [StringLength(255)]
        public string UrlSP { get; set; }

        public bool? TrangThaiSP { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? MaTL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHoaDon> CTHoaDons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTSP> CTSPs { get; set; }

        public virtual TheLoai TheLoai { get; set; }
    }
}
