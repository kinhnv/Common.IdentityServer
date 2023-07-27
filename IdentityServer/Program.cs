using i3rothers.AspNetCore.Extensions;
using i3rothers.Domain.Extensions;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args)
    .AddACS();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        builder => builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});


// Add AddInfrastructure
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseCors("AllowAll");

app.UseIdentityServer();

app.Run();
