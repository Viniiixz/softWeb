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
    public class ProductsController : Controller
    {
        ConnectSQLServer con = new ConnectSQLServer();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        string queryProduct = "select top 10 DimProduct.ProductKey, DimProduct.ProductAlternateKey," +
                    "DimProduct.EnglishProductName, DimProductCategory.EnglishProductCategoryName, " +
                    "DimProductSubcategory.EnglishProductSubcategoryName, DimProduct.Color," +
                    "DimProduct.EnglishDescription, DimProduct.Size, DimProduct.ListPrice," +
                    "DimProduct.StartDate, DimProduct.EndDate, DimProduct.Status, DimProduct.LargePhoto from DimProduct join DimProductCategory on DimProductCategory.ProductCategoryKey = DimProduct.ProductSubcategoryKey join DimProductSubcategory on DimProductSubcategory.ProductCategoryKey = DimProduct.ProductSubcategoryKey";


        List<getProducts> listProducts = new List<getProducts>();

        // GET: ProductsController
        public ActionResult Index()
        {
            getProducts();
            return View(listProducts);
        }

        public ActionResult Products()
        {
            getProducts();
            return View(listProducts);
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
                com.CommandText = queryProduct;
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

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
            {
                return View();
            }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
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

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductsController/Edit/5
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

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsController/Delete/5
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
