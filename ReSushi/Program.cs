using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReSushi;
using ReSushi.Models;
using ReSushi.Repository;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

IConfigurationRoot _confString = new ConfigurationBuilder().
    SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();

builder.Services.AddDbContext<EFDataContext>(options =>
               options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
//.AddEntityFrameworkStores<EFDataContext>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
//.AddEntityFrameworkStores<EFDataContext>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer( x =>
{
    var secret = builder.Configuration.GetValue<string>("Secret");
    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidAudience = "https://localhost:7051/",
        ValidIssuer = "https://localhost:7051/",
        IssuerSigningKey = key,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

//var jwtSection = builder.Configuration.GetValue<string>("JwtBearerTokenSettings");
//builder.Services.Configure<JwtBearerTokenSettings>(jwtSection);
//var jwtBearerTokenSettings = jwtSection.Get<JwtBearerTokenSettings>();
//var key = Encoding.ASCII.GetBytes(jwtSection.SecretKey);

/*var secret = builder.Configuration.GetValue<string>("Secret");
var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        //ValidIssuer = jwtBearerTokenSettings.Issuer,
        ValidIssuer = "https://localhost:7051/",
        ValidateAudience = true,
        //ValidAudience = jwtBearerTokenSettings.Audience,
        ValidAudience = "https://localhost:7051/",
        ValidateIssuerSigningKey = true,
        //IssuerSigningKey = new SymmetricSecurityKey(key),
        IssuerSigningKey = key,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});*/



builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "client",
            policy => policy.WithOrigins("http://localhost:3000")
                   .AllowAnyMethod()
                   .AllowAnyHeader());
    });

    builder.Services.AddSwaggerGen(options => {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "SuShI",
            Version = "v1"
        });
    });


    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "WEB API");
            c.DocumentTitle = "SuShI";
            c.DocExpansion(DocExpansion.List);
        });
    }



    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseCors("client");

    app.UseAuthorization();

    app.UseAuthentication();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
        RequestPath = "/Photos"
    });

using (var scope = app.Services.CreateScope())
using (var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>())
using (var db = scope.ServiceProvider.GetRequiredService<EFDataContext>())
{
    db.Database.Migrate();
    var user = await userManager.FindByNameAsync(Consts.UserName);

    if (user == null)
    {
        user = new IdentityUser(Consts.UserName);
        await userManager.CreateAsync(user, Consts.Password);
    }
}



app.Run();

