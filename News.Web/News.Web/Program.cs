using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using News.Web.Data;
using News.Web.Data.Entities.Identity;
using News.Web.Helpers;
using News.Web.Mapper;
using News.Web.Middleware;
using News.Web.Services;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppEFContext>(options =>
   options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<AppEFContext>().AddDefaultTokenProviders();


// Add services to the container.
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<String>("JwtKey")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = signinKey,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// https://github.com/openiddict/openiddict-core/issues/518
// And
// https://github.com/aspnet/Docs/issues/2384#issuecomment-297980490
//var forwardOptions = new ForwardedHeadersOptions
//{
//    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
//};
//forwardOptions.KnownNetworks.Clear();
//forwardOptions.KnownProxies.Clear();

//app.UseForwardedHeaders(forwardOptions);



app.UseLoggerFile();

app.SeedData();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
var dir = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
if (!Directory.Exists(dir))
{
    Directory.CreateDirectory(dir);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(dir),
    RequestPath = "/images"
});


app.UseAuthentication();
app.UseAuthorization();


//app.UseMiddleware<ErrorHandlerMiddleware>();
//app.UseCustomExceptionHandler();

app.MapControllers();

app.Run();
