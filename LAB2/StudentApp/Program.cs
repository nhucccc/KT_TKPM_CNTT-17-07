using System.Text;
using StudentApp;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding  = Encoding.UTF8;

        // Thay connection string nếu MongoDB chạy ở địa chỉ khác
        // Mặc định MongoDB chạy local: mongodb://localhost:27017
        var repository = new StudentRepository("mongodb://localhost:27017", "StudentDB");
        var service    = new StudentService(repository);
        var ui         = new StudentUI(service);

        await ui.Run();
    }
}
