using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SupplyChain.Domain.Bus;
using SupplyChain.Domain.Interface;
using SupplyChain.Infra.CrossCutting.Notification;
using SupplyChain.Infra.Data.Context;

namespace SupplyChain.Infra.CrossCuting.IoC;

public static class DependencyInjection
{
    /// <summary>
    ///  Configura e adiciona as infraestruturas necessárias para a aplicação.
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
    /// <param name="configuration">Configurações da aplicação</param>
    public static void AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddSwagger(services);
        AddDbContext(services, configuration);
        AddBus(services);
    }

    private static void AddBus(IServiceCollection services)
    {
        services.AddScoped<INotify, Notify>();
        services.AddScoped<Bus>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InventarioDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
    }
    
    private static void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            var xmlFileService = $"{Assembly.Load("SupplyChain.Api").GetName().Name}.xml";
            var xmlPathService = Path.Combine(AppContext.BaseDirectory, xmlFileService);
            options.IncludeXmlComments(xmlPathService);
            
            var xmlFileApplication = $"{Assembly.Load("SupplyChain.Application").GetName().Name}.xml";
            var xmlPathApplication = Path.Combine(AppContext.BaseDirectory, xmlFileApplication);
            options.IncludeXmlComments(xmlPathApplication);
            
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Supply Chain Api",
                Description = "Api para gerenciamento de mercadorias",
                Contact = new OpenApiContact
                {
                    Name = "Email",
                    Email = "jose.csousa22@gmail.com"
                }
            });
        });
    }
}