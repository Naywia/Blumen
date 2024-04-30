using Blumen.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Blumen.Persistence
{
    public class ProductRepo : Repo<Product>
    {
        public Product GetProduct(string name)
        {
            return repo.Where(p => name == p.Name).FirstOrDefault()!;
        }

        public override ObservableCollection<Product> GetItems()
        {
            //return repo;
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
