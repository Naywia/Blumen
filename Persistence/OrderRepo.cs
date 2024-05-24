using Blumen.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Data;

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
                                         "output INSERTED.OrderID " +
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
                int id = (int)sqlCommand.ExecuteScalar();
                if (id > 0)
                {
                    SqlCommand? sqlCommand2 = new(null, sqlConnection);
                    string command = "INSERT INTO ORDER_PRODUCT(OrderID, ProductID) ";
                    for (int i = 0; i < item.Products.Count; i++)
                    {
                        if (command.Contains("VALUES "))
                        {
                            command += ", ";
                        }
                        command += " VALUES (@OrderID,@ProductID)";

                        sqlCommand2.Parameters.Add("@OrderID", SqlDbType.NVarChar).Value = id;
                        sqlCommand2.Parameters.Add("@ProductID", SqlDbType.NVarChar).Value = item.Products[i].ProductID;
                    }
                    sqlCommand2.CommandText = command;
                    if (sqlCommand2 != null)
                    {
                        int result = sqlCommand2.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            return false;
        }

        private static Order CreateOrder(SqlDataReader sqlDataReader)
        {
            ProductRepo productRepo = new();
            Order order = new()
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
            order.Products = productRepo.GetProductsInOrder(order);
            return order;
        }
        #endregion

        #region Read
        private readonly string selectStatement = "SELECT \"ORDER\".OrderID, Comment, \"ORDER\".Price, OrderDate, Delivery, PaymentStatus, Card, PaymentNote FROM \"ORDER\" ";
        public Order GetOrder(int orderNumber)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(DateTime orderDate)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Order> GetOrdersFromItem(Customer customer)
        {
            ObservableCollection<Order> items = [];
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            SqlDataReader sqlDataReader;
            sqlCommand = new(selectStatement + "WHERE CustomerID = @CustomerID", sqlConnection);
            sqlCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customer.CustomerID;
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Order temp = CreateOrder(sqlDataReader);
                items.Add(temp);
            }
            return items;
        }

        public ObservableCollection<Order> GetOrdersFromItem(Invoice invoice)
        {
            ObservableCollection<Order> items = [];
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            SqlDataReader sqlDataReader;
            sqlCommand = new(selectStatement + "WHERE InvoiceID = @InvoiceID", sqlConnection);
            sqlCommand.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = invoice.InvoiceID;
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Order temp = CreateOrder(sqlDataReader);
                items.Add(temp);
            }
            return items;
        }

        public override ObservableCollection<Order> GetItems()
        {
            ObservableCollection<Order> items = [];
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            SqlDataReader sqlDataReader;
            sqlCommand = new(selectStatement, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            CustomerRepo customerRepo = new();
            while (sqlDataReader.Read())
            {
                Order temp = CreateOrder(sqlDataReader);
                temp.Customer = customerRepo.GetCustomerFromItem(temp);
                items.Add(temp);
            }
            return items;
        }

        public ObservableCollection<Order> GetOrdersNotInInvoice(int customerID)
        {
            ObservableCollection<Order> items = [];
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            SqlDataReader sqlDataReader;
            sqlCommand = new(selectStatement + "WHERE CustomerID = @CustomerID AND InvoiceID = NULL", sqlConnection);
            sqlCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customerID;
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Order temp = CreateOrder(sqlDataReader);
                items.Add(temp);
            }
            return items;
        }
        #endregion

        #region Update
        public bool AddOrderToCustomer(Order order,int customerID)
        {
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new("UPDATE \"ORDER\" SET CustomerID = @CustomerID WHERE OrderID = @OrderID", sqlConnection);
            sqlCommand.Parameters.Add("@CustomerID", SqlDbType.NVarChar).Value = customerID;

            sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int).Value = order.OrderID;
            if (sqlCommand != null)
            {
                int result = sqlCommand.ExecuteNonQuery();
                return result > 0;
            }
            return false;
        }

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
