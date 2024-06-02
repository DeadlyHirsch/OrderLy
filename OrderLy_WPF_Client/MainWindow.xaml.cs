using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using MongoDB.Bson;
using MongoDB.Driver;
using RestSharp;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Documents;

namespace OrderLy_WPF_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public ObservableCollection<double> Weeks { get; set; } = new ObservableCollection<double>(new double[53]);
        public ObservableCollection<Order> Orders { get; set; } = [];
        public DispatcherTimer timer = new() { Interval = TimeSpan.FromSeconds(5) };
        public MainWindow()
        {
            InitializeComponent();
            InitializeDispatcherTimer();
            SetDefaultValues();
            DataContext = this;
        }

        private void InitializeDispatcherTimer()
        {
            timer.Tick += DispatcherTimer_Tick!;
            timer.Start();
        }
        private async void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (await IsApiReachableAsync("https://localhost:7180/api/Order"))
            {
                LBLAPIConnection.Foreground = new SolidColorBrush(Colors.LimeGreen);
                LBLAPIConnection.Content = "API connection Successfull";
                Orders.Clear();
                var orders = await LoadAllOrdersAsync();
                foreach (Order order in orders)
                {
                    Orders.Add(order);
                }
                var weekData = await LoadChartDataAsync();
                foreach (Week week in weekData)
                {
                    Weeks[Convert.ToInt32(week.Id)] = week.Count;
                }
            }
            else
            {
                LBLAPIConnection.Foreground = new SolidColorBrush(Colors.Red);
                LBLAPIConnection.Content = "Unable to reach API, trying again in 5 sec";
            }
        }

        public void SetDefaultValues()
        {
            string CurrentDate = DateTime.Now.ToString().Split(' ')[0];
            string WeekDay = DateTime.Now.DayOfWeek.ToString();
            string Year = DateTime.Now.Year.ToString();
            LBLDate.Content = $"{CurrentDate}, {WeekDay}";
            CHTOrders.Series =
            [
                new LineSeries<double>
                {
                    Values = Weeks,
                    Fill = new SolidColorPaint(new SKColor(13,115,119)) {StrokeThickness = 5},
                    Stroke = new SolidColorPaint(new SKColor(20,255,235)),
                    GeometryStroke = new SolidColorPaint(new SKColor(20,255,235)),
                    LineSmoothness = 0.1,
                    EasingFunction = EasingFunctions.CubicIn,
                    GeometrySize = 3
                }
            ];
            CHTOrders.XAxes =
            [
                new Axis
                {
                    Name = $"Calendar Week ({Year})",
                    NamePaint = new SolidColorPaint(SKColors.White),
                    NameTextSize = 15,
                    LabelsPaint = new SolidColorPaint(SKColors.White),
                    TextSize = 10,
                    SeparatorsPaint = new SolidColorPaint(SKColors.White)
                }

            ];
            CHTOrders.YAxes =
            [
                new Axis
                {
                    Name = "# of Orders",
                    NamePaint = new SolidColorPaint(SKColors.White),
                    NameTextSize = 15,
                    LabelsPaint = new SolidColorPaint(SKColors.White),
                    TextSize = 10,
                    Padding = new LiveChartsCore.Drawing.Padding(10,10,10,0),
                    SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray)
                    {
                        StrokeThickness = 2,
                        PathEffect = new DashEffect([3, 3])
                    }
                }
            ];
            CHTOrders.DrawMarginFrame = new DrawMarginFrame
            {
                Fill = new SolidColorPaint(new SKColor(50, 50, 50))
            };
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LBLDate.FontSize = LBLDate.ActualHeight * 0.7;
            LBLRecent.FontSize = LBLRecent.ActualHeight * 0.7;
            LBLOrderLy.FontSize = LBLOrderLy.ActualHeight * 0.7;
            LBLAPIConnection.FontSize = LBLAPIConnection.ActualHeight * 0.7;
            CHTOrders.XAxes.ElementAt(0).NameTextSize = CHTOrders.ActualHeight * 0.1;
            CHTOrders.YAxes.ElementAt(0).NameTextSize = CHTOrders.ActualHeight * 0.075;
            GVCDate.Width = LVOrders.ActualWidth * 0.25;
            GVCVendor.Width = LVOrders.ActualWidth * 0.3;
            GVCCost.Width = LVOrders.ActualWidth * 0.2;
            GVCConsumers.Width = LVOrders.ActualWidth * 0.25;
            BTNAddOrder.FontSize = BTNAddOrder.ActualHeight * 0.3;
        }

        private async Task<List<Order>> LoadAllOrdersAsync()
        {
            try
            {
                var client = new RestClient("https://localhost:7180/api");
                var request = new RestRequest("/Order", Method.Get);
                var response = await client.ExecuteAsync(request);

                return JsonSerializer.Deserialize<List<Order>>(response.Content!) ?? new List<Order>();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Request error: {ex.Message}");
                return new List<Order>();
            }
        }

        private async Task<List<Week>> LoadChartDataAsync()
        {
            try
            {
                var client = new RestClient("https://localhost:7180/api");
                var request = new RestRequest("/Order/Weekly", Method.Get);
                var response = await client.ExecuteAsync(request);

                return JsonSerializer.Deserialize<List<Week>>(response.Content!) ?? new List<Week>();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Request error: {ex.Message}");
                return new List<Week>();
            }
        }

        public static async Task<bool> IsApiReachableAsync(string url)
        {
            using HttpClient client = new();
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        private void BTNAddOrder_Click(object sender, RoutedEventArgs e)
        {
            NewOrder newOrderWindow = new NewOrder();
            newOrderWindow.ShowDialog();
        }
    }
}