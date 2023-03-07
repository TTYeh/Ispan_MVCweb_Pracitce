﻿using Ispan_AspNetWeb_firstPractice.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Ispan_AspNetWeb_firstPractice.Controllers
{
    public class GreetingController : Controller
    {
        // GET: Greeting
        public ActionResult showById(int? id)
        {
            // http://localhost:61327/Greeting/showById/1
            if (id == null)
            {
                ViewBag.message = "沒有傳入ID參數";
            }
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    //cmd.CommandText = "SELECT * FROM Customers WHERE fId =" + id.ToString();
                    cmd.CommandText = "SELECT* FROM Customers WHERE fId = 1";
                    ViewBag.message = "查無任何資料";
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        CCustomer c = new CCustomer()
                        {
                            fId = (int) reader["fId"],
                            fName = reader["fName"].ToString(),
                            fAddress = reader["fAddress"].ToString()
                            
                        };
                        ViewBag.customer = c;

                    }
                    return View();
                }

            }

        }


        public ActionResult showView(int? id)
        {
            ViewBag.message = "Hello world";
            return View();
        }
        public string queryById(int? id)
        {
            // http://localhost:61327/Greeting/queryById/1
            if (id == null) {
                return "沒有傳入參數";
            }
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Customers WHERE fId=" + id.ToString(), con);
            SqlDataReader reader = cmd.ExecuteReader();
            string s = "查無任何資料";
            if (reader.Read())
                // 這個不行
                //s = reader["fName"].ToString() + " /r/n " + reader["fPhone"].ToString();
                s = reader["fName"].ToString() + " <br>" + reader["fPhone"].ToString();
            con.Close();
            return s;

        }
        public ActionResult Index()
        {
            return View();
        }
        public string sayHello()
        {
            //http://localhost:61327/Greeting/sayHello
            string Hello = "Hello World";
            return Hello;
        }
        public string letto() {
            // http://localhost:61327/Greeting/letto
            // 取1~49不重覆的array，總共取49個數字。
            string result = string.Empty;
            var rdm = new Random();
            int genStart = 1; //從1開始取
            int genEnd = 49; //取到49
            int takeTotal = 49; //總共取6個數字
            List<int> takeArray = new List<int>();
            // 取1~49不重覆的array，總共取49個數字。
            for (int i = 0; i < takeTotal; i++)
            {
                int rdmNum = rdm.Next(genStart, genEnd + 1);
                if (!takeArray.Contains(rdmNum))
                {
                    takeArray.Add(rdmNum);
                }
                else
                {
                    i--;
                }
            }
            result = String.Join(", ", takeArray);
            return result;
        }

        public string addToCart()
        {
            // Demo Request
            // http://localhost:61327/Greeting/addToCart/?pid=1
            string id = Request.QueryString["pid"];
            if (id == "0")
                return "XBox 加入購物車成功";
            if (id == "1")
                return "PS5 加入購物車成功";
            if (id == "2")
                return "Switch 加入購物車成功";
            return "找不到該產品資料";
        }

        public string demoParameters(int? pid)
        {
            // int? 才不會因為沒參數當掉
            // Demo Request
            // http://localhost:61327/Greeting/demoParameters/?pid=0
            if (pid == 0)
                return "XBox 加入購物車成功";
            if (pid == 1)
                return "PS5 加入購物車成功";
            if (pid == 2)
                return "Switch 加入購物車成功";
            return "找不到該產品資料";
        }

        public string demoParameterId(int? id)
        {
            // int? 才不會因為沒參數當掉
            // Demo Request
            // http://localhost:61327/Greeting/demoParameterId/1
            if (id == 0)
                return "XBox 加入購物車成功";
            if (id == 1)
                return "PS5 加入購物車成功";
            if (id == 2)
                return "Switch 加入購物車成功";
            return "找不到該產品資料";
        }

        public string demoServer()
        {
            return "目前伺服器上的實體位置：" + Server.MapPath(".");
            // C:\Users\User\Documents\Github\Ispan_MVCweb_pracitce\Ispan_AspNetWeb_firstPractice\Ispan_AspNetWeb_firstPractice\Ispan_AspNetWeb_firstPractice\Greeting
        }
        public string demoResponse()
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.Filter.Close();
            Response.WriteFile(@"C:\Users\User\Documents\Github\Ispan_MVCweb_pracitce\Ispan_AspNetWeb_firstPractice\Ispan_AspNetWeb_firstPractice\Ispan_AspNetWeb_firstPractice\Resource\images\01.jpg");
            Response.End();
            return "";
        }

    }
}