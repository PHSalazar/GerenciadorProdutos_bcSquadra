using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using webapi_Produtos.Data;
using webapi_Produtos.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//options.SwaggerDoc("v1",
//        new Microsoft.OpenApi.Models.OpenApiInfo
//        {
//            Title = "Api Gerenciador de Produtos",
//            Version = "v1",
//            Description = "API para Gerenciamento de Produtos"
//        }
//    );

//var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Bootcamp .NET C#",
            Version = "2024.2",
            Description = "API CRUD"
        });

    string applicationBasePath = AppContext.BaseDirectory;
    string applicationName = AppDomain.CurrentDomain.FriendlyName;
    string xmlDocumentPath = Path.Combine(applicationBasePath, $"{applicationName}.xml");

    if (File.Exists(xmlDocumentPath))
    {
        options.IncludeXmlComments(xmlDocumentPath);
    }

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT desta maneira: Bearer SEU_TOKEN",
        Name = "Authorization",
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
});


builder.Services.AddScoped<IProduto, ProdutoService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
