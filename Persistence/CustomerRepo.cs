using Blumen.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Data;

namespace Blumen.Persistence
{
    public class CustomerRepo : Repo<Customer>
    {
        #region Create
        public override bool AddItem(Customer item)
        {
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new("INSERT INTO CUSTOMER(Name,Address,PhoneNumber,Email,PaymentNumber) " +
                                        "VALUES (@Name,@Address,@PhoneNumber,@Email,@PaymentNumber)", sqlConnection);
            sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = item.Name;
            sqlCommand.Parameters.Add("@Address", SqlDbType.NVarChar).Value = item.Address;
            sqlCommand.Parameters.Add("@PhoneNumber", SqlDbType.BigInt).Value = item.PhoneNumber;
            sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = item.Email;
            sqlCommand.Parameters.Add("@PaymentNumber", SqlDbType.BigInt).Value = item.PaymentNumber;
            if (sqlCommand != null)
            {
                int result = sqlCommand.ExecuteNonQuery();
                return result > 0;
            }
            return false;
        }
        #endregion

        #region Read
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
        #endregion

        #region Update
        public override bool UpdateItem(Customer oldItem, Customer newItem)
        {
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            if (JsonConvert.SerializeObject(oldItem) == JsonConvert.SerializeObject(newItem))
            {
                return false;
            }
            string command = "UPDATE CUSTOMER SET ";
            sqlCommand = new("", sqlConnection);
            if (oldItem.Name != newItem.Name)
            {
                command += "Name = @Name";
                sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = newItem.Name;
            }
            if (oldItem.Address != newItem.Address)
            {
                if (command.Contains('='))
                {
                    command += ", ";
                }
                command += "Address = @Address";
                sqlCommand.Parameters.Add("@Address", SqlDbType.NVarChar).Value = newItem.Address;
            }
            if (oldItem.PhoneNumber != newItem.PhoneNumber)
            {
                if (command.Contains('='))
                {
                    command += ", ";
                }
                command += "PhoneNumber = @PhoneNumber";
                sqlCommand.Parameters.Add("@PhoneNumber", SqlDbType.BigInt).Value = newItem.PhoneNumber;
            }
            if (oldItem.Email != newItem.Email)
            {
                if (command.Contains('='))
                {
                    command += ", ";
                }
                command += "Email = @Email";
                sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = newItem.Email;
            }
            if (oldItem.PaymentNumber != newItem.PaymentNumber)
            {
                if (command.Contains('='))
                {
                    command += ", ";
                }
                command += "PaymentNumber = @PaymentNumber";
                sqlCommand.Parameters.Add("@PaymentNumber", SqlDbType.BigInt).Value = newItem.PaymentNumber;
            }
            command += " WHERE CustomerID = @CustomerID";
            sqlCommand.CommandText = command;
            sqlCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = newItem.CustomerID;

            if (sqlCommand != null)
            {
                int result = sqlCommand.ExecuteNonQuery();
                return result > 0;
            }
            return false;
        }
        #endregion

        #region Delete
        public override bool RemoveItem(Customer item)
        {
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = new("DELETE CUSTOMER FROM CUSTOMER WHERE CustomerID = @CustomerID", sqlConnection);
            sqlCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = item.CustomerID;

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

