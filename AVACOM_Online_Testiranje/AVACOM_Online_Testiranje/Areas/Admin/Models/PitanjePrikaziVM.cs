using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AVACOM_Online_Testiranje.Areas.Admin.Models
{
    public class PitanjePrikaziVM
    {
        public class PitanjeInfo
        {
            public int Id { get; set; }
            public string Tekst { get; set; }
            public int Bod { get; set; }
            public string VrtaPitanja_Naziv { get; set; }
            public string Oblst_Naziv { get; set; }
        }

        public List<PitanjeInfo> Pitanja { get; set; }
        public List<SelectListItem> oblastiStavke { get; set; }
        public int OblastId { get; set; }
        public List<SelectListItem> oblastivrstapitanja { get; set; }
        public int VrstaPitanjaId { get; set; }
    }
}