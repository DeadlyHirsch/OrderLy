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
using static MongoDB.Driver.WriteConcern;

namespace OrderLy_WPF_Client
{
    /// <summary>
    /// Interaktionslogik für ProductDialog.xaml
    /// </summary>
    public partial class ProductDialog : Window
    {
        public FoodItem Item { get; set; } = new();
        public ProductDialog()
        {
            InitializeComponent();
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Item.Name = TBName.Text;
            if (double.TryParse(TBCost.Text, out double d) && !Double.IsNaN(d) && !Double.IsInfinity(d))
            {
                Item.Price = d;
                DialogResult = true;
            }
            else
            {
                Item = null!;
                DialogResult = false;
            }
        }
    }
}
