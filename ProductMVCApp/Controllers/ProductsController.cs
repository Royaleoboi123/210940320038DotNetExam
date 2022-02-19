using ProductMVCApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductMVCApp.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            List<Product> prodList = new List<Product>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotnetTest;Integrated Security=True";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SelectOperation";
            
           

            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    Product product = new Product();
                    product.ProductId =(int) dr["ProductId"];
                    product.ProductName = dr["ProductName"].ToString();
                    product.Rate = (decimal)dr["Rate"];
                    product.Description = dr["Description"].ToString();
                    product.CategoryName = dr["CategoryName"].ToString();
                    prodList.Add(product);

                }
                dr.Close();
               
            }

            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return View(prodList);


        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotnetTest;Integrated Security=True";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "InsertOperation";
            cmd.Parameters.AddWithValue("@ProductName",product.ProductName);
            cmd.Parameters.AddWithValue("@Rate", product.Rate);
            cmd.Parameters.AddWithValue("@Description", product.Description);
            cmd.Parameters.AddWithValue("@CategoryName", product.CategoryName);
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
