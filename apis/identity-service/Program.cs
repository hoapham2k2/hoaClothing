using System.Text;
using identity_service.Data;
using identity_service.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//for entity framework core
builder.Services.AddDbContext<AppDbContext>(options =>
{
    Console.WriteLine($"---> Configuring DbContext,connection string: {builder.Configuration.GetConnectionString("DefaultConnection")}");
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});


//for identity framework core
builder.Services.AddIdentity<AppUser, IdentityRole>(
        options =>
        {
            options.User.RequireUniqueEmail = true; //yêu cầu email là duy nhất trong hệ thống 
            options.Password.RequireDigit = true; //yêu cầu password có chữ số
            options.Password.RequireLowercase = true; //yêu cầu password có chữ thường
            options.Password.RequireUppercase = true; //yêu cầu password có chữ hoa
        })
    .AddEntityFrameworkStores<AppDbContext>();

//for jwtBearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata= false; //yêu cầu https là false để dùng http
        options.SaveToken = true; //lưu token vào httpcontext để dùng cho các request sau này
        options.TokenValidationParameters = new TokenValidationParameters() //cấu hình token validation parameters 
        {
            ValidateLifetime = true, //validate lifetime là true để token có thời hạn sử dụng 
            ValidateIssuerSigningKey = true, //validate issuer signing key là true để validate key 
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) //key
        };
    });
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "This is Identity Server")
    .RequireAuthorization("ApiScope"); 

app.Run();