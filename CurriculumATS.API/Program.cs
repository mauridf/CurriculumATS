using CurriculumATS.Domain.Interfaces;
using CurriculumATS.Application.Services;
using CurriculumATS.Application.Settings;
using CurriculumATS.Persistence.Repositories;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;
using CurriculumATS.Infrastructure.Interfaces;
using DinkToPdf;
using DinkToPdf.Contracts;
using CurriculumATS.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration["MongoDbSettings:ConnectionString"]));
builder.Services.AddScoped(s =>
{
    var client = s.GetRequiredService<IMongoClient>();
    var settings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<IResumoRepository, ResumoRepository>();
builder.Services.AddScoped<IHabilidadeRepository, HabilidadeRepository>();
builder.Services.AddScoped<IFormacaoRepository, FormacaoRepository>();
builder.Services.AddScoped<IExperienciaProfissionalRepository, ExperienciaProfissionalRepository>();
builder.Services.AddScoped<ICertificacoesCertificadosRepository, CertificacoesCertificadosRepository>();

builder.Services.AddScoped<ICurriculoATSService, CurriculoATSService>();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddSingleton<IAuthService, AuthService>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins("http://localhost:4200"); // substitua com a URL do seu frontend se mudar depois
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CurriculumATS API V1");
        c.InjectStylesheet("/swagger/swagger-custom.css");
        c.RoutePrefix = string.Empty; // Faz o Swagger abrir direto na raiz (https://localhost:7265/)
    });
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
