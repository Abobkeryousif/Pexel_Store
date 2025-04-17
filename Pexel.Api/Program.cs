using Microsoft.Extensions.FileProviders;
using Pexel.Application.DependecyInjection;
using Pexel.Infrastructrue.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.InfraRegister(builder.Configuration);
builder.Services.AppRegister();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();   
    app.UseSwaggerUI();   

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
