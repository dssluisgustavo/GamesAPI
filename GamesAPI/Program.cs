using GamesAPI;
using GamesAPI.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NPOI.SS.Formula.Functions;
using Repository;
using Repository.Interfaces;
using RepositoryEF.Models;
using RepositoryEF.Repository;
using Services;
using Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IGameService, GameService>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IPlatformService, PlatformService>();
builder.Services.AddTransient<IPublisherService, PublisherService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<IRefreshTokenProvider, RefreshTokenProvider>();
builder.Services.AddTransient<IJwtProvider, JwtProvider>();
builder.Services.AddTransient<IGameRepository, GamesRepository>();
builder.Services.AddTransient<IPlatformRepository, PlatformsRepository>();
builder.Services.AddTransient<IPublisherRepository, PublishersRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddControllers().AddNewtonsoftJson();
//Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "GamesAPI", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
}
);
builder.Services.ConfigureOptions<JwtOptionsSetUp>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

var connectionString = builder.Configuration["dbContextSettings:ConnectionString"];
builder.Services.AddDbContext<PostgresContext>((options) =>
{
    options.UseNpgsql(connectionString);
});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
