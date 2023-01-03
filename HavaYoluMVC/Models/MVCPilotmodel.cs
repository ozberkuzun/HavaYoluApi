using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HavaYoluMVC.Models
{
    public class MVCPilotmodel
    {
        public int PilotNo { get; set; }
        [Required(ErrorMessage = "Sirket Adı Girilmesi Zorunludur")]

        public string PilotAdi { get; set; }

        public int PilotYas { get; set; }

        public int? PilotTecrube { get; set; } //soru işareti boş geçebilmemiz için koyuyoruz.

        public int? UcakNo { get; set; }
    }
}