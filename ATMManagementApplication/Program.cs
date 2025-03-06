using Microsoft.EntityFrameworkCore;
using ATMManagementApplication.Data;

var builder = WebApplication.CreateBuilder(args);

// Add CORS service to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin() // Cho phép tất cả các origin
               .AllowAnyMethod() // Cho phép tất cả các phương thức (GET, POST, PUT, DELETE, ...)
               .AllowAnyHeader(); // Cho phép tất cả các header
    });
});

// Add service to container => thiết lập cấu hình data model
builder.Services.AddControllers();
builder.Services.AddDbContext<ATMContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 33))));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins"); // Sử dụng policy CORS
app.UseAuthentication();
app.MapControllers();

app.Run();
