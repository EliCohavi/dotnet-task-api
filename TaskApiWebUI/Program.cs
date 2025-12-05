using TaskApiWebUI.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Http Clients
builder.Services.AddHttpClient<EmployeeApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7136/api/employees/");
});
builder.Services.AddHttpClient<TaskApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7136/api/tasks/");
});
// Detailed Errors
builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => { options.DetailedErrors = true; });


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
