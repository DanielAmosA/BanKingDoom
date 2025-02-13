using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using Server.Helpers.Middleware;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Server.Helpers.Service;
using Server.Helpers.ServiceInterfaces;
using Server.Helpers.Validator;
using System.Buffers.Text;
using Microsoft.Extensions.Options;
using Server.Models.ExternalProvider;
using Server.Models.Entity;
using Server.Models.Settings;
using Server.ModelsInterfaces.ExternalProvider;
using Server.Helpers.DB;
using Server.Helpers.DBInterfaces;
using Server.DAL;
using Server.DALInterfaces;
using Server.BL;
using Server.BLInterfaces;
using Server.Orchestration;
using Server.OrchestrationInterfaces;
using Server.BL.Factory;
using Server.ModelsInterfaces.Settings;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configure AES Action
var aesSettings = builder.Configuration.GetSection("AESSettings");
var key = aesSettings["Key"];
var iv = aesSettings["IV"];

builder.Services.AddSingleton<ISecurityService, SecurityService>(objSecurityService => new SecurityService(key!, iv!));

// Configure HttpClient
var httpClientSettings = builder.Configuration.GetSection("HttpClientSettings");
var baseUrl = httpClientSettings["BaseUrl"];
builder.Services.AddHttpClient<IAPIClient, ActionAPIClient>(client=>
{
    client.BaseAddress = new Uri(baseUrl!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Configure Services Configure
builder.Services.Configure<OpenBankingParameters>(builder.Configuration.GetSection("OpenBankingParameters"));
builder.Services.Configure<DbConfig>(builder.Configuration.GetSection("DbConfig"));

// Add services to the container.

// Configure Dependency injection

// AddScoped: Service depends on the context of the request
//           and needs to share data for a request of that request,

// DAL Layer
builder.Services.AddScoped<IRegisterUserDAL, RegisterUserDAL>();
builder.Services.AddScoped<ITransactionDAL, TransactionDAL>();

// Factory - BL Layer
builder.Services.AddScoped<IFactory<RegisterUserBL>, RegisterUserBLFactory>();
builder.Services.AddScoped<IFactory<TransactionBL>, TransactionBLFactory>();

// Orc Layer
builder.Services.AddScoped<IRegisterUserOrcRead, RegisterUserOrcRead>();
builder.Services.AddScoped<IRegisterUserOrcWrite, RegisterUserOrcWrite>();
builder.Services.AddScoped<ITransactionOrcRead, TransactionOrcRead>();
builder.Services.AddScoped<ITransactionOrcWrite, TransactionOrcWrite>();

// Configure Validators
builder.Services.AddScoped<IValidatorService<RegisterUser>, ValidatorRegisterUserService>();
builder.Services.AddScoped<IValidatorService<RegisterUserBasic>, ValidatorRegisterUserBasicService>();
builder.Services.AddScoped<IValidatorService<TransactionActionInsert>, ValidatorTransactionActionInsert>();
builder.Services.AddScoped<IValidatorService<TransactionActionBasic>, ValidatorTransactionActionBasic>();

// Configure Http Client Action API
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionHandlerService, TransactionHandlerService>();
builder.Services.AddScoped<ITransactionActionAPI, TransactionDepositActionAPI>();
builder.Services.AddScoped<ITransactionActionAPI, TransactionWithdrawalActionAPI>();

// AddSingleton: Service does not depend on a specific request
//              (e.g., services that perform fixed settings, database
//              access that only requires one share).

builder.Services.AddSingleton<IDataHelper, DataHelper>();

// Configure CORS (Cross-Origin)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClientSide",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure JWT (Authentication)
var jwtSettings = new JwtSettings();
builder.Configuration.GetSection("jwtSettings").Bind(jwtSettings);
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})

.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey!))
    };
});

builder.Services.AddAuthentication();

// Configure Log Tool
builder.Host.UseSerilog((context, configuration) =>
configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Middleware
app.UseMiddleware<LogRequest>();
app.UseMiddleware<ExceptionRequest>();
app.UseCors("AllowClientSide");

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
