namespace WebTraSua1.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        public int MaAdmin { get; set; }

        [Required]
        [StringLength(255)]
        public string TaiKhoan { get; set; }

        [Required]
        [StringLength(255)]
        public string MatKhau { get; set; }

        [StringLength(255)]
        public string TenAdmin { get; set; }

        public DateTime? NgayTao { get; set; }
    }
}
