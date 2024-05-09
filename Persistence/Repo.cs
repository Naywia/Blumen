using Blumen.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Controls;

namespace Blumen.Persistence
{
    public abstract class Repo<T>
    {
        protected string? connectionString;

        public Repo()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            connectionString = config.GetConnectionString("DBConnection");
        }

        public bool AddItem(T item)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            switch (item)
            {
                case Customer customer:
                    sqlCommand = new("INSERT INTO CUSTOMER(Name,Address,PhoneNumber,Email,PaymentNumber) " +
                                     "VALUES (@Name,@Address,@PhoneNumber,@Email,@PaymentNumber)", sqlConnection);
                    sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = customer.Name;
                    sqlCommand.Parameters.Add("@Address", SqlDbType.NVarChar).Value = customer.Address;
                    sqlCommand.Parameters.Add("@PhoneNumber", SqlDbType.BigInt).Value = customer.PhoneNumber;
                    sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = customer.Email;
                    sqlCommand.Parameters.Add("@PaymentNumber", SqlDbType.BigInt).Value = customer.PaymentNumber;
                    break;
                case Order order:
                    sqlCommand = new("INSERT INTO \"ORDER\"(Comment,Price,OrderDate,Delivery,PaymentStatus,Card,PaymentNote) " +
                                     "VALUES (@Comment,@Price,@OrderDate,@Delivery,@PaymentStatus,@Card,@PaymentNote)", sqlConnection);
                    sqlCommand.Parameters.Add("@Comment", SqlDbType.NVarChar).Value = order.Comment;
                    sqlCommand.Parameters.Add("@Price", SqlDbType.Float).Value = order.Price;
                    sqlCommand.Parameters.Add("@OrderDate", SqlDbType.DateTime2).Value = order.OrderDate;
                    sqlCommand.Parameters.Add("@Delivery", SqlDbType.NVarChar).Value = order.Delivery;
                    sqlCommand.Parameters.Add("@PaymentStatus", SqlDbType.Int).Value = (int)order.PaymentStatus;
                    sqlCommand.Parameters.Add("@Card", SqlDbType.NVarChar).Value = order.Card;
                    sqlCommand.Parameters.Add("@PaymentNote", SqlDbType.NVarChar).Value = order.PaymentNote;
                    break;
                case Product product:
                    sqlCommand = new("INSERT INTO PRODUCT(Name,Price,Description,Quantity) " +
                                     "VALUES (@Name,@Price,@Description,@Quantity)", sqlConnection);
                    sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = product.Name;
                    sqlCommand.Parameters.Add("@Price", SqlDbType.Float).Value = product.Price;
                    sqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = product.Description;
                    sqlCommand.Parameters.Add("@Quantity", SqlDbType.Int).Value = product.Quantity;
                    break;
                case ProductType productType:
                    sqlCommand = new("INSERT INTO PRODUCT_TYPE(Name) " +
                                     "VALUES (@Name)", sqlConnection);
                    sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = productType.Name;
                    break;
                case Service service:
                    sqlCommand = new("INSERT INTO SERVICE(Name,Price) " +
                                     "VALUES (@Name,@Price)", sqlConnection);
                    sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = service.Name;
                    sqlCommand.Parameters.Add("@Price", SqlDbType.Float).Value = service.Price;
                    break;
            }
            if (sqlCommand != null)
            {
                int result = sqlCommand.ExecuteNonQuery();
                return result > 0;
            }
            return false;
        }

        public bool UpdateItem(T oldItem, T newItem)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            switch (oldItem, newItem)
            {
                case (Customer oldCustomer, Customer newCustomer):
                    if (JsonConvert.SerializeObject(oldCustomer) == JsonConvert.SerializeObject(newCustomer))
                    {
                        break;
                    }
                    string cCommand = "UPDATE CUSTOMER SET ";
                    sqlCommand = new("", sqlConnection);
                    if (oldCustomer.Name != newCustomer.Name)
                    {
                        cCommand += "Name = @Name";
                        sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = newCustomer.Name;
                    }
                    if (oldCustomer.Address != newCustomer.Address)
                    {
                        if (cCommand.Contains('='))
                        {
                            cCommand += ", ";
                        }
                        cCommand += "Address = @Address";
                        sqlCommand.Parameters.Add("@Address", SqlDbType.NVarChar).Value = newCustomer.Address;
                    }
                    if (oldCustomer.PhoneNumber != newCustomer.PhoneNumber)
                    {
                        if (cCommand.Contains('='))
                        {
                            cCommand += ", ";
                        }
                        cCommand += "PhoneNumber = @PhoneNumber";
                        sqlCommand.Parameters.Add("@PhoneNumber", SqlDbType.BigInt).Value = newCustomer.PhoneNumber;
                    }
                    if (oldCustomer.Email != newCustomer.Email)
                    {
                        if (cCommand.Contains('='))
                        {
                            cCommand += ", ";
                        }
                        cCommand += "Email = @Email";
                        sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = newCustomer.Email;
                    }
                    if (oldCustomer.PaymentNumber != newCustomer.PaymentNumber)
                    {
                        if (cCommand.Contains('='))
                        {
                            cCommand += ", ";
                        }
                        cCommand += "PaymentNumber = @PaymentNumber";
                        sqlCommand.Parameters.Add("@PaymentNumber", SqlDbType.BigInt).Value = newCustomer.PaymentNumber;
                    }
                    cCommand += " WHERE CustomerID = @CustomerID";
                    sqlCommand.CommandText = cCommand;
                    sqlCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = newCustomer.CustomerID;
                    break;
                case (Order oldOrder, Order newOrder):
                    if (JsonConvert.SerializeObject(oldOrder) == JsonConvert.SerializeObject(newOrder))
                    {
                        break;
                    }
                    string oCommand = "UPDATE \"ORDER\" SET ";
                    sqlCommand = new("", sqlConnection);

                    if (oldOrder.Comment != newOrder.Comment)
                    {
                        oCommand += "Comment = @Comment";
                        sqlCommand.Parameters.Add("@Comment", SqlDbType.NVarChar).Value = newOrder.Comment;
                    }
                    if (oldOrder.Price != newOrder.Price)
                    {
                        if (oCommand.Contains('='))
                        {
                            oCommand += ", ";
                        }
                        oCommand += "Price = @Price";
                        sqlCommand.Parameters.Add("@Price", SqlDbType.Float).Value = newOrder.Price;
                    }
                    if (oldOrder.OrderDate != newOrder.OrderDate)
                    {
                        if (oCommand.Contains('='))
                        {
                            oCommand += ", ";
                        }
                        oCommand += "OrderDate = @OrderDate";
                        sqlCommand.Parameters.Add("@OrderDate", SqlDbType.DateTime2).Value = newOrder.OrderDate;
                    }
                    if (oldOrder.Delivery != newOrder.Delivery)
                    {
                        if (oCommand.Contains('='))
                        {
                            oCommand += ", ";
                        }
                        oCommand += "Delivery = @Delivery";
                        sqlCommand.Parameters.Add("@Delivery", SqlDbType.NVarChar).Value = newOrder.Delivery;
                    }
                    if (oldOrder.PaymentStatus != newOrder.PaymentStatus)
                    {
                        if (oCommand.Contains('='))
                        {
                            oCommand += ", ";
                        }
                        oCommand += "PaymentStatus = @PaymentStatus";
                        sqlCommand.Parameters.Add("@PaymentStatus", SqlDbType.Int).Value = newOrder.PaymentStatus;
                    }
                    if (oldOrder.Card != newOrder.Card)
                    {
                        if (oCommand.Contains('='))
                        {
                            oCommand += ", ";
                        }
                        oCommand += "Card = @Card";
                        sqlCommand.Parameters.Add("@Card", SqlDbType.NVarChar).Value = newOrder.Card;
                    }
                    if (oldOrder.PaymentNote != newOrder.PaymentNote)
                    {
                        if (oCommand.Contains('='))
                        {
                            oCommand += ", ";
                        }
                        oCommand += "PaymentNote = @PaymentNote";
                        sqlCommand.Parameters.Add("@PaymentNote", SqlDbType.NVarChar).Value = newOrder.PaymentNote;
                    }
                    if (oldOrder.IsComplete != newOrder.IsComplete)
                    {
                        if (oCommand.Contains('='))
                        {
                            oCommand += ", ";
                        }
                        oCommand += "IsComplete = @IsComplete";
                        sqlCommand.Parameters.Add("@IsComplete", SqlDbType.Bit).Value = newOrder.IsComplete;
                    }
                    oCommand += " WHERE OrderID = @OrderID";
                    sqlCommand.CommandText = oCommand;

                    sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int).Value = newOrder.OrderID;
                    break;
                case (Product oldProduct, Product newProduct):
                    if (JsonConvert.SerializeObject(oldProduct) == JsonConvert.SerializeObject(newProduct))
                    {
                        break;
                    }
                    string pCommand = "UPDATE PRODUCT SET ";
                    sqlCommand = new("", sqlConnection);

                    if (oldProduct.Name != newProduct.Name)
                    {
                        pCommand += "Name = @Name";
                        sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = newProduct.Name;
                    }
                    if (oldProduct.Price != newProduct.Price)
                    {
                        if (pCommand.Contains('='))
                        {
                            pCommand += ", ";
                        }
                        pCommand += "Price = @Price";
                        sqlCommand.Parameters.Add("@Price", SqlDbType.Float).Value = newProduct.Price;
                    }
                    if (oldProduct.Description != newProduct.Description)
                    {
                        if (pCommand.Contains('='))
                        {
                            pCommand += ", ";
                        }
                        pCommand += "Description = @Description";
                        sqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = newProduct.Description;
                    }
                    if (oldProduct.Quantity != newProduct.Quantity)
                    {
                        if (pCommand.Contains('='))
                        {
                            pCommand += ", ";
                        }
                        pCommand += "Quantity = @Quantity";
                        sqlCommand.Parameters.Add("@Quantity", SqlDbType.Int).Value = newProduct.Quantity;
                    }

                    pCommand += " WHERE ProductID = @ProductID";
                    sqlCommand.CommandText = pCommand;

                    sqlCommand.Parameters.Add("@ProductID", SqlDbType.Int).Value = newProduct.ProductID;
                    break;
                case (ProductType oldProductType, ProductType newProductType):
                    if (JsonConvert.SerializeObject(oldProductType) == JsonConvert.SerializeObject(newProductType))
                    {
                        break;
                    }
                    sqlCommand = new("UPDATE PRODUCT_TYPE SET Name = @Name WHERE ProductTypeID = @ProductTypeID", sqlConnection);
                    sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = newProductType.Name;
                    sqlCommand.Parameters.Add("@ProductID", SqlDbType.Int).Value = newProductType.ProductTypeID;
                    break;
            }
            if (sqlCommand != null)
            {
                int result = sqlCommand.ExecuteNonQuery();
                return result > 0;
            }
            return false;
        }

        public abstract ObservableCollection<T> GetItems();

        public bool RemoveItem(T item)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;

            switch (item)
            {
                case Customer customer:
                    sqlCommand = new("DELETE CUSTOMER FROM CUSTOMER WHERE CustomerID = @CustomerID", sqlConnection);
                    sqlCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customer.CustomerID;
                    break;
                case Order order:
                    sqlCommand = new("DELETE \"ORDER\" FROM \"ORDER\" WHERE OrderID = @OrderID", sqlConnection);
                    sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int).Value = order.OrderID;
                    break;
                case Product product:
                    sqlCommand = new("DELETE PRODUCT FROM PRODUCT WHERE ProductID = @ProductID", sqlConnection);
                    sqlCommand.Parameters.Add("@ProductID", SqlDbType.Int).Value = product.ProductID;
                    break;
                case ProductType productType:
                    sqlCommand = new("DELETE PRODUCT_TYPE FROM PRODUCT_TYPE WHERE ProductTypeID = @ProductTypeID", sqlConnection);
                    sqlCommand.Parameters.Add("@ProductTypeID", SqlDbType.Int).Value = productType.ProductTypeID;
                    break;
            }
            if (sqlCommand != null)
            {
                int result = sqlCommand.ExecuteNonQuery();
                return result < 0;
            }
            return false;
        }
    }
}
