using Asp.Versioning;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// CORS configuration
builder.Services.AddCors(options => {
    options.AddPolicy("AllowSpecificOrigins",
        policy => {
            policy.WithOrigins("http://localhost:3000",
                               "http://localhost:5026",
                               "https://dev-dataprocess.vercel.app",
                               "https://dataprocess-react-app.vercel.app",
                               "https://dev-dataprocess-api.azurewebsites.net",
                               "https://dataprocess-api.azurewebsites.net")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB Contexts - Entity
MySqlServerVersion mySqlVersion = new MySqlServerVersion(new Version(8, 0, 39));

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySql(Environment.GetEnvironmentVariable("STRING_CONEXAO_MYSQL") ?? builder.Configuration.GetConnectionString("DefaultConnection"), mySqlVersion)
);

// API Versioning
builder.Services.AddApiVersioning(options => {
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = false;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options => {
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

// Add Authentication
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.SaveToken = true;
    options.RequireHttpsMetadata = true; // Alterado para produção
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET") ?? builder.Configuration["JWT:Secret"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"));

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
