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
    public class InvoiceController : Controller
    {
        string connectionString = @"Data Source =DESKTOP-URMJPGU; Initial Catalog =MVCCrudDB; Integrated Security = True";
        [HttpGet]
        // GET: Invoice
        public ActionResult List()
        {
            DataTable dtblInvoice = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Invoice", sqlCon);
                sqlDa.Fill(dtblInvoice);
            }
            return View(dtblInvoice);
        }

        [HttpGet]
        // GET: Invoice/Create
        public ActionResult Create()
        {
            return View(new InvoiceModel());
        }

        // POST: Invoice/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InvoiceModel invoiceModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "Insert Into Invoice VALUES (@Items,@Description,@Quantity,@Price,@Tax,@Amount)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Items", invoiceModel.Items);
                sqlCmd.Parameters.AddWithValue("@Description", invoiceModel.Description);
                sqlCmd.Parameters.AddWithValue("@Quantity", invoiceModel.Quantity);
                sqlCmd.Parameters.AddWithValue("@Price", invoiceModel.Price);
                sqlCmd.Parameters.AddWithValue("@Tax", invoiceModel.Tax);
                sqlCmd.Parameters.AddWithValue("@Amount", invoiceModel.Amount);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("List");
        }

        // GET: Invoice/Edit/5
        public ActionResult Edit(int id)
        {
            InvoiceModel invoiceModel = new InvoiceModel();
            DataTable dtblInvoice = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "SELECT * FROM Invoice Where InvoiceID = @InvoiceID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@InvoiceID", id);
                sqlDa.Fill(dtblInvoice);
            }
            if (dtblInvoice.Rows.Count == 1)
            {
                invoiceModel.InvoiceID = Convert.ToInt32(dtblInvoice.Rows[0][0].ToString());
                invoiceModel.Items = dtblInvoice.Rows[0][1].ToString();
                invoiceModel.Description = dtblInvoice.Rows[0][2].ToString();
                invoiceModel.Quantity = Convert.ToInt32(dtblInvoice.Rows[0][3].ToString());
                invoiceModel.Price = Convert.ToDecimal(dtblInvoice.Rows[0][4].ToString());
                invoiceModel.Tax = Convert.ToDecimal(dtblInvoice.Rows[0][5].ToString());
                invoiceModel.Amount = Convert.ToDecimal(dtblInvoice.Rows[0][6].ToString());
                return View(invoiceModel);
            }
            else
                return RedirectToAction("List");
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InvoiceModel invoiceModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE Invoice SET Items = @Items , Description= @Description , Quantity = @Quantity, Price = @Price,Tax = @Tax,Amount = @Amount WHere InvoiceID = @InvoiceID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@InvoiceID", invoiceModel.InvoiceID);
                sqlCmd.Parameters.AddWithValue("@Items", invoiceModel.Items);
                sqlCmd.Parameters.AddWithValue("@Description", invoiceModel.Description);
                sqlCmd.Parameters.AddWithValue("@Quantity", invoiceModel.Quantity);
                sqlCmd.Parameters.AddWithValue("@Price", invoiceModel.Price);
                sqlCmd.Parameters.AddWithValue("@Tax", invoiceModel.Tax);
                sqlCmd.Parameters.AddWithValue("@Amount", invoiceModel.Amount);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("List");
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "Delete FROM Invoice Where InvoiceID = @InvoiceID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@InvoiceID", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("List");
        }
    }
}