using MVCOyun.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace MVCOyun.Controllers
{
    public class HomeController : Controller
    {
        OyunEntities db = new OyunEntities();
        public ActionResult Index(int Page=1)
        {
            var model = db.Oyuns.OrderByDescending(m => m.id).ToPagedList(Page, 5);
            return View(model);
        }

        public ActionResult KategoriPartial()
        {
            return View(db.Kategoris.ToList());
        }
        public ActionResult Deneme()
        {
            return View();
        }

        public ActionResult Fragman()
        {
            return View();
        }

        public ActionResult Detay(int? id)
        {
            if (id==null)
            {
                return HttpNotFound();
            }
            else
            {
                Oyun oyun = db.Oyuns.Find(id);
                return View("Detay", oyun);
            }
          
        }

        public ActionResult Elestirmen()
        {
            return View();
        }
        public ActionResult Tartisma()
        {
            return View();
        }

        public ActionResult KategoriOyun(int id)
        {
            var model = db.Oyuns.Where(x => x.KategoriId == id).ToList();
            return View(model);
        }

        public ActionResult Searchs(string Ara=null)
        {
            var aranan = db.Oyuns.Where(m => m.Ad.Contains(Ara)).ToList();
            return View(aranan.OrderByDescending(x => x.Tarih));
        }

        public ActionResult Oyun()
        {
            var model = db.Oyuns.ToList();
            return View(model);
        }
    }
}