using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SignalRTest.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddDbContext<ChatDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
builder.Services.AddScoped<MyHub>();




builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((host) => true)
                   .AllowCredentials();
        }));

var app = builder.Build();


// app.UseHttpsRedirection();
// app.UseStaticFiles();
// app.UseRouting();

app.UseCors("CorsPolicy");


// app.UseAuthorization();
app.Urls.Add("http://*:8885");
app.MapControllers();
app.MapHub<MyHub>("/MyHub");

app.Run();