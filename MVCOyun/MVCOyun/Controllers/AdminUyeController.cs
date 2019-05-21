using MVCOyun.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MVCOyun.Controllers
{
    public class AdminUyeController : Controller
    {
        OyunEntities db = new OyunEntities();
        public ActionResult Index()
        {
            var model = db.Uyes.ToList();
            return View(model);
        }

        public ActionResult Detay(int? id)
        {
            if (id==null)
            {
                return HttpNotFound();
            }
            else
            {
                Uye uye = db.Uyes.Find(id);
                return View("Detay", uye);
            }
        }

        public ActionResult Yeni()
        {
            return View("AdminUyeForm");
        }

        public ActionResult Kaydet(Uye uye,HttpPostedFileBase Foto)
        {
            if (uye.id==0)
            {
                WebImage img = new WebImage(Foto.InputStream);
                FileInfo fotoinfo = new FileInfo(Foto.FileName);
                string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                img.Resize(500, 500);
                img.Save("~/Uploads/UyeFoto/" + newfoto);
                uye.Foto = "/Uploads/UyeFoto/" + newfoto;
                uye.Yetkiid = 2;
                db.Uyes.Add(uye);
            }
            else
            {
                var gu = db.Uyes.Find(uye.id);
                if (gu==null)
                {
                    return HttpNotFound();
                }
                else
                {
                    if (System.IO.File.Exists(Server.MapPath(uye.Foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(uye.Foto));
                    }
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(500, 500);
                    img.Save("~/Uploads/UyeFoto/" + newfoto);
                    uye.Foto = "/Uploads/UyeFoto/" + newfoto;
                    gu.AdSoyad = uye.AdSoyad;
                    gu.DogumTarihi = uye.DogumTarihi;
                    gu.Email = uye.Email;
                    gu.Kullaniciadi = uye.Kullaniciadi;
                    gu.Sifre = uye.Sifre;

                }
                
            }
            db.SaveChanges();
            return RedirectToAction("Index", "AdminUye");
        }

        public ActionResult Guncelle(int id)
        {
            var gu = db.Uyes.Find(id);
            if (gu==null)
            {
                return HttpNotFound();
            }
            else
            {
                return View("AdminUyeForm", gu);
            }
        }

        public ActionResult Sil(int id)
        {
            var su = db.Uyes.Find(id);
            if (su==null)
            {
                return HttpNotFound();
            }
            else
            {
                db.Uyes.Remove(su);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "AdminUye");
        }
    }
}