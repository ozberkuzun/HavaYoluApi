using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HavaYoluMVC.Models
{
    public class MVCSirketmodel
    {
        public int SirketNo { get; set; }
        [Required(ErrorMessage = "Sirket Adı Girilmesi Zorunludur")]

        public string SirketAdi { get; set; }

        public string Sirketİl { get; set; }

        public int? SirketKurulus { get; set; } //soru işareti boş geçebilmemiz için koyuyoruz.

    }
}