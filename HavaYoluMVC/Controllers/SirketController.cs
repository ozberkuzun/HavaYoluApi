using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;
using HavaYoluMVC.Models;

namespace HavaYoluMVC.Controllers
{
    public class SirketController : Controller
    {
        // GET: Sirket
        public ActionResult Index()
        {
            IEnumerable<MVCSirketmodel> calList;
            HttpResponseMessage response = GlobalVariables.WebApClient.GetAsync("Sirkets").Result; //apideki controllerin adı olacak.
            calList = response.Content.ReadAsAsync<IEnumerable<MVCSirketmodel>>().Result;



            return View(calList);
        }


        public ActionResult Ekle(int id = 0)          //crup işlemi:bir metotta iki işlemi yaptırıyoruz.
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApClient.GetAsync("Sirkets/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MVCSirketmodel>().Result);
            }
        }

        [HttpPost]
        public ActionResult Ekle(MVCSirketmodel sirket)//crup
        {
            if (sirket.SirketNo == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApClient.PostAsJsonAsync("Sirkets", sirket).Result;
                TempData["SuccessMessage"] = "başarılı bir şekilde eklendi";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApClient.PutAsJsonAsync("Sirkets/" + sirket.SirketNo, sirket).Result;
                TempData["SuccessMessage"] = "başarılı bir şekilde güncellendi";
            }
            return RedirectToAction("Index");
        }

        public ActionResult sil(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApClient.DeleteAsync("Sirkets/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "başarılı bir şekilde silindi";
            return RedirectToAction("Index");
        }

    }
}