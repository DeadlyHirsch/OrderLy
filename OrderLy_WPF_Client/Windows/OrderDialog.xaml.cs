using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static MongoDB.Driver.WriteConcern;

namespace OrderLy_WPF_Client
{
    /// <summary>
    /// Interaktionslogik für NewOrder.xaml
    /// </summary>
    public partial class OrderDialog : Window
    {
        public Order Order = new();
        public OrderDialog()
        {
            InitializeComponent();
            Order.Consumers = [];
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (Order is not null && Order.Consumers.Count > 0)
            {
                Order.Vendor = new Vendor() { Name = TBVendor.Text };
                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }

        }

        private void BTNConsumer_Click(object sender, RoutedEventArgs e)
        {
            ConsumerDialog dialog = new ConsumerDialog();
            if (dialog.ShowDialog() == true)
            {
                Order.Consumers.Add(dialog.Consumer);
            }
        }
    }
}
