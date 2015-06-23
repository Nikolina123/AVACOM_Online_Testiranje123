using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AVACOM_Online_Testiranje.Areas.Admin.Models
{
    public class OdgovorPrikazi
    {
        public class OdgovorInfo 
        {
            public int Id { get; set; }
            public string Tekst { get; set; }
            public bool Tacan { get; set; }
            public string Pitanje_Naziv { get; set; }
        
        }
        public List<OdgovorInfo> odgovori { get; set;}

        public List<SelectListItem> pitanja { get; set; }

        public int PitanjeId { get; set; }
       

    }
}