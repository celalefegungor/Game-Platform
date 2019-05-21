using MVCOyun.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Helpers;
using System.IO;
using MVCOyun.ViewModels;

namespace MVCOyun.Controllers
{
    public class AdminOyunController : Controller
    {
        OyunEntities db = new OyunEntities();
        public ActionResult Index()
        {
            var model = db.Oyuns.Include(x => x.Kategori).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Yeni()
        {
            var model = new OyunFormViewModel() {
                Kategoris=db.Kategoris.ToList(),
            };
            return View("AdminOyunForm",model);
        }

        public ActionResult Kaydet(Oyun oyun,HttpPostedFileBase Foto)
        {
            if (oyun.id==0)
            {
                WebImage img = new WebImage(Foto.InputStream);
                FileInfo fotoinfo = new FileInfo(Foto.FileName);
                string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                img.Resize(900, 400);
                img.Save("~/Uploads/OyunFoto/" + newfoto);
                oyun.Foto = "/Uploads/OyunFoto/" + newfoto;
                db.Oyuns.Add(oyun);
            }
            else
            {
                var go = db.Oyuns.Find(oyun.id);
                if (go==null)
                {
                    return HttpNotFound();
                }
                else
                {
                    if (System.IO.File.Exists(Server.MapPath(oyun.Foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(oyun.Foto));
                    }
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(900, 400);
                    img.Save("~/Uploads/OyunFoto/" + newfoto);
                    oyun.Foto = "/Uploads/OyunFoto/" + newfoto;
                    go.KategoriId = oyun.KategoriId;
                    go.Ad = oyun.Ad;
                    go.Foto = oyun.Foto;
                    go.Icerik = oyun.Icerik;
                    go.Puan = oyun.Puan;
                    go.Tarih = oyun.Tarih;
                    go.Sirket = oyun.Sirket;
                    go.Fiyat = oyun.Fiyat;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var gk = db.Oyuns.Find(id);
            if (gk==null)
            {
                return HttpNotFound();
            }
            else
            {
                var model = new OyunFormViewModel() {
                    Kategoris = db.Kategoris.ToList(),
                    Oyun = db.Oyuns.Find(id),
                };
                return View("AdminOyunForm", model);
            }
        }

        public ActionResult Sil(int id)
        {
            var so = db.Oyuns.Find(id);
            if (so==null)
            {
                return HttpNotFound();
            }
            else
            {
                db.Oyuns.Remove(so);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
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
                return View("Detay",oyun);
            }
        }
    }
}