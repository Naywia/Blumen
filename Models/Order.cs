using System.Collections.ObjectModel;

namespace Blumen.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public ObservableCollection<Product> Products { get; set; } //Skal tilføjes til OrderView.xaml
        public string Comment { get; set; }
        public double Price { get; set; }
        public DateTime OrderDate { get; set; }
        public string Delivery {  get; set; }
        public Payment PaymentStatus { get; set; }
        public string Card {  get; set; }
        public string PaymentNote { get; set; }
        public bool IsComplete { get; set; }
        public int InvoiceID { get; set; }
        public Customer Customer { get; set; }



        public TimeOnly OrderDateTime
        {
            get => TimeOnly.Parse($"{OrderDate.Hour}:{OrderDate.Minute}");
        }
        public string OrderDateDay
        {
            get => OrderDate.DayOfWeek.ToString();
        }
        public DateOnly OrderDateDate
        {
            get => DateOnly.Parse($"{OrderDate.Day}-{OrderDate.Month}-{OrderDate.Year}");
        }

        public int GetTotal()
        {
            throw new NotImplementedException();
        }
    }
}
