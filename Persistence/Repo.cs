using Blumen.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Collections.ObjectModel;
using System.Data;

namespace Blumen.Persistence
{
    public abstract class Repo<T>
    {
        protected ObservableCollection<T> repo = new ObservableCollection<T> { };
        protected string? connectionString;

        public Repo() 
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            connectionString = config.GetConnectionString("DBConnection");
        }

        public bool AddItem(T item)
        {
            repo.Add(item);
            //return repo.IndexOf(item) >= 0;
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            switch (item)
            {
                case Customer customer:
                    sqlCommand = new("INSERT INTO CUSTOMER(Name,Address,PhoneNumber,Email,PaymentNumber) " +
                        "              VALUES (@Name,@Address,@PhoneNumber,@Email,@PaymentNumber)",sqlConnection);
                    sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = customer.Name;
                    sqlCommand.Parameters.Add("@Address", SqlDbType.NVarChar).Value = customer.Address;
                    sqlCommand.Parameters.Add("@PhoneNumber", SqlDbType.BigInt).Value = customer.PhoneNumber;
                    sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = customer.Email;
                    sqlCommand.Parameters.Add("@PaymentNumber", SqlDbType.BigInt).Value = customer.PaymentNumber;
                    break;
                case Order order:
                    sqlCommand = new("INSERT INTO \"ORDER\"(Comment,Price,OrderDate,Delivery,PaymentStatus,Card,PaymentNote) " +
                        "               VALUES (@Comment,@Price,@OrderDate,@Delivery,@PaymentStatus,@Card,@PaymentNote)", sqlConnection);
                    sqlCommand.Parameters.Add("@Comment", SqlDbType.NVarChar).Value = order.Comment;
                    sqlCommand.Parameters.Add("@Price", SqlDbType.Float).Value = order.Price;
                    sqlCommand.Parameters.Add("@OrderDate", SqlDbType.DateTime2).Value = order.OrderDate;
                    sqlCommand.Parameters.Add("@Delivery", SqlDbType.NVarChar).Value = order.Delivery;
                    sqlCommand.Parameters.Add("@PaymentStatus", SqlDbType.Int).Value = order.PaymentStatus;
                    sqlCommand.Parameters.Add("@Card", SqlDbType.NVarChar).Value = order.Card;
                    sqlCommand.Parameters.Add("@PaymentNote", SqlDbType.NVarChar).Value = order.PaymentNote;
                    break;
                case Product product:
                    sqlCommand = new("INSERT INTO PRODUCT(Name,Price,Description,Quantity) " +
                        "               VALUES (@Name,@Price,@Description,@Quantity)",sqlConnection);
                    sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = product.Name;
                    sqlCommand.Parameters.Add("@Price", SqlDbType.Float).Value = product.Price;
                    sqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = product.Description;
                    sqlCommand.Parameters.Add("@Quantity", SqlDbType.Int).Value = product.Quantity;
                    break;
                case ProductType productType:
                    sqlCommand = new("INSERT INTO PRODUCT_TYPE(Name) " +
                        "               VALUES (@Name)", sqlConnection);
                    sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = productType.Name;
                    break;
            }
            if (sqlCommand != null)
            {
            var result = sqlCommand.ExecuteScalar();
                return result != null ? (int)result > 0 : false;
            }
            return false;
        }

        public bool UpdateItem(T oldItem, T newItem)
        {
            int index = repo.IndexOf(oldItem);
            if (index >= 0)
            {
                repo[index] = newItem;
                return true;
            }
            return false;
        }

        public ObservableCollection<T> GetItems()
        {
            return repo;
        }

        public bool RemoveItem(T item)
        {
            repo.Remove(item);
            return repo.IndexOf(item) < 0;
        }
    }
}
