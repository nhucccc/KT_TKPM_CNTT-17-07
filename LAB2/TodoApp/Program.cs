using System.Text;
using TodoAppV2;

public class Program
{
    private static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding  = Encoding.UTF8;

        var repository = new TodoRepository(
            "Server=LAPTOP-Q9IT8I08\\MSSQLSERVER01;Database=TodoDB;Integrated Security=true;TrustServerCertificate=true");

        var service = new TodoService(repository);
        var ui      = new TodoUI(service);

        await ui.Run();
    }
}
