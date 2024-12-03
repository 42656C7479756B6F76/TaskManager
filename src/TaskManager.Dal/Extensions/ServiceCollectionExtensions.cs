using TaskManager.Dal.Infrastructure;
using TaskManager.Dal.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Dal.Repositories;
using TaskManager.Dal.Repositories.Interfaces;

namespace TaskManager.Dal.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDalRepositories(
        this IServiceCollection services)
    {
        AddPostgresRepositories(services);
        AddRedisRepositories(services);
        
        return services;
    }

    private static void AddRedisRepositories(IServiceCollection services)
    {
        services.AddScoped<ITakenTaskRepository, TakenTaskRepository>();
    }

    private static void AddPostgresRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITaskLogRepository, TaskLogRepository>();
        services.AddScoped<ITaskCommentRepository, TaskCommentRepository>();
    }

    
    public static IServiceCollection AddDalInfrastructure(
        this IServiceCollection services, 
        IConfigurationRoot config)
    {
        //read config
        services.Configure<DalOptions>(config.GetSection(nameof(DalOptions)));
        
        //add migrations
        Postgres.AddMigrations(services);
        
        return services;
    }
}
