using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blumen.Persistence
{
    public abstract class Repo<T>
    {
        protected ObservableCollection<T> repo = new ObservableCollection<T> { };

        public bool AddItem(T item)
        {
            repo.Add(item);
            return repo.IndexOf(item) >= 0;
        }

        public bool UpdateItem(T oldItem, T newItem)
        {
            int index = repo.IndexOf(oldItem);
            if (index >= 0)
            {
                repo[index] = newItem;
                return true;
            }
            return false;
        }

        public ObservableCollection<T> GetItems()
        {
            return repo;
        }

        public bool RemoveItem(T item)
        {
            repo.Remove(item);
            return repo.IndexOf(item) < 0;
        }
    }
}
