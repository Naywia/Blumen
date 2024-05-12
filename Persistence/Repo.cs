using Blumen.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Controls;

namespace Blumen.Persistence
{
    public abstract class Repo<T>
    {
        protected string? connectionString;

        public Repo()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            connectionString = config.GetConnectionString("DBConnection");
        }

        public abstract bool AddItem(T item);

        public abstract bool UpdateItem(T oldItem, T newItem);

        public abstract ObservableCollection<T> GetItems();

        public abstract bool RemoveItem(T item);
    }
}
