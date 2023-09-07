using System.Security.Claims;
using System.Text;
using AutoMapper;
using identity_service.AutoMapper;
using identity_service.Data;
using identity_service.Models;
using identity_service.Repositories.AuthRepository;
using identity_service.Repositories.JwtRepository;
using identity_service.Repositories.RoleRepository;
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


//For Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options=>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection") ?? throw new InvalidOperationException()));

// For Authentication JWT Token Bearer Configuration
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

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

//For AutoMapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

//for DI of Repositories
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IJwtRepository, JwtRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();


//for cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "client:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello, This is Identity Service!");

app.Run();