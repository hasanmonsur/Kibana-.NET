using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

// Set up the Serilog Logger explicitly
Log.Logger = new LoggerConfiguration()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://127.0.0.1:9200"))
    {
        AutoRegisterTemplate = true, // Automatically register template with Elasticsearch
        IndexFormat = "logstash-{0:yyyy.MM.dd}" // Log index naming convention
    })
    .MinimumLevel.Debug()
    //.MinimumLevel.Information()  // Adjust log level
    .Enrich.FromLogContext()     // Enrich logs with contextual information
    .Enrich.WithProperty("ApplicationName", "LogsKibanaApi")
    .CreateLogger();

// Add Serilog to the application's logging services manually
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSerilog();  // Manually add Serilog to the logging pipeline
});

// Other builder configurations (Add services, etc.)
builder.Services.AddControllers();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();
