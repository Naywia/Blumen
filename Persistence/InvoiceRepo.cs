using Blumen.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blumen.Persistence
{
    public class InvoiceRepo : Repo<Invoice>
    {
        #region Create
        public override bool AddItem(Invoice item)
        {
            OrderRepo orderRepo = new();
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = new("INSERT INTO INVOICE(InvoiceNumber,InvoiceAddress,InvoiceDate,Comment) " +
                                         "output INSERTED.InvoiceID " +
                                         "VALUES (@InvoiceNumber,@InvoiceAddress,@InvoiceDate,@Comment)", sqlConnection);
            sqlCommand.Parameters.Add("@InvoiceNumber", SqlDbType.BigInt).Value = item.InvoiceNumber;
            sqlCommand.Parameters.Add("@InvoiceAddress", SqlDbType.NVarChar).Value = item.InvoiceAddress;
            sqlCommand.Parameters.Add("@InvoiceDate", SqlDbType.DateTime2).Value = item.InvoiceDate;
            sqlCommand.Parameters.Add("@Comment", SqlDbType.NVarChar).Value = item.Comment;

            if (sqlCommand != null)
            {
                int id = (int)sqlCommand.ExecuteScalar();
                foreach (Order order in item.InvoiceOrders)
                {
                    Order updated = new()
                    {
                        OrderID = order.OrderID,
                        Products = order.Products,
                        Comment = order.Comment,
                        Price = order.Price,
                        OrderDate = order.OrderDate,
                        Delivery = order.Delivery,
                        PaymentStatus = order.PaymentStatus,
                        Card = order.Card,
                        PaymentNote = order.PaymentNote,
                        IsComplete = order.IsComplete,
                        InvoiceID = id,
                    };
                    orderRepo.UpdateItem(order, updated);
                }
                return true;
            }
            return false;
        }
        #endregion

        #region Read
        public Invoice GetInvoice()
        {
            throw new NotImplementedException();
        }

        public override ObservableCollection<Invoice> GetItems()
        {
            ObservableCollection<Invoice> items = [];
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            SqlDataReader sqlDataReader;
            sqlCommand = new("SELECT INVOICE.InvoiceID, InvoiceNumber, InvoiceAddress, InvoiceDate, INVOICE.Comment, " +
                             "OrderID, \"ORDER\".Comment, Price, OrderDate, Delivery, PaymentStatus, Card, PaymentNote " +
                             "FROM INVOICE " +
                             "LEFT JOIN \"ORDER\" ON INVOICE.InvoiceID = \"ORDER\".InvoiceID", sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            int prevInvoiceID = 0;
            Invoice temp = new();
            while (sqlDataReader.Read())
            {
                int invoiceID = int.Parse(sqlDataReader["InvoiceID"].ToString());
                if (prevInvoiceID == invoiceID)
                {
                    Order order = new()
                    {
                        OrderID = int.Parse(sqlDataReader["OrderID"].ToString()),
                        Comment = sqlDataReader[6].ToString(),
                        Price = double.Parse(sqlDataReader["Price"].ToString()),
                        OrderDate = DateTime.Parse(sqlDataReader["OrderDate"].ToString()),
                        Delivery = sqlDataReader["Delivery"].ToString(),
                        PaymentStatus = (Payment)int.Parse(sqlDataReader["PaymentStatus"].ToString()),
                        Card = sqlDataReader["Card"].ToString(),
                        PaymentNote = sqlDataReader["PaymentNote"].ToString()
                    };
                    temp.InvoiceOrders.Add(order);
                }
                else
                {
                    if (prevInvoiceID != 0)
                    {
                        items.Add(temp);
                    }

                    prevInvoiceID = invoiceID;
                    temp = new()
                    {
                        InvoiceID = invoiceID,
                        InvoiceNumber = long.Parse(sqlDataReader["InvoiceNumber"].ToString()),
                        InvoiceAddress = sqlDataReader["InvoiceAddress"].ToString(),
                        InvoiceDate = DateTime.Parse(sqlDataReader["InvoiceDate"].ToString()),
                        Comment = sqlDataReader[4].ToString(),
                        InvoiceOrders = []
                    };

                    Order order = new()
                    {
                        OrderID = int.Parse(sqlDataReader["OrderID"].ToString()),
                        Comment = sqlDataReader[6].ToString(),
                        Price = double.Parse(sqlDataReader["Price"].ToString()),
                        OrderDate = DateTime.Parse(sqlDataReader["OrderDate"].ToString()),
                        Delivery = sqlDataReader["Delivery"].ToString(),
                        PaymentStatus = (Payment)int.Parse(sqlDataReader["PaymentStatus"].ToString()),
                        Card = sqlDataReader["Card"].ToString(),
                        PaymentNote = sqlDataReader["PaymentNote"].ToString()
                    };
                    temp.InvoiceOrders.Add(order);
                }
            }
            items.Add(temp);
            return items;
        }

        private void MakeInvoice()
        {

        }
        #endregion

        #region Update
        public override bool UpdateItem(Invoice oldItem, Invoice newItem)
        {
            throw new NotSupportedException();
        }
        #endregion

        #region Delete
        public override bool RemoveItem(Invoice item)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}
