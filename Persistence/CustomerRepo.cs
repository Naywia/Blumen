using Blumen.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;

namespace Blumen.Persistence
{
    public class CustomerRepo : Repo<Customer>
    {
        public Customer GetCustomer(string name)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(int phoneNumber)
        {
            throw new NotImplementedException();
        }

        public override ObservableCollection<Customer> GetItems()
        {
            ObservableCollection<Customer> items = new() { };
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            SqlDataReader sqlDataReader;
            sqlCommand = new("SELECT CustomerID, Name, Address, PhoneNumber, Email, PaymentNumber FROM CUSTOMER", sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Customer temp = new()
                {
                    CustomerID = int.Parse(sqlDataReader["CustomerID"].ToString()),
                    Name = sqlDataReader["Name"].ToString(),
                    Address = sqlDataReader["Address"].ToString(),
                    Email = sqlDataReader["Email"].ToString(),
                    PhoneNumber = long.Parse(sqlDataReader["PhoneNumber"].ToString()),
                    PaymentNumber = long.Parse(sqlDataReader["PaymentNumber"].ToString()),
                    //PaymentNumberType = Enum.Parse(PaymentNumberType, sqlDataReader["PaymentNumberType"].ToString())
                };
                items.Add(temp);
            }
            return items;
        }
    }
}

