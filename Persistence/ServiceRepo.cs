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
    class ServiceRepo : Repo<Service>
    {
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

    }
}
