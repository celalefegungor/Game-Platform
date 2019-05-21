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
    public class UyeController : Controller
    {
        OyunEntities db = new OyunEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create",new Uye());
        }

        [HttpPost]
        public ActionResult Create(Uye uye,HttpPostedFileBase Foto)
        {
            if (!ModelState.IsValid)
            {
                return View("Create",uye);
            }
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
                    gu.Foto = uye.Foto;
                    gu.Kullaniciadi = uye.Kullaniciadi;
                    gu.Sifre = uye.Sifre;
                }

            }
            db.SaveChanges();
            return RedirectToAction("Login", "Uye");
        }

        public ActionResult UyeGuncelle(int id)
        {
            var gu = db.Uyes.Find(id);
            if (gu==null)
            {
                return HttpNotFound();
            }
            else
            {
                return View("Create", gu);
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            
            return View("Login",new Uye());
        }

        [HttpPost]
        public ActionResult Login(Uye uye)
        {
           


            var login = db.Uyes.Where(x => x.Kullaniciadi == uye.Kullaniciadi).SingleOrDefault();
            if (login == null)
            {
                ViewBag.msj = "Kullanıcı Adı veya Şifre Hatalı.";
                return View("Login");
            }
            
            if (login.Kullaniciadi==uye.Kullaniciadi && login.Sifre==uye.Sifre)
               
            {
                Session["uyeid"] = login.id;
                Session["kullaniciadi"] = login.Kullaniciadi;
                Session["yetkiid"] = login.Yetkiid;
            }
            else
            {
                return View("Login");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session["uyeid"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Uye");
        }

        public ActionResult Detay(int id)
        {
            if (id==0)
            {
                return HttpNotFound();
            }
            else
            {
                Uye uye = db.Uyes.Find(id);
                return View("Detay",uye);
            }
        }
        
    }   
}