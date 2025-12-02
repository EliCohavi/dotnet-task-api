using TaskApi.Dtos.Employee_Dtos;
using TaskApi.Dtos.Employee_Dtos;

Console.WriteLine("=== Task API Console Tester ===");
Console.WriteLine("1. Get Employee By Id");
Console.WriteLine("2. Create Employee");
Console.Write("Choose an option: ");

var choice = Console.ReadLine();

var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7036/")
};

var api = new EmployeeApiClient(httpClient);

switch (choice)
{
    case "1":
        await GetById(api);
        break;

    case "2":
        await CreateEmployee(api);
        break;

    default:
        Console.WriteLine("Invalid selection.");
        break;
}

Console.WriteLine("\nDone. Press any key to exit.");
Console.ReadKey();


// ========== METHODS ==========

static async Task GetById(EmployeeApiClient api)
{
    Console.Write("Enter employee ID to fetch: ");
    if (!int.TryParse(Console.ReadLine(), out var id))
    {
        Console.WriteLine("Invalid number.");
        return;
    }

    var employee = await api.GetEmployeeByIdAsync(id);

    if (employee == null)
    {
        Console.WriteLine("Employee not found.");
        return;
    }

    Console.WriteLine($"\n--- Employee {employee.Id} ---");
    Console.WriteLine($"Name: {employee.Name}");

    if (employee.Tasks == null || employee.Tasks.Count == 0)
    {
        Console.WriteLine("Tasks: None");
    }
    else
    {
        Console.WriteLine("Tasks:");
        foreach (var task in employee.Tasks)
        {
            Console.WriteLine($"  - ({task.Id}) {task.Title}");
        }
    }
}


static async Task CreateEmployee(EmployeeApiClient api)
{
    Console.Write("Enter new employee name: ");
    var name = Console.ReadLine();

    var dto = new EmployeeCreateDto
    {
        Name = name
    };

    var created = await api.CreateEmployeeAsync(dto);

    if (created == null)
    {
        Console.WriteLine("Failed to create employee.");
        return;
    }

    Console.WriteLine($"\nEmployee created!");
    Console.WriteLine($"Id: {created.Id}");
    Console.WriteLine($"Name: {created.Name}");
}
