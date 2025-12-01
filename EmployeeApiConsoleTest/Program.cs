using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TaskApi.Dtos.Employee_Dtos;

namespace EmployeeApiConsoleTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var http = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7136/api/employees/")
            };

            Console.WriteLine("=== Employee API Test ===");

            // 1. POST: Create an employee
            var newEmployee = new EmployeeCreateDto
            {
                Name = "Test Employee"
            };

            Console.WriteLine("Creating employee...");
            var createResponse = await http.PostAsJsonAsync("", newEmployee);
            createResponse.EnsureSuccessStatusCode();

            var createdEmployee = await createResponse.Content.ReadFromJsonAsync<EmployeeDto>();
            Console.WriteLine($"Employee created! ID = {createdEmployee.Id}, Name = {createdEmployee.Name}");

            // 2. GET: Retrieve employee by ID
            Console.WriteLine($"\nGetting employee by ID {createdEmployee.Id}...");

            var getEmployee =
                await http.GetFromJsonAsync<EmployeeDto>($"{createdEmployee.Id}");

            if (getEmployee == null)
            {
                Console.WriteLine("Employee not found!");
            }
            else
            {
                Console.WriteLine($"Employee Retrieved:");
                Console.WriteLine($"  Id: {getEmployee.Id}");
                Console.WriteLine($"  Name: {getEmployee.Name}");
            }

            Console.WriteLine("\nDone!");
        }
    }
}
