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
            SqlCommand sqlCommand = new("INSERT INTO CUSTOMER(Name,Address,PhoneNumber,Email,PaymentNumber,PaymentNumberTypeID) " +
                                        "VALUES (@Name,@Address,@PhoneNumber,@Email,@PaymentNumber,@PaymentNumberTypeID)", sqlConnection);
            sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = item.Name;
            sqlCommand.Parameters.Add("@Address", SqlDbType.NVarChar).Value = item.Address;
            sqlCommand.Parameters.Add("@PhoneNumber", SqlDbType.BigInt).Value = item.PhoneNumber;
            sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = item.Email;
            sqlCommand.Parameters.Add("@PaymentNumber", SqlDbType.BigInt).Value = item.PaymentNumber;
            sqlCommand.Parameters.Add("@PaymentNumberTypeID", SqlDbType.Int).Value = (int)item.PaymentNumberType +1;
            if (sqlCommand != null)
            {
                int result = sqlCommand.ExecuteNonQuery();
                return result > 0;
            }
            return false;
        }
        #endregion

        #region Read
        public Customer? GetCustomer(string name)
        {
            OrderRepo orderRepo = new();
            Customer customer = null;
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new("SELECT CUSTOMER.CustomerID, Name, Address, PhoneNumber, Email, PaymentNumber, PaymentNumberTypeID " +
                             "FROM CUSTOMER " +
                             "Where Name='" + name + "'", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                customer = new()
                {
                    CustomerID = int.Parse(sqlDataReader["CustomerID"].ToString()),
                    Name = sqlDataReader["Name"].ToString(),
                    Address = sqlDataReader["Address"].ToString(),
                    Email = sqlDataReader["Email"].ToString(),
                    PhoneNumber = long.Parse(sqlDataReader["PhoneNumber"].ToString()),
                    PaymentNumber = long.Parse(sqlDataReader["PaymentNumber"].ToString()),
                    PaymentNumberType = int.Parse(sqlDataReader["PaymentNumberTypeID"].ToString())-1.ParseToPaymentNumber(),
                };
                customer.Orders = orderRepo.GetOrdersFromItem(customer);
            }
            return customer;
        }

        public Customer GetCustomer(int phoneNumber)
        {
            throw new NotImplementedException();
        }

        public override ObservableCollection<Customer> GetItems()
        {
            ObservableCollection<Customer> items = new() { };
            OrderRepo orderRepo = new();
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            SqlDataReader sqlDataReader;
            sqlCommand = new("SELECT CUSTOMER.CustomerID, Name, Address, PhoneNumber, Email, PaymentNumber, PaymentNumberTypeID " +
                             "FROM CUSTOMER", sqlConnection);
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
                    PaymentNumberType = int.Parse(sqlDataReader["PaymentNumberTypeID"].ToString())-1.ParseToPaymentNumber(),
                };
                temp.Orders = orderRepo.GetOrdersFromItem(temp);
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
            if (oldItem.PaymentNumberType != newItem.PaymentNumberType)
            {
                if (command.Contains('='))
                {
                    command += ", ";
                }
                command += "PaymentNumberTypeID = @PaymentNumberTypeID";
                sqlCommand.Parameters.Add("@PaymentNumberTypeID", SqlDbType.Int).Value = (int)newItem.PaymentNumberType +1;
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

