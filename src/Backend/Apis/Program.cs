using Services.Middlewares.LoggingMiddleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCrossPlatformLogging(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCrossPlatformLogging(context => new LogContext
{
    Project = "Api",
    Module = context.Request.Path,
    User = context.User.Identity?.Name,
    CustomProperties = 
    {
        ["IP"] = context.Connection.RemoteIpAddress?.ToString(),
        ["Method"] = context.Request.Method,
        ["Route"]= app.Environment.IsDevelopment()? context.Request.Path:""
    }
});


app.UseHttpsRedirection();
app.MapControllers();

app.Run();
