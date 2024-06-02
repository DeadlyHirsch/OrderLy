using System;
using System.Collections.Generic;
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

namespace OrderLy_WPF_Client
{
    /// <summary>
    /// Interaktionslogik für ConsumerDialog.xaml
    /// </summary>
    public partial class ConsumerDialog : Window
    {
        public Consumer Consumer { get; set; } = new();
        public ConsumerDialog()
        {
            InitializeComponent();
            Consumer.FoodItems = [];
        }
        
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Consumer.Name = TBName.Text;
            DialogResult = true;
        }

        private void BTNProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductDialog dialog = new();
            if (dialog.ShowDialog() == true)
            {
                Consumer.FoodItems.Add(dialog.Item);
            }
        }
    }
}
