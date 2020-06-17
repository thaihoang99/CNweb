using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebTraSua1.Models;
using WebTraSua1.Models.Models;

namespace WebTraSua1.Controllers
{
    public class CustomerController : Controller
    {

        public ActionResult DangKyDangNhap()
        {
            return View();
        }

        [HttpPost]
        public JsonResult DangKy(KhachHang model)
        {
            if (ModelState.IsValid)
            {
                model.MatKhau = MD5Hash(model.MatKhau);
                model.NgayTao = DateTime.Now;

                var db = new TraSuaDbContext();
                db.KhachHangs.Add(model);
                db.SaveChanges();

                return Json(new { Success = 1 }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DangNhap(LoginModel model)
        {
            string enc = MD5Hash(model.UserPassword);
            var result = new TraSuaDbContext().KhachHangs.Count(x => x.TaiKhoan == model.UserName && x.MatKhau == enc);

            if (ModelState.IsValid && result > 0)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true);

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["CartItem"] = null;
            return RedirectToAction("DangKyDangNhap", "Customer");
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        [Authorize]

        public ActionResult Detail()
        {
            return View();
        }

        //form dang nhap-> dang ky cai form de authentication -> dang nhap thanh cong -> set cookie
    }
}