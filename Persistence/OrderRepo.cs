using Blumen.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;

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
            //return repo;
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
    }
}
