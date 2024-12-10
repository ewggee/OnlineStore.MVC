using AutoMapper;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Core.Authentication.Services;
using OnlineStore.Core.Carts.Repositories;
using OnlineStore.Core.Carts.Services;
using OnlineStore.Core.Categories.Repositories;
using OnlineStore.Core.Categories.Services;
using OnlineStore.Core.Common.Cache;
using OnlineStore.Core.Common.DateTimeProviders;
using OnlineStore.Core.Common.Models;
using OnlineStore.Core.Common.Redis;
using OnlineStore.Core.Images;
using OnlineStore.Core.Images.Repositories;
using OnlineStore.Core.Images.Services;
using OnlineStore.Core.Orders.Repositories;
using OnlineStore.Core.Orders.Services;
using OnlineStore.Core.ProductAttributes.Repositories;
using OnlineStore.Core.ProductAttributes.Services;
using OnlineStore.Core.Products.Repositories;
using OnlineStore.Core.Products.Services;
using OnlineStore.Core.Users.Services;
using OnlineStore.DataAccess.Carts.Repositories;
using OnlineStore.DataAccess.Categories.Repositories;
using OnlineStore.DataAccess.Common;
using OnlineStore.DataAccess.Images.Repositories;
using OnlineStore.DataAccess.Orders.Repositories;
using OnlineStore.DataAccess.ProductAttributes.Repositories;
using OnlineStore.DataAccess.Products.Repositories;
using OnlineStore.Domain.Entities;
using OnlineStore.Infrastructure;
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

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                })
                .AddVkontakte(options =>
                {
                    options.ClientId = "52824834";
                    options.ClientSecret = "sKYrhShMqdHK1AyUGnWY";
                });

            RegisterOptions(services, configuration);
            RegisterRepositories(services, configuration);
            RegisterServices(services, configuration);
            RegisterMapper(services);
            RegisterHangfire(services, configuration);
        }

        private static void RegisterOptions(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ImageOptions>(configuration.GetSection("ImagesOptions"));
            services.Configure<PaginationOptions>(configuration.GetSection("PaginationOptions"));
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
            services.AddScoped<IOrderRepository, OrderRepository>();
        }

        private static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            var redisConfiguration = configuration
                .GetSection("Redis")
                .Get<RedisConfiguration>()!;

            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfiguration);

            services.AddScoped<IStoreAuthenticationService, AuthenticationService>();
            services.AddScoped<IProductAttributeService, ProductAttributeService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserService, UserService>();

            services.AddSingleton<IRedisCache, RedisCache>();
            services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.Configure<DecoratorSettings>(configuration.GetSection("DecoratorSettings"));
            var decorationSettings = configuration.GetSection("DecoratorSettings").Get<DecoratorSettings>()!;
            if (decorationSettings.AllowDecoration)
            {
                services.TryDecorate<IProductAttributeService, CachedProductAttributeService>();
                services.TryDecorate<ICartService, CachedCartService>();
            }
        }

        private static void RegisterMapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProductAttributeMappingProfile());
                mc.AddProfile(new ProductMappingProfile());
                mc.AddProfile(new CategoryMappingProfile());
                mc.AddProfile(new ImageMappingProfile());
                mc.AddProfile(new OrderItemMappingProfile());
                mc.AddProfile(new OrderMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private static void RegisterHangfire(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<JobScheduler>();

            services.AddHangfire(conf =>
                conf.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(options =>
                {
                    options.UseNpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
                })
            );

            services.AddHangfireServer();
        }
    }
}
