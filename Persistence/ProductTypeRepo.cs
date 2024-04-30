using Blumen.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blumen.Persistence
{
    public class ProductTypeRepo : Repo<ProductType>
    {
        public ProductType GetProductType(string name)
        {
            return repo.Where(p => name == p.Name).FirstOrDefault()!;
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
