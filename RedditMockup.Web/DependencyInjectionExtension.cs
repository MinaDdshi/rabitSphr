using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.DtoBusinesses;
using RedditMockup.Business.EntityBusinesses;
using RedditMockup.Common.Constants;
using RedditMockup.Common.Dtos;
using RedditMockup.Common.Profiles;
using RedditMockup.Common.Validations;
using RedditMockup.DataAccess;
using RedditMockup.DataAccess.Context;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.Model.Entities;
using Sieve.Services;
//using StackExchange.Redis;

namespace RedditMockup.Web;

internal static class DependencyInjectionExtension
{
    internal static IServiceCollection InjectApi(this IServiceCollection services) =>
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            })
            .Services;

    internal static IServiceCollection InjectSwagger(this IServiceCollection services) =>
        services.AddSwaggerGen();

    internal static IServiceCollection InjectUnitOfWork(this IServiceCollection services) =>
        services.AddScoped<IUnitOfWork, UnitOfWork>();

    internal static IServiceCollection InjectContext(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment environment)
    {
        if (environment.IsEnvironment("Testing") || environment.IsDevelopment())
        {
            return services.AddDbContextPool<RedditMockupContext>(options => options.UseInMemoryDatabase("RedditMockup"));
        }

        return services.AddDbContextPool<RedditMockupContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"));
            options.EnableSensitiveDataLogging();
        });
    }

    internal static IServiceCollection InjectNLog(this IServiceCollection services)

    {
        var factory = NLogBuilder.ConfigureNLog("nlog.config");

        return services.AddSingleton(_ => factory.GetLogger("Info"))
            .AddSingleton(_ => factory.GetLogger("Error"));
    }

    internal static IServiceCollection InjectSieve(this IServiceCollection services) =>
        services.AddScoped<ISieveProcessor, SieveProcessor>();

    internal static IServiceCollection InjectAuthentication(this IServiceCollection services) =>
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            })
            .AddCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.Headers["Location"] = context.RedirectUri;
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };

                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.Headers["Location"] = context.RedirectUri;
                    context.Response.StatusCode = 403;
                    return Task.CompletedTask;
                };
            })
            .Services
            .AddAuthorization(options =>
            {
                options.AddPolicy(PolicyConstants.Admin,
                    policy => policy.RequireClaim(RoleConstants.Admin));
                options.AddPolicy(PolicyConstants.User,
                    policy => policy.RequireClaim(RoleConstants.User));
            });

    /*
    internal static IServiceCollection InjectBusinesses(this IServiceCollection services) =>
        services.Scan(scan =>
            scan.FromAssembliesOf(typeof(IBaseBusiness<>))
                .AddClasses(classes =>
                    classes.AssignableTo(typeof(IBaseBusiness<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes =>
                    classes.Where(predicate =>
                        predicate.Name.EndsWith("Business") &&
                        !predicate.IsAssignableTo(typeof(IBaseBusiness<>))))
                .AsSelf()
                .WithScopedLifetime());
    */

    internal static IServiceCollection InjectBusinesses(this IServiceCollection services) =>
        services.AddScoped<IBaseBusiness<User>, UserBusiness>()
        .AddScoped<IBaseBusiness<Answer>, AnswerBusiness>()
        .AddScoped<IBaseBusiness<Question>, QuestionBusiness>()
        .AddScoped<IBaseBusiness<Bookmark>, BookmarkBusiness>()
        .AddScoped<AccountBusiness>()
        .AddScoped<IDtoBaseBusiness<UserDto>, UserDtoBusiness>()
        .AddScoped<IDtoBaseBusiness<AnswerDto>, AnswerDtoBusiness>()
        .AddScoped<IDtoBaseBusiness<QuestionDto>, QuestionDtoBusiness>()
        .AddScoped<IDtoBaseBusiness<BookmarkDto>, BookmarkDtoBusiness>();


    internal static IServiceCollection InjectFluentValidation(this IServiceCollection services) =>
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<RoleValidator>();

    internal static IServiceCollection InjectAutoMapper(this IServiceCollection services) =>
        services.AddAutoMapper(typeof(UserProfile).Assembly);

    #region [Redis Injection]

    /* 
       
    internal static IServiceCollection InjectRedis(this IServiceCollection services, IConfiguration configuration) =>
            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(configuration["RedisConnection"]));
    */

    #endregion

}
