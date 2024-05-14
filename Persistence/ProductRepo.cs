using Blumen.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Data;

namespace Blumen.Persistence
{
    public class ProductRepo : Repo<Product>
    {
        #region Create
        public override bool AddItem(Product item)
        {
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = new("INSERT INTO PRODUCT(Name,Price,Description,Quantity,ProductTypeID) " +
                                     "VALUES (@Name,@Price,@Description,@Quantity,@ProductTypeID)", sqlConnection);
            sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = item.Name;
            sqlCommand.Parameters.Add("@Price", SqlDbType.Float).Value = item.Price;
            sqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = item.Description;
            sqlCommand.Parameters.Add("@Quantity", SqlDbType.Int).Value = item.Quantity;
            sqlCommand.Parameters.Add("@ProductTypeID", SqlDbType.Int).Value = item.Type.ProductTypeID;

            if (sqlCommand != null)
            {
                int result = sqlCommand.ExecuteNonQuery();
                return result > 0;
            }
            return false;
        }
        #endregion

        #region Read
        public Product GetProduct(string name)
        {
            Product temp = new();
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            SqlDataReader sqlDataReader;
            sqlCommand = new("SELECT ProductID, Name, Price, Description, Quantity FROM PRODUCT WHERE Name = @Name", sqlConnection);
            sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                temp = new()
                {
                    ProductID = int.Parse(sqlDataReader["ProductID"].ToString()),
                    Name = sqlDataReader["Name"].ToString(),
                    Price = double.Parse(sqlDataReader["Price"].ToString()),
                    Description = sqlDataReader["Description"].ToString(),
                    Quantity = int.Parse(sqlDataReader["Quantity"].ToString())
                };
            }
            return temp;
        }

        public ObservableCollection<Product> GetProductsInOrder(Order order)
        {
            ObservableCollection<Product> items = [];
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            SqlDataReader sqlDataReader;
            sqlCommand = new("SELECT PRODUCT.ProductID, Name, Price, Description, Quantity FROM ORDER_PRODUCT " +
                "INNER JOIN PRODUCT ON ORDER_PRODUCT.ProductID = PRODUCT.ProductID " +
                "WHERE OrderID = @OrderID", sqlConnection);
            sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int).Value = order.OrderID;
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Product temp = new()
                {
                    ProductID = int.Parse(sqlDataReader["ProductID"].ToString()),
                    Name = sqlDataReader["Name"].ToString(),
                    Price = double.Parse(sqlDataReader["Price"].ToString()),
                    Description = sqlDataReader["Description"].ToString(),
                    Quantity = int.Parse(sqlDataReader["Quantity"].ToString())
                };
                items.Add(temp);
            }
            return items;
        }

        public override ObservableCollection<Product> GetItems()
        {
            ObservableCollection<Product> items = new() { };
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            SqlDataReader sqlDataReader;
            sqlCommand = new("SELECT ProductID, PRODUCT.Name, Price, Description, Quantity, PRODUCT.ProductTypeID, PRODUCT_TYPE.Name " +
                             "FROM PRODUCT " +
                             "INNER JOIN PRODUCT_TYPE ON PRODUCT.ProductTypeID = PRODUCT_TYPE.ProductTypeID", sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Product temp = new()
                {
                    ProductID = int.Parse(sqlDataReader["ProductID"].ToString()),
                    Name = sqlDataReader[1].ToString(),
                    Price = double.Parse(sqlDataReader["Price"].ToString()),
                    Description = sqlDataReader["Description"].ToString(),
                    Quantity = int.Parse(sqlDataReader["Quantity"].ToString()),
                    Type = new ProductType() { ProductTypeID = int.Parse(sqlDataReader[5].ToString()), Name = sqlDataReader[6].ToString() }
                };
                items.Add(temp);
            }
            return items;
        }
        #endregion

        #region Update
        public override bool UpdateItem(Product oldItem, Product newItem)
        {
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            if (JsonConvert.SerializeObject(oldItem) == JsonConvert.SerializeObject(newItem))
            {
                return false;
            }
            string command = "UPDATE PRODUCT SET ";
            sqlCommand = new("", sqlConnection);

            if (oldItem.Name != newItem.Name)
            {
                command += "Name = @Name";
                sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = newItem.Name;
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
            if (oldItem.Description != newItem.Description)
            {
                if (command.Contains('='))
                {
                    command += ", ";
                }
                command += "Description = @Description";
                sqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = newItem.Description;
            }
            if (oldItem.Quantity != newItem.Quantity)
            {
                if (command.Contains('='))
                {
                    command += ", ";
                }
                command += "Quantity = @Quantity";
                sqlCommand.Parameters.Add("@Quantity", SqlDbType.Int).Value = newItem.Quantity;
            }
            if(oldItem.Type != newItem.Type)
            {
                if (command.Contains('='))
                {
                    command += ", ";
                }
                command += "ProductTypeID = @Type";
                sqlCommand.Parameters.Add("@Type", SqlDbType.Int).Value = newItem.Type.ProductTypeID;
            }

            command += " WHERE ProductID = @ProductID";
            sqlCommand.CommandText = command;

            sqlCommand.Parameters.Add("@ProductID", SqlDbType.Int).Value = newItem.ProductID;
            if (sqlCommand != null)
            {
                int result = sqlCommand.ExecuteNonQuery();
                return result > 0;
            }
            return false;
        }
        #endregion

        #region Delete
        public override bool RemoveItem(Product item)
        {
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = new("DELETE PRODUCT FROM PRODUCT WHERE ProductID = @ProductID", sqlConnection);
            sqlCommand.Parameters.Add("@ProductID", SqlDbType.Int).Value = item.ProductID;

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
