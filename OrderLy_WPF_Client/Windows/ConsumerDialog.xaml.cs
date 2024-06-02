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
        public string Name { get; private set; }
        public List<FoodItem> FoodItems { get; private set; } = new();
        public ConsumerDialog()
        {
            InitializeComponent();
        }
        
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Name = TBName.Text;
            DialogResult = true;
        }

        private void TBProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductDialog productDialog = new();
            if (productDialog.ShowDialog() == true)
            {
                FoodItems.Add(productDialog.Item);
            }
        }
    }
}
