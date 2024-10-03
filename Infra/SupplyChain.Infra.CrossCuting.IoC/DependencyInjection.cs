using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SupplyChain.Application.Interfaces;
using SupplyChain.Application.Mapper;
using SupplyChain.Application.Services;
using SupplyChain.Domain.Bus;
using SupplyChain.Domain.Interface.Notification;
using SupplyChain.Domain.Interface.Repository;
using SupplyChain.Infra.CrossCutting.Notification;
using SupplyChain.Infra.Data.Context;
using SupplyChain.Infra.Data.Repository;

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
        AddMapper(services);
        ConfigureIoC(services);
    }

    /// <summary>
    /// Configura a inversão de dependências
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
    private static void ConfigureIoC(IServiceCollection services)
    {
        services.AddScoped<IMercadoriaRepository, MercadoriaRepository>();
        services.AddScoped<IMercadoriaAppService, MercadoriaAppService>();
    }

    /// <summary>
    /// Configura os serviços que irão fazer parte do Bus
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
    private static void AddBus(IServiceCollection services)
    {
        services.AddScoped<INotify, Notify>();
        services.AddScoped<Bus>();
    }

    /// <summary>
    /// Configura os dbContext da aplicação
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
    /// <param name="configuration">Configurações da aplicação</param>
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InventarioDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
    }
    
    /// <summary>
    /// Configura o swagger da aplicação
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
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
    
    /// <summary>
    /// Configura o automapper
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
    private static void AddMapper(IServiceCollection services)
    {
        var mapper = MappingConfiguration.RegisterMappings().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}