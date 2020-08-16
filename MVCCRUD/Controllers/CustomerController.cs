using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCCRUD.Models;

namespace MVCCRUD.Controllers
{
    public class CustomerController : Controller
    {
        string connectionString = @"Data Source =DESKTOP-URMJPGU; Initial Catalog =MVCCrudDB; Integrated Security = True";
        // GET: Customer
        public ActionResult List()
        {
            DataTable dtblCustomer = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Customer", sqlCon);
                sqlDa.Fill(dtblCustomer);
            }
                return View(dtblCustomer);
        }

        [HttpGet]
        // GET: Customer/Create
        public ActionResult Create()
        {
            return View(new CustomerModel());
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerModel customerModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "Insert Into Customer VALUES (@CustomerName,@CustomerAddress,@PhoneNo)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@CustomerName", customerModel.CustomerName);
                sqlCmd.Parameters.AddWithValue("@CustomerAddress", customerModel.CustomerAddress);
                sqlCmd.Parameters.AddWithValue("@PhoneNo", customerModel.PhoneNo);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("List");
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            CustomerModel customerModel = new CustomerModel();
            DataTable dtblCustomer = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "SELECT * FROM Customer Where CustomerID = @CustomerID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@CustomerID", id);
                sqlDa.Fill(dtblCustomer);
            }
            if (dtblCustomer.Rows.Count == 1)
            {
                customerModel.CustomerID = Convert.ToInt32(dtblCustomer.Rows[0][0].ToString());
                customerModel.CustomerName = dtblCustomer.Rows[0][1].ToString();
                customerModel.CustomerAddress = dtblCustomer.Rows[0][2].ToString();
                customerModel.PhoneNo = Convert.ToInt32(dtblCustomer.Rows[0][3].ToString());
                return View(customerModel);
            }
            else
                return RedirectToAction("List");
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerModel customerModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE Customer SET CustomerName = @CustomerName , CustomerAddress= @CustomerAddress , PhoneNo = @PhoneNo WHere CustomerID = @CustomerID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@CustomerID", customerModel.CustomerID);
                sqlCmd.Parameters.AddWithValue("@CustomerName", customerModel.CustomerName);
                sqlCmd.Parameters.AddWithValue("@CustomerAddress", customerModel.CustomerAddress);
                sqlCmd.Parameters.AddWithValue("@PhoneNo", customerModel.PhoneNo);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("List");
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "Delete FROM Customer Where CustomerID = @CustomerID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@CustomerID", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("List");
        }
    }
}