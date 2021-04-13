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

        List<getCustomers> listCustomers = new List<getCustomers>();
        List<getProducts> listProducts = new List<getProducts>();



        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            con.Connect();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Customers()
        {
            getCustomers();
            return View(listCustomers);
        }
        public IActionResult Products()
        {
            getProducts();
            return View(listProducts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void getProducts()
        {
            if (listProducts.Count > 0)
            {
                listProducts.Clear();
            }
            try
            {
                con.Connect();
                com.Connection = con.Connect();
                com.CommandText = "select top 100 DimProduct.ProductKey, DimProduct.ProductAlternateKey," +
                    "DimProduct.EnglishProductName, DimProductCategory.EnglishProductCategoryName, " +
                    "DimProductSubcategory.EnglishProductSubcategoryName, DimProduct.Color," +
                    "DimProduct.EnglishDescription, DimProduct.Size, DimProduct.ListPrice," +
                    "DimProduct.StartDate, DimProduct.EndDate, DimProduct.Status, DimProduct.LargePhoto from DimProduct join DimProductCategory on DimProductCategory.ProductCategoryKey = DimProduct.ProductSubcategoryKey join DimProductSubcategory on DimProductSubcategory.ProductCategoryKey = DimProduct.ProductSubcategoryKey";

                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    listProducts.Add(new getProducts()
                    {
                        ProductKey = dr["ProductKey"].ToString(),
                        ProductAlternateKey = dr["ProductAlternateKey"].ToString(),
                        EnglishProductName = dr["EnglishProductName"].ToString(),
                        EnglishProductCategoryName = dr["EnglishProductCategoryName"].ToString(),
                        EnglishProductSubcategoryName = dr["EnglishProductSubcategoryName"].ToString(),
                        Color = dr["Color"].ToString(),
                        EnglishDescription = dr["EnglishDescription"].ToString(),
                        Size = dr["Size"].ToString(),
                        ListPrice = dr["ListPrice"].ToString(),
                        StartDate = dr["StartDate"].ToString(),
                        EndDate = dr["EndDate"].ToString(),
                        Status = dr["Status"].ToString(),
                        LargePhoto = dr["LargePhoto"].ToString(),
                    });
                }
                con.Disconnect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void getCustomers()
        {
            if (listCustomers.Count > 0)
            {
                listCustomers.Clear();
            }
            try
            {
                con.Connect();
                com.Connection = con.Connect();
                com.CommandText = "select top 100 CustomerKey, FirstName, MiddleName, LastName, BirthDate, MaritalStatus, Gender, EmailAddress, EnglishOccupation, AddressLine1, Phone  from dbo.DimCustomer";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    listCustomers.Add(new getCustomers()
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
    }
}
