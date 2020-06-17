using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebTraSua1.Models.Models;

namespace WebTraSua1.Models
{
    public class CartModel
    {
        public SanPham SP { get; set; }
        public int Quantity { get; set; }
        public Size size { get; set; }
    }
}