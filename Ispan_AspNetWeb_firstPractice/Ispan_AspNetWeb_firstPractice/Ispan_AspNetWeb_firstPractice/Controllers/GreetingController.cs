using Ispan_AspNetWeb_firstPractice.Models;
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
        CCustomerRepo repo = new CCustomerRepo();
        public ActionResult DemoUQE() {
            ViewBag.Ans = "?";
            if (!string.IsNullOrEmpty(Request.Form["txtA"]))
            {
                double a = Convert.ToInt32(Request.Form["txtA"]);
                double b = Convert.ToInt32(Request.Form["txtB"]);
                double c = Convert.ToInt32(Request.Form["txtC"]);
                double r = b * b - 4 * a * c;
                ViewBag.a = a;
                ViewBag.b = b;
                ViewBag.c = c;
                r = Math.Sqrt(r);

                ViewBag.Ans = ((-b + r) / (2 * a)).ToString("0.0#") + " Or X=" + ((-b - r) / (2 * a)).ToString();
            }
            return View();
        }
        public ActionResult DemoAdd() {
            ViewBag.ANS = "?";
            if (Request.Form["num1"] != null || Request.Form["num1"] != string.Empty)
            { 
                double a = Convert.ToDouble(Request.Form["num1"]);
                double b = Convert.ToDouble(Request.Form["num2"]);
                ViewBag.ANS = a + b;
            }
            return View();
        }
        
        // GET: Greeting
        public string Update(int? id)
        {
            if (id == null)
            {
                return "沒有傳入參數";
            }
            CCustomerEntity entity = new CCustomerEntity()
            {
                fId = (int)id,
                fName = "隔壁老王",
                fPhone = "09878787",
                fEmail = "8787@gmail.com",
                fAddress = "Taiwan",
                fPassword = "x123456"
            };
            int newId = repo.Update(entity);
            return $"更新成功?{newId}";
        }
        public string Create()
        {
            CCustomerEntity entity = new CCustomerEntity()
            {
                fName = "王小明",
                fPhone = "0912345678",
                fEmail = "123@gmail.com",
                fAddress = "Taiwan",
                fPassword = "x123456"
            };
            int newId = repo.Create(entity);
            return $"新增資料成功?{newId}";
        }
        public string Delete(int? id)
        {
            {
                int rowAffected = 0;
                if (id != null)
                {
                    rowAffected = repo.Delete((int)id);
                }
                if (rowAffected > 0)
                {
                    ViewBag.message = $"刪除成功，被影響資料:{rowAffected}";
                }
                else
                {
                    ViewBag.message = "刪除失敗";
                }
            }
            return ViewBag.message;
        }
        public string Search(int? id)
        {
            if (id == null)
            {
                return "沒有傳入參數";
            }
            CCustomerEntity entity = repo.GetById((int)id);
            if (entity == null)
            {
                return "查無資料";
            }
            return entity.ToString();
        }

        //public List<CCustomerEntity> GetAll()
        public string GetAll()
        {
            var result = new List<CCustomerEntity>();
            string temp = string.Empty;
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT * FROM Customers";
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CCustomerEntity c = new CCustomerEntity()
                        {
                            fId = (int)reader["fId"],
                            fName = reader["fName"].ToString(),
                            fPhone = reader["fPhone"].ToString(),
                            fEmail = reader["fEmail"].ToString(),
                            fAddress = reader["fAddress"].ToString(),
                            fPassword = reader["fPassword"].ToString()
                        };
                        result.Add(c);
                        temp += c.ToString();
                        temp += "<br>";
                    }
                }
            }
            return temp;
        } 
        

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
                        CCustomerEntity c = new CCustomerEntity()
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