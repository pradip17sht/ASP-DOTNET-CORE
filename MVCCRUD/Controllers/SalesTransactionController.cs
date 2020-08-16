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
    public class SalesTransactionController : Controller
    {
        string connectionString = @"Data Source =DESKTOP-URMJPGU; Initial Catalog =MVCCrudDB; Integrated Security = True";
        [HttpGet]
        // GET: SalesTransaction
        public ActionResult List()
        {
            DataTable dtblSalesTransaction = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Sales_Transaction", sqlCon);
                sqlDa.Fill(dtblSalesTransaction);
            }
            return View(dtblSalesTransaction);
        }

        [HttpGet]
        // GET: SalesTransaction/Create
        public ActionResult Create()
        {
            return View(new SalesTransactionModel());
        }

        // POST: SalesTransaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SalesTransactionModel salesTransactionModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "Insert Into Sales_Transaction VALUES (@TransactionName,@Code,@Amount)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@TransactionName", salesTransactionModel.TransactionName);
                sqlCmd.Parameters.AddWithValue("@Code", salesTransactionModel.Code);
                sqlCmd.Parameters.AddWithValue("@Amount", salesTransactionModel.Amount);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("List");
        }

        // GET: SalesTransaction/Edit/5
        public ActionResult Edit(int id)
        {
            SalesTransactionModel salesTransactionModel = new SalesTransactionModel();
            DataTable dtblSalesTransaction = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "SELECT * FROM Sales_Transaction Where TransactionID = @TransactionID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@TransactionID", id);
                sqlDa.Fill(dtblSalesTransaction);
            }
            if (dtblSalesTransaction.Rows.Count == 1)
            {
                salesTransactionModel.TransactionID = Convert.ToInt32(dtblSalesTransaction.Rows[0][0].ToString());
                salesTransactionModel.TransactionName = dtblSalesTransaction.Rows[0][1].ToString();
                salesTransactionModel.Code = Convert.ToInt32(dtblSalesTransaction.Rows[0][3].ToString());
                salesTransactionModel.Amount = Convert.ToDecimal(dtblSalesTransaction.Rows[0][2].ToString());
                return View(salesTransactionModel);
            }
            else
                return RedirectToAction("List");
        }

        // POST: SalesTransaction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SalesTransactionModel salesTransactionModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE Sales_Transaction SET TransactionName = @TransactionName , Code= @Code , Amount = @Amount WHere TransactionID = @TransactionID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@TransactionID", salesTransactionModel.TransactionID);
                sqlCmd.Parameters.AddWithValue("@TransactionName", salesTransactionModel.TransactionName);
                sqlCmd.Parameters.AddWithValue("@Code", salesTransactionModel.Code);
                sqlCmd.Parameters.AddWithValue("@Amount", salesTransactionModel.Amount);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("List");
        }

        // GET: SalesTransaction/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "Delete FROM Sales_Transaction Where TransactionID = @TransactionID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@TransactionID", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("List");
        }
    }
}