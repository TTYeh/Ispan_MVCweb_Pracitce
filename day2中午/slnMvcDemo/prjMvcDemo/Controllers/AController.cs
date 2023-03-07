using prjMauiDemo.Models;
using prjMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class AController : Controller
    {
        public ActionResult() 
        {
            
        }


        //public string testingInsert()
        //{
        //    CCustomer x = new CCustomer()
        //    {
        //        fId = 1,
        //        fName = "John",
        //        fPhone = "0912345678"
        //    };
        //    (new CCustomer)


        //}




        public ActionResult showByIdBinding(int? id)
        {
            CCustomer x = null;
            if (id != null)
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM tCustomer WHERE fId=" + id.ToString(),
                    con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    x = new CCustomer()
                    {
                        fId = (int)reader["fId"],
                        fName = reader["fName"].ToString(),
                        fPhone = reader["fPhone"].ToString()
                    };
                }
                con.Close();
            }

            return View(x);
        }
        public ActionResult showById(int? id)
        {
            if (id != null)
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM tCustomer WHERE fId=" + id.ToString(),
                    con);
                SqlDataReader reader = cmd.ExecuteReader();
               
                if (reader.Read())
                {
                    CCustomer x = new CCustomer()
                    {
                        fId=(int)reader["fId"],
                        fName=reader["fName"].ToString(),
                        fPhone=reader["fPhone"].ToString()
                    };
                    ViewBag.KK = x;
                }
                con.Close();
            }

            return View();
        }
        public string queryById(int? id)
        {
            if (id == null)
                return "沒有指定id";

            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM tCustomer WHERE fId=" + id.ToString(),
                con);
            SqlDataReader reader = cmd.ExecuteReader();
            string s = "查無任何資料";
            if (reader.Read())
                s = reader["fName"].ToString() + "<br/>" + reader["fPhone"].ToString();
            con.Close();
            return s;
        }
        public string demoServer()
        {
            return "目前伺服器上的實體位置：" + Server.MapPath(".");
        }
        public string demoParameter(int? id)
        {

            if (id == null)
                return "沒有指定id";
            if (id == 0)
                return "XBox 加入購物車成功";
            else if (id == 1)
                return "PS5 加入購物車成功";
            else if (id == 2)
                return "Nintendo Switch 加入購物車成功";
            return "找不到該產品資料";
        }

        public string demoRequest()
        {
            string id = Request.QueryString["pid"];
            if (id == "0")
                return "XBox 加入購物車成功";
            else if (id == "1")
                return "PS5 加入購物車成功";
            else if (id == "2")
                return "Nintendo Switch 加入購物車成功";
            return "找不到該產品資料";
        }

        public string demoResponse()
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.Filter.Close();
            Response.WriteFile(@"C:\QNote\8000.jpg");
            Response.End();
            return "";
        }

        public string sayHello()
        {
            return "Hello Asp.Net MVC";
        }
        [NonAction]

        public string lotto()
        {
            return (new CLottoGen()).getNumber();
        }
        // GET: A
        public ActionResult Index()
        {
            return View();
        }
    }
}