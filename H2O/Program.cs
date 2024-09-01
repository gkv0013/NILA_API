using DllLayer.PgSqlHelper;
using H2O.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // Configure Newtonsoft.Json serializer options
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    // Add any other Newtonsoft.Json configuration options here
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var configuration = app.Services.GetRequiredService<IConfiguration>();
PgsqlHelper.Initialize(configuration);
app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:3000") // Add your allowed origins here
           .AllowAnyHeader()
           .AllowAnyMethod();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Custom Middleware for handling responses
app.UseMiddleware<CustomResponseMiddleware>();


app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
