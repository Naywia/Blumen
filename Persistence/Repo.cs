using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;

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
