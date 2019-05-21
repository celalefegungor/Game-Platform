using MVCOyun.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCOyun.Controllers
{
    public class AdminKategoriController : Controller
    {
        OyunEntities db = new OyunEntities();
        public ActionResult Index()
        {
            var model = db.Kategoris.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Yeni()
        {
            return View("AdminKategoriForm");
        }
        
        [HttpPost]
        public ActionResult Kaydet(Kategori kategori)
        {
            if (kategori.id==0)
            {
                db.Kategoris.Add(kategori);
            }
            else
            {
                var gk = db.Kategoris.Find(kategori.id);
                if (gk==null)
                {
                    return HttpNotFound();
                }
                else
                {
                    gk.Ad = kategori.Ad;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var gk = db.Kategoris.Find(id);
            if (gk==null)
            {
                return HttpNotFound();
            }
            else
            {
                return View("AdminKategoriForm", gk);
            }
        }

        public ActionResult Sil(int id)
        {
            var sk = db.Kategoris.Find(id);
            if (sk==null)
            {
                return HttpNotFound();
            }
            else
            {
                db.Kategoris.Remove(sk);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}