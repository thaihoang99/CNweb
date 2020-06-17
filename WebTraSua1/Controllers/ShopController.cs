using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTraSua1.Models;
using WebTraSua1.Models.Models;

namespace WebTraSua1.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult DanhSachSanPham(string search)
        {
            ViewBag.search = HttpUtility.UrlEncode(search);
            return View();
        }

        public ActionResult DanhSachPartial(string search, int? maphanloai, int index)
        {

            int step = 4;
            search = HttpUtility.UrlDecode(search);
            List<SanPham> list = new TraSuaDbContext().SanPhams.AsNoTracking().ToList();
            if (!string.IsNullOrEmpty(search))
            {
                list = list.Where(x => x.TenSP.Contains(search)).ToList();
            }
            if (maphanloai != null)
            {
                list = list.Where(x => x.MaTL == maphanloai).ToList();
            }

            list = list.Skip(index * step).Take(step).ToList();

            return PartialView("DanhSachPartial", list);
        }

        public ActionResult ChiTietSanPham(int id)
        {
            SanPham item = new TraSuaDbContext().SanPhams.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        public ActionResult DanhSachPhanLoai(int maphanloai)
        {
            ViewBag.phanloai = maphanloai;
            return View();
        }

        public JsonResult GetSize(int id)
        {
            var db = new TraSuaDbContext();
            db.Configuration.ProxyCreationEnabled = false;
            var ctsp = from s in db.Sizes
                       join ct in db.CTSPs on s.MaSize equals ct.MaSize
                       where ct.MaSP == id
                       select s;
            return Json(ctsp.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}