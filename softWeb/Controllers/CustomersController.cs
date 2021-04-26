using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using softWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace softwareWeb.Controllers
{
    public class CustomersController : Controller
    {
        ConnectSQLServer con = new ConnectSQLServer();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        string queryCustomer = "select top 10 CustomerKey, FirstName, MiddleName, LastName, BirthDate, MaritalStatus, Gender, EmailAddress, EnglishOccupation, AddressLine1, Phone  from dbo.DimCustomer";

        List<getCustomers> listCustomers = new List<getCustomers>();

        // GET: CustomersController
        public ActionResult Index()
        {
            getCustomers();
            return View(listCustomers);
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
                com.CommandText = queryCustomer;
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


        // GET: CustomersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
