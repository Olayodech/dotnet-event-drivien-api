using CommandService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMvc();
builder.WebHost.UseUrls(Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? "http://*:8080");
builder.Services.AddScoped<ICommandRepository, CommandRepository>();


builder.Services.AddDbContext<AppDbContext>(opt => {
    if(builder.Environment.IsDevelopment()) {
        // opt.UseInMemoryDatabase("inMem");
        opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsDbConnection"));
    } else {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsDbConnection"));
    }}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
