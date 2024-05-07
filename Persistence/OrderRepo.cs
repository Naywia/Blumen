using Blumen.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Blumen.Persistence
{
    public class OrderRepo : Repo<Order>
    {
        public Order GetOrder(int orderNumber)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(DateTime orderDate)
        {
            throw new NotImplementedException();
        }

        public override ObservableCollection<Order> GetItems()
        {
            ObservableCollection<Order> items = new() { };
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            SqlDataReader sqlDataReader;
            sqlCommand = new("SELECT OrderID, Comment, Price, OrderDate, Delivery, PaymentStatus, Card, PaymentNote FROM \"ORDER\"", sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Order temp = new()
                {
                    OrderID = int.Parse(sqlDataReader["OrderID"].ToString()),
                    Comment = sqlDataReader["Comment"].ToString(),
                    Price = double.Parse(sqlDataReader["Price"].ToString()),
                    OrderDate = DateTime.Parse(sqlDataReader["OrderDate"].ToString()),
                    Delivery = sqlDataReader["Delivery"].ToString(),
                    PaymentStatus = (Payment)int.Parse(sqlDataReader["PaymentStatus"].ToString()),
                    Card = sqlDataReader["Card"].ToString(),
                    PaymentNote = sqlDataReader["PaymentNote"].ToString()
                };
                items.Add(temp);
            }
            return items;
        }

        public ObservableCollection<Order> GetOrdersNotInInvoice(int customerID)
        {
            ObservableCollection<Order> items = new() { };
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            SqlDataReader sqlDataReader;
            sqlCommand = new("SELECT OrderID, Comment, Price, OrderDate, Delivery, PaymentStatus, Card, PaymentNote FROM \"ORDER\" WHERE CustomerID = @CustomerID AND InvoiceID = NULL", sqlConnection);
            sqlCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customerID;
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Order temp = new()
                {
                    OrderID = int.Parse(sqlDataReader["OrderID"].ToString()),
                    Comment = sqlDataReader["Comment"].ToString(),
                    Price = double.Parse(sqlDataReader["Price"].ToString()),
                    OrderDate = DateTime.Parse(sqlDataReader["OrderDate"].ToString()),
                    Delivery = sqlDataReader["Delivery"].ToString(),
                    PaymentStatus = (Payment)int.Parse(sqlDataReader["PaymentStatus"].ToString()),
                    Card = sqlDataReader["Card"].ToString(),
                    PaymentNote = sqlDataReader["PaymentNote"].ToString()
                };
                items.Add(temp);
            }
            return items;
        }
    }
}
