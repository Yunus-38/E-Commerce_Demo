using Business.Abstract;
using Business.Concrete;
using Core.DependencyResolvers;
using Core.Utilities.Mapping.Abstract;
using Core.Utilities.Mapping.Concrete;
using Core.Utilities.Security.JWT;
using Core.Extensions;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Core.Utilities.IoC;
using Autofac.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Utilities.Security.Encryption;
using Business.DependencyResolvers.Autofac;
using Autofac;
using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IProductService, ProductManager>();
builder.Services.AddSingleton<IProductRepository, EfProductRepository>();

builder.Services.AddSingleton<IMapper, Mapper>();

builder.Services.AddSingleton<ITokenHelper, JwtHelper>();

builder.Services.AddSingleton<IUserRepository, EfUserRepository>();
builder.Services.AddSingleton<IUserService, UserManager>();
builder.Services.AddSingleton<IAuthService, AuthManager>();

builder.Services.AddDependencyResolvers(new ICoreModule[] {
                new CoreModule()
            });

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                                        .ConfigureContainer<ContainerBuilder>(builder =>
                                        {
                                            builder.RegisterModule(new AutofacBusinessModule());
                                        });

//Host.CreateDefaultBuilder(args)
//                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
//                .ConfigureContainer<ContainerBuilder>(builder =>

//                {
//                    builder.RegisterModule(new AutofacBusinessModule());

//                });


var app = builder.Build();
((JwtHelper)app.Services.GetService<ITokenHelper>()).TokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
