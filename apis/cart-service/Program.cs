using System.Security.Claims;
using System.Text;
using AutoMapper;
using cart_service.Data;
using cart_service.MappingProfile;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Entity Framework Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//for auto mapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

//for authentication and authorization
var mySymmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])); 
builder.Services.AddSingleton(mySymmetricKey); 
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true; //to save token in HttpContext for later use (e.g. for refresh token)
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = true, //RequireExpirationTime: yêu cầu token phải có claim exp (hạn của token)
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = mySymmetricKey,
            ClockSkew = TimeSpan.Zero, //ClockSkew: thời gian cho phép trễ của token so với server
            RoleClaimType = ClaimTypes.Role //RoleClaimType: claim type dùng để lưu role của user    
        };
    });

builder.Services.AddAuthorization(
    options =>
    {
        options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
        options.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
    }
);

// for dependency injection of repositories and interfaces




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

app.Run();