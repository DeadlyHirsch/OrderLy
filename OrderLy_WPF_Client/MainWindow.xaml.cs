using MongoDB.Bson;
using MongoDB.Driver;
using System.Windows;

namespace OrderLy_WPF_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string CurrentDate = DateTime.Now.ToString().Split(' ')[0];
            string WeekDay = DateTime.Now.DayOfWeek.ToString();
            LBLDate.Content = $"{CurrentDate}, {WeekDay}";
            LoadChartData();
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LBLDate.FontSize = LBLDate.ActualHeight * 0.7;
            LBLRecent.FontSize = LBLRecent.ActualHeight * 0.7;
            LBLOrderLy.FontSize = LBLOrderLy.ActualHeight * 0.7;
            CHTOrders.XAxes.ElementAt(0).NameTextSize = CHTOrders.Height * 1;
            CHTOrders.Series.ElementAt(0);
        }

        private string LoadChartData()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("OrderLy");
            var collection = db.GetCollection<BsonDocument>("Orders");

            var Result = collection.Aggregate()
                .Match(new BsonDocument { { "createdAt",
                new BsonDocument("$gte", DateTime.Now.AddDays(-365)) } })
                .Group(new BsonDocument{
                    { "_id",
                        new BsonDocument("$dateToString",
                        new BsonDocument
                        {
                            { "format", "%V" },
                            { "date", "$createdAt" }
                        })
                    },
                { "count",
                    new BsonDocument("$sum", 1) }
                })
                .ToList()
                .ToJson();

            return Result;
        }
    }
}