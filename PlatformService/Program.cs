using AsyncDataServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PlatformService.Data;
using PlatformService.Repository;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.WebHost.UseUrls(Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? "http://*:8080");

builder.Services.AddMvc();
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => {
    options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
});
builder.Services.AddDbContext<AppDbContext>(options => {
    if(builder.Environment.IsDevelopment()) {
        options.UseInMemoryDatabase("inMem");
        // options.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsDbConnection"));
    }  else {
                options.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsDbConnection"));

    }
    });
builder.Services.AddScoped<IPlatformRepository, PlatformRepositoryImpl>();
builder.Services.AddHttpClient<ICommandDataClient, CommandDataClientImpl>();
builder.Services.AddSingleton<IMessageClient, MesaageBusClientImpl>(); //a single connnection in the lifetime
var app = builder.Build();
// Configure the HTTP request pipeline.
Console.WriteLine("------> Command connection url is", builder.Configuration.GetConnectionString("CommandService"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}  
if (app.Environment.IsProduction()) {
    Console.WriteLine("----> Running in Production Mode");
    builder.Services.AddDbContext<AppDbContext>(option => {
        option.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsDbConnection"));
    });
    PrepDb.PrepPopulation(app, app.Environment.IsProduction());
}
app.UseHttpsRedirection();

// app.UseCookiePolicy(new CookiePolicyOptions {
//     MinimumSameSitePolicy = SameSiteMode.None,
//     HttpOnly = HttpOnlyPolicy.Always,
//     Secure = CookieSecurePolicy.Always,
// });

app.MapControllers();
app.Run();


