using AspNetCore.Environment.Extensions;
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

// Add Serilog
builder.Host.UseSerilog((hostContext, services, configuration) => {
    var url = builder.Configuration.GetConfiguration("ElasticSearch:Url");
    if (!string.IsNullOrEmpty(url))
    {
        configuration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(url))
        {
            AutoRegisterTemplate = true,
            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
            IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{builder.Environment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
        });
    }
    configuration.ReadFrom.Configuration(builder.Configuration);
});

var app = builder.Build();

app.UseCors("AllowAll");

app.UseIdentityServer();

app.Run();
