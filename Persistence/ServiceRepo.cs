using Blumen.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blumen.Persistence
{
    class ServiceRepo : Repo<Service>
    {
        #region Create
        public override bool AddItem(Service item)
        {
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = new("INSERT INTO SERVICE(Name,Price) " +
                                         "VALUES (@Name,@Price)", sqlConnection);
            sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = item.Name;
            sqlCommand.Parameters.Add("@Price", SqlDbType.Float).Value = item.Price;
            if (sqlCommand != null)
            {
                int result = sqlCommand.ExecuteNonQuery();
                return result > 0;
            }
            return false;
        }
        #endregion

        #region Read
        public override ObservableCollection<Service> GetItems()
        {
            ObservableCollection<Service> services = new() { };
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            SqlDataReader sqlDataReader;
            sqlCommand = new("SELECT ServiceID, Name, Price FROM SERVICE", sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Service temp = new()
                {
                    ServiceID = int.Parse(sqlDataReader["ServiceID"].ToString()),
                    Name = sqlDataReader["Name"].ToString(),
                    Price = double.Parse(sqlDataReader["Price"].ToString())
                };
                services.Add(temp);
            }
            return services;
        }

        public Service GetService(string name)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Update
        public override bool UpdateItem(Service oldItem, Service newItem)
        {
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = null;
            if (JsonConvert.SerializeObject(oldItem) == JsonConvert.SerializeObject(newItem))
            {
                return false;
            }
            sqlCommand = new("", sqlConnection);
            string command = "UPDATE SERVICE SET ";

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
            command += " WHERE ServiceID = @ServiceID";
            sqlCommand.CommandText = command;

            sqlCommand.Parameters.Add("@ServiceID", SqlDbType.Int).Value = newItem.ServiceID;
            if (sqlCommand != null)
            {
                int result = sqlCommand.ExecuteNonQuery();
                return result > 0;
            }
            return false;
        }
        #endregion

        #region Delete
        public override bool RemoveItem(Service item)
        {
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            SqlCommand? sqlCommand = new("DELETE SERVICE FROM SERVICE WHERE ServiceID = @ServiceID", sqlConnection);
            sqlCommand.Parameters.Add("@ServiceID", SqlDbType.Int).Value = item.ServiceID;

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
