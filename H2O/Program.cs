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
if (app.Environment.IsDevelopment())
{
    app.UseCors(builder =>
    {
        builder.WithOrigins("http://localhost:4200", "https://nilaapi20240830153816.azurewebsites.net", "https://api.telegram.org/bot7092587891:AAHZqlRZsNqqsMEs5F6dhzstQL___NSEV3A", "https://coin-99c1e.firebaseapp.com")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
}
else
{
    app.UseCors(builder =>
    {
         builder.WithOrigins("http://localhost:4200", "https://nilaapi20240830153816.azurewebsites.net", "https://api.telegram.org/bot7092587891:AAHZqlRZsNqqsMEs5F6dhzstQL___NSEV3A/", "https://coin-99c1e.firebaseapp.com")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
}


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
