using Microsoft.EntityFrameworkCore;
using AwesomeCSharpNvim.Models.DTO;
using AwesomeCSharpNvim.Persistence;
using AwesomeCSharpNvim.Persistence.Repositories;
using AwesomeCSharpNvim.Persistence.Repositories.Interfaces;
using AwesomeCSharpNvim.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddTransient<IPluginRepository, PluginRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var context = services.GetRequiredService<ApplicationDbContext>();
	context.Database.Migrate();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapPost("/", async (PluginDTO pluginDTO, IPluginRepository repository) => await repository.AddAsync(pluginDTO)); 
app.MapGet("/", async (IPluginRepository repository) => await repository.GetAllAsync());
app.MapGet("/{id:int}", async (int id, IPluginRepository repository) => await repository.GetByIdAsync(id));
app.MapPut("/{id:int}", async (int id, PluginDTO pluginDTO, IPluginRepository repository) => await repository.UpdateAsync(id, pluginDTO)); 
app.MapDelete("/{id:int}", async (int id, IPluginRepository repository) => await repository.RemoveByIdAsync(id));
app.Run();