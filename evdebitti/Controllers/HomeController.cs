using evdebitti.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace evdebitti.Controllers
{
    public class HomeController : Controller
    {
        KuzeyYeliEntities db = new KuzeyYeliEntities();
        
        public ActionResult Index()
        {
             List<C_URUNLER> urunler = db.C_URUNLER.ToList();
           

            //  ViewBag.Kategories = db.C_SEPET.ToList().Select(s => new SelectListItem { Value = s.S_URUNFIYAT.ToString(), Text = s.S_URUNADI });

          //  Tuple.Create<C_URUNLER, C_SEPET>(new C_URUNLER(), new C_SEPET())

            return View(urunler);
        }


        public ActionResult oturumac()
        {
            return View();
        }

        public ActionResult uyeol()
        {
            return View();
        }
        public ActionResult urunbilgileri()
        {
            
            return View();
        }

        
        
        //public ActionResult Index(int URUNID,int quantity,string urunadi,double urunfiyat /*string urunadi, string urunfiyat*/)
        //{
        //    List<C_URUNLER> urunler = db.C_URUNLER.ToList();

        //    //List<C_SEPET> a = db.C_SEPET.ToList();
        //    //var query = db.C_SEPET.Where(s => s.S_URUNID == URUNID);

        //    //if (query == null)
        //    //{
        //    //    C_SEPET cs = new C_SEPET();
        //    //    cs.S_URUNID = URUNID;

        //    //    cs.S_URUNADI = urunadi;
        //    //    cs.S_URUNFIYAT = urunfiyat.ToString();

        //    //    db.C_SEPET.Add(cs);

        //    //    db.SaveChanges();


        //    //}
        //    //else if (query != null)
        //    //{

        //    //    C_SEPET cs = new C_SEPET();
        //    //    cs.S_URUNID = URUNID;

        //    //    cs.S_URUNADI = urunadi;
        //    //    cs.S_URUNFIYAT = urunfiyat.ToString();
        //    //    db.C_SEPET.Add(cs);

        //    //    db.SaveChanges();


        //    //}
        //    //else
        //    //{

        //    //}
        //    return View(urunler);
        //    //sepetigoruntule();
         

        //}
        [HttpPost]
        public void sepeteekle(int URUNID, int quantity, string urunadi,string urunresimyol, int urunfiyat)
        {
            List<C_SEPET> d = db.C_SEPET.ToList();
            C_SEPET cs = new C_SEPET();

            
            if (d.Any(x=>x.S_URUNID==URUNID))
            {
                db.C_SEPET.First(x => x.S_URUNID == URUNID).S_URUNADET++;
            }
            else
            {
                cs.S_URUNADET = quantity;


                cs.S_URUNID = URUNID;
                cs.S_URUNADI = urunadi;
                cs.S_URUNFIYAT = urunfiyat;

                cs.S_URUNRESIM = urunresimyol;
                db.C_SEPET.Add(cs);
            }
                
                db.SaveChanges();

                sepetbilgileri();

        }

        public PartialViewResult sepetbilgileri()
        {
            List<C_SEPET> d = db.C_SEPET.ToList();
            if(d.Count!=0)
            {
                ViewBag.toplam = d.Sum(a => a.S_URUNFIYAT*a.S_URUNADET);
                ViewBag.sepeturunsayisi= d.Select(a=>a.S_URUNID).Distinct().Count();

            }
            else
            {
                ViewBag.toplam = 0;
                ViewBag.sepeturunsayisi = 0;
            }
            return PartialView(d);
        }
        public void sepeturunsil(int id)
        {
            List<C_SEPET> d = db.C_SEPET.ToList();
            if (d.Any(x => x.S_URUNID == id)&&d.Any(x=>x.S_URUNADET>1))
            {
                db.C_SEPET.First(x => x.S_URUNID == id).S_URUNADET--;
                db.SaveChanges();
            }
            else
            {
                C_SEPET u = db.C_SEPET.FirstOrDefault(x => x.S_URUNID == id);
                db.C_SEPET.Remove(u);
            } 
            db.SaveChanges();
        }

        public ActionResult Sepetigoruntule()
        {

            return View();
        }



    }
}