using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using TaskApi.Dtos.Employee_Dtos;
using System;

public class EmployeeApiClient
{
    private readonly HttpClient _http;

    public EmployeeApiClient(HttpClient http)
    {
        _http = http;
    }

    // GET: api/employees
    public async Task<IEnumerable<EmployeeDto>?> GetAllEmployeesAsync()
    {
        return await _http.GetFromJsonAsync<IEnumerable<EmployeeDto>>("");
    }

    // GET: api/employees/{id}
    public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<EmployeeDto>($"{id}");
    }

    // POST: api/employees
    public async Task<EmployeeDto?> CreateEmployeeAsync(EmployeeCreateDto dto)
    {
        var response = await _http.PostAsJsonAsync("", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<EmployeeDto>();
    }

    // PUT: api/employees/{id}
    public async Task UpdateEmployeeAsync(int id, EmployeeUpdateDto dto)
    {
        var response = await _http.PutAsJsonAsync($"{id}", dto);
        response.EnsureSuccessStatusCode();
    }

    // PATCH: api/employees/{id}
    public async Task PatchEmployeeAsync(int id, EmployeePatchDto dto)
    {
        var response = await _http.PatchAsJsonAsync($"{id}", dto);
        response.EnsureSuccessStatusCode();
    }

    // DELETE: api/employees/{id}
    public async Task DeleteEmployeeAsync(int id)
    {
        var response = await _http.DeleteAsync($"{id}");
        response.EnsureSuccessStatusCode();
    }

    // POST: api/employees/{employeeId}/tasks/{taskId}
    public async Task AssignTaskAsync(int employeeId, int taskId)
    {
        var response = await _http.PostAsync($"{employeeId}/tasks/{taskId}", null);
        response.EnsureSuccessStatusCode();
    }

    // DELETE: api/employees/{employeeId}/tasks/{taskId}
    public async Task RemoveTaskAsync(int employeeId, int taskId)
    {
        var response = await _http.DeleteAsync($"{employeeId}/tasks/{taskId}");
        response.EnsureSuccessStatusCode();
    }

    // GET: api/employees/employees-with-overdue-tasks
    public async Task<IEnumerable<EmployeeDto>?> GetEmployeesWithOverdueTasksAsync()
    {
        return await _http.GetFromJsonAsync<IEnumerable<EmployeeDto>>("employees-with-overdue-tasks");
    }
}
