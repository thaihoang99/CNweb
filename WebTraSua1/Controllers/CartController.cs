using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebTraSua1.Models;
using WebTraSua1.Models.Models;

namespace WebTraSua1.Controllers
{
    public class CartController : Controller
    {
        [Authorize]
        public ActionResult Cart()
        {
            return View();
        }

        [HttpGet]
        public JsonResult AddItem(int id, int quantity, int size)
        {
            var db = new TraSuaDbContext();
            var item = db.SanPhams.Find(id);
            if (item == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            if (Session["CartItem"] == null)
            {
                var cart = new List<CartModel>();
                cart.Add(new CartModel()
                {
                    SP = item,
                    Quantity = quantity,
                    size = db.Sizes.Find(size)
                });
                Session["CartItem"] = cart;
            }
            else
            {
                var index = CheckExist(id, size);
                var cart = Session["CartItem"] as List<CartModel>;
                if (index == -1)
                {
                    cart.Add(new CartModel()
                    {
                        SP = item,
                        Quantity = quantity,
                        size = db.Sizes.Find(size)
                    });
                }
                else
                    cart[index].Quantity += quantity;
            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public int CheckExist(int id, int sizeid)
        {
            //tim vi tri cua id trong gio hang
            var cart = Session["CartItem"] as List<CartModel>;
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].SP.MaSP == id && cart[i].size.MaSize == sizeid)
                    return i;
            }
            return -1;
        }

        public void DeleteItem(int id, int size, string url)
        {
            int index = CheckExist(id, size);
            var cart = Session["CartItem"] as List<CartModel>;
            cart.RemoveAt(index);
            if (cart.Count == 0)
            {
                Session["CartItem"] = null;
            }
            //return RedirectToAction("DanhSachSanPham", "Shop");
            Response.Redirect(url, true);
        }

        [Authorize]
        public ActionResult CheckOut(HoaDon model)
        {
            string cookiename = HttpContext.User.Identity.Name;
            var user = new TraSuaDbContext().KhachHangs.Where(x => x.TaiKhoan == cookiename).First();
            return View(user);
        }

        public JsonResult SubmitCheckout(HoaDon model)
        {
            try
            {
                var user = new TraSuaDbContext().KhachHangs.Where(x => x.TaiKhoan == HttpContext.User.Identity.Name).First();
                if (ModelState.IsValid)
                {
                    var idHD = TaoHD(model, user.MaKH);
                    if (idHD != 0)
                    {
                        if (TaoCTHD(idHD) != 0)
                        {
                            Session["CartItem"] = null;
                            return Json(new { Success = 1 }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { Success = 0, error = "tao cthd ko thanh cong" }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { Success = 0, error = "tao hd ko thanh cong" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Success = 0, error = "model ko hop le" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Success = 0, error = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public int TaoHD(HoaDon model, int userid)
        {
            try
            {
                var hd = new HoaDon
                {
                    NgayTao = DateTime.Now,
                    MaKH = userid,
                    SDT = model.SDT,
                    TenKH = model.TenKH,
                    DiaChi = model.DiaChi,
                    MaTT = 1,
                    TongTien = GetTotal()
                };
                var db = new TraSuaDbContext();
                db.HoaDons.Add(hd);
                db.SaveChanges();
                return hd.MaHD;
            }
            catch
            {
                return 0;
            }
        }

        public int TaoCTHD(int idHD)
        {
            try
            {
                var db = new TraSuaDbContext();
                var cart = Session["CartItem"] as List<CartModel>;
                foreach (var item in cart)
                {
                    var cthd = new CTHoaDon
                    {
                        MaHD = idHD,
                        MaSP = item.SP.MaSP,
                        SoLuong = item.Quantity,
                        MaSize = item.size.MaSize
                    };
                    db.CTHoaDons.Add(cthd);
                    db.SaveChanges();
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public decimal GetTotal()
        {
            var cart = Session["CartItem"] as List<CartModel>;
            decimal total = 0;
            foreach (var item in cart)
            {
                total += item.SP.GiaSP * item.Quantity;
            }
            return total;
        }
    }
}