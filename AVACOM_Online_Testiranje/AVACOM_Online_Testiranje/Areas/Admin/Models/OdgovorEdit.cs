using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AVACOM_Online_Testiranje.Areas.Admin.Models
{
    public class OdgovorEdit
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Tekst { get; set; }
        public bool Tacan { get; set; }
        public int PitanjeId { get; set; }
        public string Pitanje_Naziv { get; set; }
        public List<SelectListItem> pitanjestavke { get; set; }
    }
}