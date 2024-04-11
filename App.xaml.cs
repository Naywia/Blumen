using Blumen.Persistence;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Blumen
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    
    public partial class App : Application
    {
        public static CustomerRepo CustomerRepo = new CustomerRepo();
        public static OrderRepo OrderRepo = new OrderRepo();
    }

}
