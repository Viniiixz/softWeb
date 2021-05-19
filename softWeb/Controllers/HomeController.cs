using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using softWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Database;

namespace softWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        ConnectSQLServer con = new ConnectSQLServer();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        List<getCustomers> listCountCustomer = new List<getCustomers>();

        string queryCountCustomer = "select count(CustomerKey) from DimCustomer";

        public void getCountCustomer()
        {
            if (listCountCustomer.Count > 0)
            {
                listCountCustomer.Clear();
            }
            try
            {
                con.Connect();
                com.Connection = con.Connect();
                com.CommandText = queryCountCustomer;
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    listCountCustomer.Add(new getCustomers()
                    {
                        CustomerKey = dr["CustomerKey"].ToString(),
                        FirstName = dr["FirstName"].ToString(),
                        MiddleName = dr["MiddleName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        BirthDate = dr["BirthDate"].ToString(),
                        MaritalStatus = dr["MaritalStatus"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        EmailAddress = dr["EmailAddress"].ToString(),
                        EnglishOccupation = dr["EnglishOccupation"].ToString(),
                        AddressLine1 = dr["AddressLine1"].ToString(),
                        Phone = dr["Phone"].ToString()
                    });
                }
                con.Disconnect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            getCountCustomer();
            return View(listCountCustomer);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
