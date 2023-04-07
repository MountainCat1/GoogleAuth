using Microsoft.AspNetCore.Authentication.JwtBearer;
using SimpleAccount;
using SimpleAccount.Features.GoogleAuthentication;
using SimpleAccount.Settings;

var builder = WebApplication.CreateBuilder(args);

// ========= CONFIGURATION  =========
var configuration = builder.Configuration;


configuration.AddJsonFile("Secrets/authentication.json");
var authenticationSettings = new AuthenticationSettings();
configuration.GetSection("AuthenticationSettings").Bind(authenticationSettings);

// Add services to the container.

var services = builder.Services;

services.AddSingleton<AuthenticationSettings>(authenticationSettings);

services.AddLogging();

services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Replace this with your frontend origin
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

services.AddScoped<IGoogleAuthProviderService, GoogleAuthProviderService>();

services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = "934344019711-htpk4uv143hibkpol9vka7fk9qaasq86.apps.googleusercontent.com";
        googleOptions.ClientSecret = "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";
        googleOptions.ClaimsIssuer = "https://accounts.google.com";
    })
    .AddJwtBearer(jwtBearerOptions =>
    {
        jwtBearerOptions.SecurityTokenValidators.Clear();
        jwtBearerOptions.SecurityTokenValidators.Add(new GoogleTokenValidator("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"));
    });

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();