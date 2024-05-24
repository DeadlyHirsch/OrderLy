using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

namespace Notable_WPF_Client
{
    /// <summary>
    /// Interaktionslogik für LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        static HttpClient client = new HttpClient();
        public LoginWindow()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("http://127.0.0.1:7180");
        }

        private void BTNRegister_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void BTNLogin_Click(object sender, RoutedEventArgs e)
        {
            User user = new(TBUsername.Text, TBPassword.Text);

            var url = await CreateUserAsync(user);
        }

        static async Task<Uri> CreateUserAsync(User user)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/User", user);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        public class User
        {
            public string? Id { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }

            public User(string name, string pw)
            {
                UserName = name;
                Password = pw;
            }
        }
    }
}
