using AVACOM_Online_Testiranje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AVACOM_Online_Testiranje.Areas.Admin.Models
{
    public class TestPrikazVM
    {

           public class OblastInfo
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
        }
        public class TestoviInfo
        {
            public int Id { get; set; }
            public DateTime VrijemePocetka { get; set; }
            public DateTime VrijemeZavrsetka { get; set; }
            public List<OblastInfo> Oblasti { get; set; }
            public string BrojTacnihOdgovora { get; set; }
            public float Rezultat { get; set; }
            public Korisnik korisnik { get; set; }
        }

        public List<TestoviInfo> Testovi { get; set; }
    }

        

    }
