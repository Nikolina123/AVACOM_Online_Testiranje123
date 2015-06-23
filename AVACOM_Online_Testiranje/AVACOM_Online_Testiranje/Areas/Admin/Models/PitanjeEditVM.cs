using AVACOM_Online_Testiranje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AVACOM_Online_Testiranje.Areas.Admin.Models
{
    public class PitanjeEditVM
    {
        public int Id { get; set; }
        public string Tekst { get; set; }
        public int Bod { get; set; }
        public string VrstaPitanja_Naziv { get; set; }
        public int VrstaPitanjaId { get; set; }
        public string Oblast_Naziv { get; set; }
        public int OblastId { get; set; }
        public List<SelectListItem> OblastiStavke { get; set;}
        public List<SelectListItem> VrstaPitanjaStavke { get; set;}
    }
}