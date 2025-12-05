using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using TaskApi.Dtos;
using System;

public class TaskApiClient
{
    private readonly HttpClient _http;

    public TaskApiClient(HttpClient http)
    {
        _http = http;
    }

    // GET: api/tasks
    public async Task<IEnumerable<TaskItemDto>?> GetAllTasksAsync()
    {
        return await _http.GetFromJsonAsync<IEnumerable<TaskItemDto>>("");
    }

    // GET: api/tasks/{id}
    public async Task<TaskItemDto?> GetTaskByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<TaskItemDto>($"{id}");
    }

    // GET: api/tasks/upcoming
    public async Task<IEnumerable<TaskItemDto>?> GetUpcomingTasksAsync()
    {
        return await _http.GetFromJsonAsync<IEnumerable<TaskItemDto>>("upcoming");
    }

    // POST: api/tasks
    public async Task<TaskItemDto?> CreateTaskAsync(CreateTaskDto dto)
    {
        var response = await _http.PostAsJsonAsync("", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TaskItemDto>();
    }

    // PUT: api/tasks/{id}
    public async Task UpdateTaskAsync(int id, UpdateTaskDto dto)
    {
        var response = await _http.PutAsJsonAsync($"{id}", dto);
        response.EnsureSuccessStatusCode();
    }

    // PATCH: api/tasks/{id}
    public async Task PatchTaskAsync(int id, PatchTaskDto dto)
    {
        var response = await _http.PatchAsJsonAsync($"{id}", dto);
        response.EnsureSuccessStatusCode();
    }

    // POST api/tasks/{taskId}/employees/{employeeId}
    public async Task AssignTaskToEmployeeAsync(int taskId, int employeeId)
    {
        var response = await _http.PostAsync($"{taskId}/employees/{employeeId}", null);
        response.EnsureSuccessStatusCode();
    }

    // DELETE: api/tasks/{taskId}/employees/{employeeId}
    public async Task RemoveTaskFromEmployeeAsync(int taskId, int employeeId)
    {
        var response = await _http.DeleteAsync($"{taskId}/employees/{employeeId}");
        response.EnsureSuccessStatusCode();
    }

    // DELETE: api/tasks/{id}
    public async Task DeleteTaskAsync(int id)
    {
        var response = await _http.DeleteAsync($"{id}");
        response.EnsureSuccessStatusCode();
    }

    //GET: api/tasks/overdue-tasks
    public async Task<IEnumerable<TaskItemDto>?> GetOverdueTasksAsync()
    {
        return await _http.GetFromJsonAsync<IEnumerable<TaskItemDto>>("overdue-tasks");
    }

}
