﻿using Blumen.Models;
using Blumen.Persistence;
using System.Windows;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    public class OrderViewModel : ObservableObject
    {
        #region Fields
        private ICommand updateOrderCommand;
        private OrderRepo orderRepo = App.OrderRepo;
        private Order order;
        private Window currentWindow;

        private int orderNumber;
        private DateTime orderDate;
        private string deliveryAddress;
        private Payment paymentStatus;
        private List<string> paymentOptions = new List<string>();
        private string comment;
        #endregion

        #region Constructors
        public OrderViewModel(Window window, int orderIndex)
        {
            currentWindow = window;
            order = orderRepo.GetItems()[orderIndex];

            OrderNumber = order.OrderNumber;
            OrderDate = order.OrderDate;
            DeliveryAddress = order.DeliveryAddress;
            PaymentStatus = order.PaymentStatus;
            Comment = order.Comment;

            foreach (string name in Enum.GetNames(typeof(Payment)))
            {
                PaymentOptions.Add(name);
            }
        }
        #endregion

        #region Properties
        public ICommand UpdateOrderCommand
        {
            get
            {
                if (updateOrderCommand == null)
                    updateOrderCommand = new RelayCommand(MethodToRun => UpdateOrder());
                return updateOrderCommand;
            }
        }

        public int OrderNumber
        {
            get => orderNumber;
            set
            {
                orderNumber = value;
                NotifyPropertyChanged();
            }
        }
        public DateTime OrderDate
        {
            get => orderDate;
            set
            {
                orderDate = value;
                NotifyPropertyChanged();
            }
        }
        public TimeOnly OrderDateTime
        {
            get => TimeOnly.Parse($"{orderDate.Hour}:{orderDate.Minute}");
        }
        public string OrderDateDay
        {
            get => orderDate.DayOfWeek.ToString();
        }
        public DateOnly OrderDateDate
        {
            get => DateOnly.Parse($"{orderDate.Day}-{orderDate.Month}-{orderDate.Year}");
        }


        public string DeliveryAddress
        {
            get => deliveryAddress;
            set
            {
                deliveryAddress = value;
                NotifyPropertyChanged();
            }
        }
        public Payment PaymentStatus
        {
            get => paymentStatus;
            set
            {
                paymentStatus = value;
                NotifyPropertyChanged();
            }
        }
        public List<string> PaymentOptions
        {
            get => paymentOptions;
            private set
            {
                paymentOptions = value;
                NotifyPropertyChanged();
            }
        }
        public string Comment
        {
            get => comment;
            set
            {
                comment = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Methods
        public void UpdateOrder()
        {
            orderRepo.UpdateItem(order, new Order()
            {
                OrderNumber = OrderNumber,
                OrderDate = OrderDate,
                DeliveryAddress = DeliveryAddress,
                PaymentStatus = PaymentStatus,
                Comment = Comment
            });
            currentWindow.Close();
        }
        #endregion
    }
}
