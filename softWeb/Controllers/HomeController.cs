using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using softWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace softWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        ConnectSQLServer con = new ConnectSQLServer();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        List<getInformations> listInfo = new List<getInformations>();




        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            con.Connect();
        }

        public IActionResult Index()
        {
            getInfomationsToTable();
            return View(listInfo);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void getInfomationsToTable()
        {
            if (listInfo.Count > 0)
            {
                listInfo.Clear();
            }
            try
            {
                con.Connect();
                com.Connection = con.Connect();
                com.CommandText = "select CustomerKey, FirstName, MiddleName, LastName, BirthDate, MaritalStatus, Gender, EmailAddress, FrenchOccupation, AddressLine1, Phone  from dbo.DimCustomer";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    listInfo.Add(new getInformations()
                    {
                        CustomerKey = dr["CustomerKey"].ToString(),
                        FirstName = dr["FirstName"].ToString(),
                        MiddleName = dr["MiddleName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        BirthDate = dr["BirthDate"].ToString(),
                        MaritalStatus = dr["MaritalStatus"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        EmailAddress = dr["EmailAddress"].ToString(),
                        FrenchOccupation = dr["FrenchOccupation"].ToString(),
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
    }
}
