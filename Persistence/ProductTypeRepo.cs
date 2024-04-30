using Blumen.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blumen.Persistence
{
    public class ProductTypeRepo : Repo<ProductType>
    {
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
            ObservableCollection<ProductType> items = new(){ };
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
    }
}
