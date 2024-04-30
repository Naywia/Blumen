using Blumen.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace Blumen.Persistence
{
    public class ProductRepo : Repo<Product>
    {
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

        public override ObservableCollection<Product> GetItems()
        {
            ObservableCollection<Product> items = new(){ };
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            SqlDataReader sqlDataReader;
            sqlCommand = new("SELECT ProductID, Name, Price, Description, Quantity FROM PRODUCT", sqlConnection);
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
    }
}
