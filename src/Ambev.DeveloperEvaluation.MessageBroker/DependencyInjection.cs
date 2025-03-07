using Ambev.DeveloperEvaluation.MessageBroker.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Ambev.DeveloperEvaluation.MessageBroker;

public static class DependencyInjection
{
    public static void AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<HealthCheckPublisherOptions>(options =>
        {
            options.Delay = TimeSpan.FromSeconds(10);
        });

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.AddConsumer<OrderCancelled>(ConfigureConsumer);
            x.AddConsumer<OrderItemCancelled>(ConfigureConsumer);
            x.AddConsumer<OrderModified>(ConfigureConsumer);
            x.AddConsumer<OrderCreated>(ConfigureConsumer);

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMQ:Host"], configuration["RabbitMQ:Virtual"], h =>
                {
                    h.Username(configuration["RabbitMQ:Username"] ?? throw new InvalidOperationException("RabbitMQ:Username is missing."));
                    h.Password(configuration["RabbitMQ:Password"] ?? throw new InvalidOperationException("RabbitMQ:Password is missing."));
                });

                cfg.ConfigureJsonSerializerOptions(options =>
                {
                    options.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    options.PropertyNameCaseInsensitive = true;
                    return options;
                });

                cfg.ConfigureEndpoints(context);
            });
            return;

            void ConfigureConsumer<T>(IConsumerConfigurator<T> cfg) where T : class, IConsumer
            {
                cfg.UseMessageRetry(r => r.Interval(2, TimeSpan.FromSeconds(10)));
            }
        });
    }
}