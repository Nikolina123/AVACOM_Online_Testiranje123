using AVACOM_Online_Testiranje.Areas.Admin.Models;
using AVACOM_Online_Testiranje.DAL;
using AVACOM_Online_Testiranje.Helper;
using AVACOM_Online_Testiranje.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;


namespace AVACOM_Online_Testiranje.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        MojContext db = new MojContext();

        //
        // GET: /Admin/Home/
        public ActionResult Index()
        {
          

            List<PitanjePrikaziVM.PitanjeInfo> pitanja = db.Pitanja.
                Select(a => new PitanjePrikaziVM.PitanjeInfo
                {

                    Id = db.Pitanja.Count()
                }).ToList();

            PitanjePrikaziVM model = new PitanjePrikaziVM
            {
                Pitanja = pitanja
            };
            return View(model);
        }


        public ActionResult TestPrikazVM()
        {
            MojContext db = new MojContext();

            List<TestPrikazVM.TestoviInfo> testovi = db.Testovi
                //.Where(c => c.KorisnikId == LogiraniKorisnik.Id)
                .Select(a => new TestPrikazVM.TestoviInfo
                {
                    Id = a.Id,
                    
                    //korisnik. = a.Korisnik.Ime,
                    VrijemePocetka = a.VrijemePocetka,
                    VrijemeZavrsetka = a.VrijemeZavrsetka,
                    Rezultat = a.Rezultat,
                    BrojTacnihOdgovora = db.TestOdgovori
                    .Where(d => d.TestId == a.Id && d.OdgovorTacan == true).Count().ToString(),
                    Oblasti = db.TestOblast.Where(b => b.TestId == a.Id)
                    .Select(c => new TestPrikazVM.OblastInfo
                    {
                        Id = c.Oblast.Id,
                        Naziv = c.Oblast.Naziv
                    }).ToList()
                }).ToList();

            TestPrikazVM model = new TestPrikazVM
            {
                Testovi = testovi
            };

            return View(model);
        }



     











        //[WebMethod]
        //public static List<Oblast> GetChartData()
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection con = new SqlConnection(@"Data Source=localhost;Integrated Security=true;Initial Catalog=AVACOM"))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT Naziv  FROM Oblast ", con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);
        //        con.Close();
        //    }
        //    List<Oblast> dataList = new List<Oblast>();
        //    foreach (DataRow dtrow in dt.Rows)
        //    {
        //        Oblast details = new Oblast();
        //        details.Naziv = dtrow[0].ToString();
        //        details.Id = Convert.ToInt32(dtrow[1]);
        //        dataList.Add(details);
        //    }
        //    return dataList;
        //}  






       
	}
}