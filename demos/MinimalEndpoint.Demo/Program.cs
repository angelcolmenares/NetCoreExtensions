using Microsoft.Extensions.Options;
using MinimalEndpoint.Demo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuth();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddServices(typeof(Program).Assembly);

builder.Services.Configure<DemoOptions>(builder.Configuration.GetSection("ExternalProvider:" + nameof(DemoOptions)));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints(typeof(Program).Assembly);

app.MapGet("demo",
    (IOptions<DemoOptions> options) => options.Value?.RemoteAddress?? "nothing");
app.Run();

public record DemoOptions { public string RemoteAddress { get; set; } = default!; }