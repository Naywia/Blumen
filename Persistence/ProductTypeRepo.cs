using Blumen.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Data;

namespace Blumen.Persistence
{
    public class ProductTypeRepo : Repo<ProductType>
    {
        #region Create
        public override bool AddItem(ProductType item)
        {
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = new("INSERT INTO PRODUCT_TYPE(Name) " +
                                         "VALUES (@Name)", sqlConnection);
            sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = item.Name;
            if (sqlCommand != null)
            {
                int result = sqlCommand.ExecuteNonQuery();
                return result > 0;
            }
            return false;
        }
        #endregion

        #region Read
        public ProductType GetProductType(string name)
        {
            ProductType temp = new();
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            SqlDataReader sqlDataReader;
            sqlCommand = new("SELECT ProductTypeID, Name FROM PRODUCT_TYPE WHERE Name = @Name", sqlConnection);
            sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                temp = new()
                {
                    ProductTypeID = int.Parse(sqlDataReader["ProductTypeID"].ToString()),
                    Name = sqlDataReader["Name"].ToString()
                };
            }
            return temp;
        }

        public override ObservableCollection<ProductType> GetItems()
        {
            ObservableCollection<ProductType> items = new() { };
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            SqlDataReader sqlDataReader;
            sqlCommand = new("SELECT ProductTypeID, Name FROM PRODUCT_TYPE", sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                ProductType temp = new()
                {
                    ProductTypeID = int.Parse(sqlDataReader["ProductTypeID"].ToString()),
                    Name = sqlDataReader["Name"].ToString()
                };
                items.Add(temp);
            }
            return items;
        }
        #endregion

        #region Update
        public override bool UpdateItem(ProductType oldItem, ProductType newItem)
        {
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            if (JsonConvert.SerializeObject(oldItem) == JsonConvert.SerializeObject(newItem))
            {
                return false;
            }
            sqlCommand = new("UPDATE PRODUCT_TYPE SET Name = @Name WHERE ProductTypeID = @ProductTypeID", sqlConnection);
            sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = newItem.Name;
            sqlCommand.Parameters.Add("@ProductID", SqlDbType.Int).Value = newItem.ProductTypeID;
            if (sqlCommand != null)
            {
                int result = sqlCommand.ExecuteNonQuery();
                return result > 0;
            }
            return false;
        }
        #endregion

        #region Delete
        public override bool RemoveItem(ProductType item)
        {
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = new("DELETE PRODUCT_TYPE FROM PRODUCT_TYPE WHERE ProductTypeID = @ProductTypeID", sqlConnection);
            sqlCommand.Parameters.Add("@ProductTypeID", SqlDbType.Int).Value = item.ProductTypeID;

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
