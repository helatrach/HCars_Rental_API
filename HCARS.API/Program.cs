using AutoMapper;
using Azure.Storage.Blobs;
using HCARS.Domain.IRepository;
using HCARS.Infrastructure.Context;
using HCARS.Infrastructure.Repositories;
using HCARS.Services.IServices;
using HCARS.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();





// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();


builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<HCarsDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
b => b.MigrationsAssembly(typeof(HCarsDbContext).Assembly.FullName)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 5;

}).AddEntityFrameworkStores<HCarsDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AuthSettings:Audience"],
        ValidIssuer = builder.Configuration["AuthSettings:Issuer"],
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:Key"])),
        ValidateIssuerSigningKey = true

    };
});

//builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(ICarService), typeof(CarService));
builder.Services.AddScoped(typeof(IBrandService), typeof(BrandService));
builder.Services.AddScoped(typeof(IUserService), typeof(UserService));
builder.Services.AddScoped(typeof(IFileManager), typeof(FileManager));
builder.Services.AddScoped(typeof(IRentalService), typeof(RentalService));
builder.Services.AddScoped(opt =>
{
    return new BlobServiceClient(builder.Configuration.GetConnectionString("AzureStorage"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cars API V1");
        c.RoutePrefix = string.Empty;
    });
    app.UseHsts();
}

app.UseCors(opt => opt.AllowAnyOrigin()
//opt.WithOrigins("http://localhost:4200")
.AllowAnyMethod()
.AllowAnyHeader());
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

app.Run();
