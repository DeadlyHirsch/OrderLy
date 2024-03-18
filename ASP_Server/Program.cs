namespace ASP_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "Hallo Welt!");
            app.MapGet("/test", () => "Test Code!");

            app.Run();
        }
    }
}
