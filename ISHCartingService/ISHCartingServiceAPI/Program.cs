using ISHCartingService.ISHCartingServiceDAL;
using ISHCartingServiceAPI.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register MongoDB context
builder.Services.AddSingleton<MongoDBContext>();

// Register CartingDataManager
builder.Services.AddTransient<CartingDataManager>();

// Register CartingService
builder.Services.AddTransient<CartingService>();

// Register ItemListener as a singleton service
builder.Services.AddSingleton<ItemListener>();

// Register a hosted service to manage the lifecycle of ItemListener
builder.Services.AddHostedService<ItemListenerHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();