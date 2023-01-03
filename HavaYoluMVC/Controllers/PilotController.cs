using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavaYoluMVC.Models;
using System.Net.Http;

namespace HavaYoluMVC.Controllers
{
    public class PilotController : Controller
    {
        // GET: Pilot
        public ActionResult Index()
        {
            IEnumerable<MVCPilotmodel> calList;
            HttpResponseMessage response = GlobalVariables.WebApClient.GetAsync("Pilots").Result; //apideki controllerin adı olacak.
            calList = response.Content.ReadAsAsync<IEnumerable<MVCPilotmodel>>().Result;


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
                HttpResponseMessage response = GlobalVariables.WebApClient.GetAsync("Pilots/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MVCPilotmodel>().Result);
            }
        }

        [HttpPost]
        public ActionResult Ekle(MVCPilotmodel pilot)//crup
        {
            if (pilot.PilotNo == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApClient.PostAsJsonAsync("Pilots", pilot).Result;
                TempData["SuccessMessage"] = "başarılı bir şekilde eklendi";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApClient.PutAsJsonAsync("Pilots/" + pilot.PilotNo, pilot).Result;
                TempData["SuccessMessage"] = "başarılı bir şekilde güncellendi";
            }
            return RedirectToAction("Index");
        }

        public ActionResult sil(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApClient.DeleteAsync("Pilots/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "başarılı bir şekilde silindi";
            return RedirectToAction("Index");
        }
    }
}