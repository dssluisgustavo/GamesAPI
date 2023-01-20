using GamesAPI;
using NPOI.SS.Formula.Functions;
using Repository;
using Repository.Interfaces;
using Services;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IGameService, GameService>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IPlatformService, PlatformService>();
builder.Services.AddTransient<IPublisherService, PublisherService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<IJwtProvider, JwtProvider>();
builder.Services.AddTransient<IGameRepository, GameRepository>();
builder.Services.AddTransient<IPlatformRepository, PlatformRepository>();
builder.Services.AddTransient<IPublisherRepository, PublisherRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI((c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GamesAPI v1")));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
