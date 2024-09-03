using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Web6.Enti;
using Web6.Controllers;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MyAllowSpecificOrigins,
//                      policy =>
//                      {
//                          policy.AllowAnyOrigin();
//                      });
//});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
builder.Services.AddDbContext<BANHMIContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Web")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseCors(MyAllowSpecificOrigins);
app.UseCors("AllowSpecificOrigins");
app.UseAuthorization();

app.MapControllers();

//app.MapWebActionEndpoints();

app.Run();
