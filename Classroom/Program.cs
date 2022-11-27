using System.Globalization;
using Classroom.Context;
using Classroom.Entities;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddDataAnnotationsLocalization(options => {
      options.DataAnnotationLocalizerProvider = 
            (type ,factory) => factory.Create(typeof(Program));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLocalization(options => {
  options.ResourcesPath = "Resources";
}); 

builder.Services.AddCors(option => 
{
    option.DefaultPolicyName = "AllOrigin";
    option.AddPolicy("AllOrigin", corsPolicyBuilder => 
    {
         corsPolicyBuilder.   
         AllowAnyHeader()
         .AllowAnyMethod()
         .AllowAnyOrigin(); //, mana shu holatda istalgan joydan sorov yuborish mumkin.
        // corsPolicyBuilder
        //     .AllowAnyHeader()
        //     .AllowAnyMethod()
        //     .WithOrigins("https://localhost:44398")
        //     .AllowCredentials();
    });
});

builder.Services.AddDbContext<AppDbContext>(options => 
{
    options.UseLazyLoadingProxies().UseSqlite(builder.Configuration.GetConnectionString("Data"));
});

builder.Services.AddIdentity<User, Role>(options => 
{
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();
app.UseRequestLocalization(options =>
{
    options.DefaultRequestCulture = new RequestCulture(new CultureInfo("Ru"));
    options.SupportedUICultures = new List<CultureInfo>()
    {
        new CultureInfo("Ru"),
        new CultureInfo("Uz"),
        new CultureInfo("En"),
    };
    options.SupportedCultures = new List<CultureInfo>()
    {
        new CultureInfo("Ru"),
        new CultureInfo("Uz"),
        new CultureInfo("En"),
    };
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
