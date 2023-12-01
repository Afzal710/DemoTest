using DemoTest.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoTest.Controllers
{
    public class HomeController : Controller
    {
        ADOLayer ado = new ADOLayer();
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Index(CountryModel m)
        {
            int res = 0;
            if(m.country_id==0)
            {
                SqlParameter[] para = new SqlParameter[]
                {
                new SqlParameter("action",1),
                new SqlParameter("country_name",m.country_name)
                };
                res = ado.ExecuteDML("sp_country", para);

            }
            else
            {
                SqlParameter[] para = new SqlParameter[]
                {
                new SqlParameter("action",4),
                new SqlParameter("country_id",m.country_id),
                new SqlParameter("country_name",m.country_name)
                };
                res = ado.ExecuteDML("sp_country", para);
            }
        
            if (res > 0)
                return Json("Add Country", JsonRequestBehavior.AllowGet);
            else
                return Json ("Country not Add",JsonRequestBehavior.AllowGet);
        }
        //      //      //  Select Country   //      //      //  
        [HttpPost]
        public ActionResult SelectCountry()
        {
            SqlParameter[] para = new SqlParameter[]
            {
            new SqlParameter ("@action",2)
            };
            DataTable dt = ado.DataDisplay("sp_country", para);
            List<CountryModel> list = new List<CountryModel>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new CountryModel()
                {
                    country_id = int.Parse(row["country_id"].ToString()),
                    country_name = row["country_name"].ToString()
                });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //      //      //  Edit Country   //      //      //  
        [HttpPost]
        public ActionResult editcountryrecord(int sr)
        {
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter ("action",3),
                new SqlParameter("@country_id",sr)
            };
            DataTable dt = ado.DataDisplay("sp_country", para);
            CountryModel mod = new CountryModel()
            {
                country_id = Convert.ToInt32(dt.Rows[0]["country_id"]),
                country_name = dt.Rows[0]["country_name"].ToString()
            };
            return Json(mod, JsonRequestBehavior.AllowGet);
        }
        //      //    Delete Country   //      //       //      
        public ActionResult RecordDeleted(int? sr)
        {
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("action",5),
                new SqlParameter("@country_id",sr)
            };
            int res = ado.ExecuteDML("sp_country", para);
            return Json("Record Deleted", JsonRequestBehavior.AllowGet);
        }
        
        //      //    Bind Country   //      //       //      
        public ActionResult State()
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("action",2)
            };
            DataTable dt = ado.DataDisplay("sp_country", parameters);
            ViewBag.data = dt;
            return View();
        }
        //      //    Add State   //      //      //      

        [HttpPost]
        public ActionResult addstate(StateModel s)
        {
            int res;
            if(s.state_id==0)
            {
                SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter ("action",1),
                new SqlParameter ("state_name",s.state_name),
                new SqlParameter ("country_id",s.country_name)
            };
                res = ado.ExecuteDML("sp_state", para);
            }
            else
            {
                SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter ("action",4),
                new SqlParameter("state_id",s.state_id),
                new SqlParameter ("state_name",s.state_name),
                new SqlParameter ("country_id",s.country_name)
            };
                res = ado.ExecuteDML("sp_state", para);
            }           
            if (res > 0)
                return Json("Add State", JsonRequestBehavior.AllowGet);
            else
                return Json("State not add", JsonRequestBehavior.AllowGet);
        }
        //      //      //  Select State   //      //      //  
        [HttpPost]
        public ActionResult SelectState()
        {
            SqlParameter[] para = new SqlParameter[]
            {
            new SqlParameter ("@action",2)
            };
            DataTable dt = ado.DataDisplay("sp_state", para);
            List<StateModel> list = new List<StateModel>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new StateModel()
                {
                    state_id = int.Parse(row["state_id"].ToString()),
                    state_name = row["state_name"].ToString(),
                    //country_id = int.Parse(row["country_name"].ToString()),
                    country_name = row["country_name"].ToString()
                });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //      //      //  Delete State   //      //      //  
        public ActionResult StateDeleted(int? sr)
        {
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("action",5),
                new SqlParameter("@state_id",sr)
            };
            int res = ado.ExecuteDML("sp_state", para);
            return Json("Record Deleted", JsonRequestBehavior.AllowGet);
        }
        //      //      //  Edit State   //      //      //  
        [HttpPost]
        public ActionResult editstaterecord(int? sr)
        {
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter ("action",3),
                new SqlParameter("@state_id",sr)
            };
            DataTable dt = ado.DataDisplay("sp_state", para);
            StateModel mod = new StateModel()
            {
                state_id = Convert.ToInt32(dt.Rows[0]["state_id"]),
                state_name = dt.Rows[0]["state_name"].ToString(),
                country_id = Convert.ToInt32(dt.Rows[0]["country_id"]),
            };
            return Json(mod, JsonRequestBehavior.AllowGet);
        }
    }
}