using NLog;
using NLog.Web;
using RedditMockup.DataAccess.Context;
using RedditMockup.Web;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Logging.SetMinimumLevel(LogLevel.Trace);

builder.Host.UseNLog();

var logger = NLogBuilder
        .ConfigureNLog("nlog.config")
        .GetLogger("Info");

try
{
    builder.Services
        .AddEndpointsApiExplorer()
        .InjectApi()
        .InjectSwagger()
        .InjectUnitOfWork()
        .InjectSieve()
        .InjectAuthentication()
        .InjectNLog()
        .InjectContext(builder.Configuration, builder.Environment)
        .InjectBusinesses()
        .InjectFluentValidation()
        .InjectAutoMapper()
        .AddHealthChecks();
    //.InjectRedis(builder.Configuration)

    var app = builder.Build();

    await using var scope = app.Services.CreateAsyncScope();

    await using var context = scope.ServiceProvider.GetRequiredService<RedditMockupContext>();

    if (!app.Environment.IsProduction())
    {
        app.UseSwagger()
                .UseSwaggerUI();

        await context.Database.EnsureDeletedAsync();

        await context.Database.EnsureCreatedAsync();
    }

    else
    {
        app.UseExceptionHandler();
        app.UseHsts();
    }

    app
        .UseHttpsRedirection()
        .UseRouting()
        .UseAuthentication()
        .UseAuthorization()
        .UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/healthcheck");
        });

    await app.RunAsync();
}
catch (Exception exception)
{
    logger.Error(exception, "Program Stopped Because of Exception !");
    throw;
}
finally
{
    LogManager.Shutdown();
}

public partial class Program
{
}