using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Core.Carts.Repositories;
using OnlineStore.Core.Carts.Services;
using OnlineStore.Core.Categories.Repositories;
using OnlineStore.Core.Categories.Services;
using OnlineStore.Core.Common.Cache;
using OnlineStore.Core.Common.DateTimeProviders;
using OnlineStore.Core.Common.Models;
using OnlineStore.Core.Common.Redis;
using OnlineStore.Core.Images.Repositories;
using OnlineStore.Core.Images.Services;
using OnlineStore.Core.ProductAttributes.Repositories;
using OnlineStore.Core.ProductAttributes.Services;
using OnlineStore.Core.Products.Repositories;
using OnlineStore.Core.Products.Services;
using OnlineStore.DataAccess.Carts.Repositories;
using OnlineStore.DataAccess.Categories.Repositories;
using OnlineStore.DataAccess.Common;
using OnlineStore.DataAccess.Images.Repositories;
using OnlineStore.DataAccess.ProductAttributes.Repositories;
using OnlineStore.DataAccess.Products.Repositories;
using OnlineStore.Domain.Entities;
using OnlineStore.Infrastructure.Mappings;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace OnlineStore.DependencyRegistrar
{
    /// <summary>
    /// Класс регистрации компонентов.
    /// </summary>
    public static class Registrar
    {
        public static void RegisterComponents(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<MutableOnlineStoreDbContext>()
                .AddDefaultTokenProviders();

            //var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            //services.AddAuthentication()
            //    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            ValidIssuer = jwtOptions.Issuer,
            //            ValidAudience = jwtOptions.Audience,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
            //        };
            //    });

            //services.AddMassTransit(x =>
            //{
            //    x.UsingRabbitMq((context, cfg) =>
            //    {
            //        cfg.Host("rabbitmq://localhost", h =>
            //        {
            //            h.Username("guest");
            //            h.Password("guest");
            //        });
            //        cfg.ConfigureEndpoints(context);
            //    });
            //});

            //var botConfig = configuration.GetSection("TelegramBotOptions").Get<TelegramBotOptions>();

            //services.AddSingleton<ITelegramBotClient>(provider =>
            //         new TelegramBotClient(botConfig.SecretToken));

            RegisterRepositories(services, configuration);
            RegisterServices(services, configuration);
            RegisterMapper(services);
        }

        private static void RegisterRepositories(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MutableOnlineStoreDbContext>(options =>
                options.UseNpgsql(connectionString)
            );
            services.AddDbContext<ReadonlyOnlineStoreDbContext>(options =>
                options.UseNpgsql(connectionString)
            );

            services.AddScoped<IAttributesRepository, AttributesRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
        }

        private static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            var redisConfiguration = configuration
                .GetSection("Redis")
                .Get<RedisConfiguration>();

            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfiguration);

            services.AddScoped<IProductAttributeService, ProductAttributeService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ICartService, CartService>();

            services.AddSingleton<IRedisCache, RedisCache>();
            services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.Configure<DecoratorSettings>(configuration.GetSection("DecoratorSettings"));
            var decorationSettings = configuration.GetSection("DecoratorSettings").Get<DecoratorSettings>();
            if (decorationSettings?.EnableDecoration == true)
            {
                services.Decorate<IProductAttributeService, CachedProductAttributeService>();
                services.Decorate<ICartService, CachedCartService>();
            }
        }

        private static void RegisterMapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProductAttributeMappingProfile());
                mc.AddProfile(new ProductMappingProfile());
                mc.AddProfile(new CategoryMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
