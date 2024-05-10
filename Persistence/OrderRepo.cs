using Blumen.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Blumen.Persistence
{
    public class OrderRepo : Repo<Order>
    {
        #region Create
        public override bool AddItem(Order item)
        {
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = new("INSERT INTO \"ORDER\"(Comment,Price,OrderDate,Delivery,PaymentStatus,Card,PaymentNote) " +
                                         "VALUES (@Comment,@Price,@OrderDate,@Delivery,@PaymentStatus,@Card,@PaymentNote)", sqlConnection);
            sqlCommand.Parameters.Add("@Comment", SqlDbType.NVarChar).Value = item.Comment;
            sqlCommand.Parameters.Add("@Price", SqlDbType.Float).Value = item.Price;
            sqlCommand.Parameters.Add("@OrderDate", SqlDbType.DateTime2).Value = item.OrderDate;
            sqlCommand.Parameters.Add("@Delivery", SqlDbType.NVarChar).Value = item.Delivery;
            sqlCommand.Parameters.Add("@PaymentStatus", SqlDbType.Int).Value = (int)item.PaymentStatus;
            sqlCommand.Parameters.Add("@Card", SqlDbType.NVarChar).Value = item.Card;
            sqlCommand.Parameters.Add("@PaymentNote", SqlDbType.NVarChar).Value = item.PaymentNote;
            if (sqlCommand != null)
            {
                int result = sqlCommand.ExecuteNonQuery();
                return result > 0;
            }
            return false;
        }
        #endregion

        #region Read
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
        #endregion

        #region Update
        public override bool UpdateItem(Order oldItem, Order newItem)
        {
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            if (JsonConvert.SerializeObject(oldItem) == JsonConvert.SerializeObject(newItem))
            {
                return false;
            }
            string command = "UPDATE \"ORDER\" SET ";
            sqlCommand = new("", sqlConnection);

            if (oldItem.Comment != newItem.Comment)
            {
                command += "Comment = @Comment";
                sqlCommand.Parameters.Add("@Comment", SqlDbType.NVarChar).Value = newItem.Comment;
            }
            if (oldItem.Price != newItem.Price)
            {
                if (command.Contains('='))
                {
                    command += ", ";
                }
                command += "Price = @Price";
                sqlCommand.Parameters.Add("@Price", SqlDbType.Float).Value = newItem.Price;
            }
            if (oldItem.OrderDate != newItem.OrderDate)
            {
                if (command.Contains('='))
                {
                    command += ", ";
                }
                command += "OrderDate = @OrderDate";
                sqlCommand.Parameters.Add("@OrderDate", SqlDbType.DateTime2).Value = newItem.OrderDate;
            }
            if (oldItem.Delivery != newItem.Delivery)
            {
                if (command.Contains('='))
                {
                    command += ", ";
                }
                command += "Delivery = @Delivery";
                sqlCommand.Parameters.Add("@Delivery", SqlDbType.NVarChar).Value = newItem.Delivery;
            }
            if (oldItem.PaymentStatus != newItem.PaymentStatus)
            {
                if (command.Contains('='))
                {
                    command += ", ";
                }
                command += "PaymentStatus = @PaymentStatus";
                sqlCommand.Parameters.Add("@PaymentStatus", SqlDbType.Int).Value = newItem.PaymentStatus;
            }
            if (oldItem.Card != newItem.Card)
            {
                if (command.Contains('='))
                {
                    command += ", ";
                }
                command += "Card = @Card";
                sqlCommand.Parameters.Add("@Card", SqlDbType.NVarChar).Value = newItem.Card;
            }
            if (oldItem.PaymentNote != newItem.PaymentNote)
            {
                if (command.Contains('='))
                {
                    command += ", ";
                }
                command += "PaymentNote = @PaymentNote";
                sqlCommand.Parameters.Add("@PaymentNote", SqlDbType.NVarChar).Value = newItem.PaymentNote;
            }
            if (oldItem.IsComplete != newItem.IsComplete)
            {
                if (command.Contains('='))
                {
                    command += ", ";
                }
                command += "IsComplete = @IsComplete";
                sqlCommand.Parameters.Add("@IsComplete", SqlDbType.Bit).Value = newItem.IsComplete;
            }
            command += " WHERE OrderID = @OrderID";
            sqlCommand.CommandText = command;

            sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int).Value = newItem.OrderID;
            if (sqlCommand != null)
            {
                int result = sqlCommand.ExecuteNonQuery();
                return result > 0;
            }
            return false;
        }
        #endregion

        #region Delete
        public override bool RemoveItem(Order item)
        {
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = new("DELETE \"ORDER\" FROM \"ORDER\" WHERE OrderID = @OrderID", sqlConnection);
            sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int).Value = item.OrderID;
            if (sqlCommand != null)
            {
                int result = sqlCommand.ExecuteNonQuery();
                return result < 0;
            }
            return false;
        }
        #endregion
    }
}
