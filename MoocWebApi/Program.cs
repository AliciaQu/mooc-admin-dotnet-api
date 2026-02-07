using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.WebEncoders;
using Mooc.Application;
using Mooc.Core;
using Mooc.Core.Attributes;
using Mooc.Model.DBContext;
using MoocWebApi.Filters;
using MoocWebApi.Init;
using MoocWebApi.Middlewares;
using NLog;
using NLog.Web;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.Json;


namespace MoocWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Early init of NLog to allow startup and exception logging, before host is built
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");
            var defaultPolicy = "AllowAllOrigins";
            try
            {
                var builder = WebApplication.CreateBuilder(args);
                builder.WebHost.UseKestrel(options =>
                {
                    // Handle requests up to 50 MB
                    options.Limits.MaxRequestBodySize = 52428800;
                });


                //autofac
                builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
                builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
                {
                    containerBuilder.RegisterModule<AutofacModule>();
                });

                // Configure application to use UTF-8 encoding  
                builder.Services.Configure<WebEncoderOptions>(options =>
                {
                    options.TextEncoderSettings = new System.Text.Encodings.Web.TextEncoderSettings(System.Text.Unicode.UnicodeRanges.All);
                });

                // Add services to the container.
                builder.Services.AddAppCore(builder.Configuration);

                //Add Mooc Application services
                builder.Services.AddApplication();

                builder.Services.AddTransient<ExceptionHandlingMiddleware>();
                //
                builder.Services.Configure<DataBaseOption>(builder.Configuration.GetSection(DataBaseOption.Section));
                var dataBaseOption = builder.Configuration.GetSection(DataBaseOption.Section).Get<DataBaseOption>();
                builder.Services.AddDbContext<MoocDBContext>(option =>
                {
                    var connectString = dataBaseOption?.ConnectionString;
                    var type = dataBaseOption?.Type;
                    if (type == Mooc.Shared.Enum.DBType.Sqlite)
                        option.UseSqlite(connectString);
                    else if (type == Mooc.Shared.Enum.DBType.SqlServer)
                        option.UseSqlServer(connectString);
                    else if (type == Mooc.Shared.Enum.DBType.MySql)
                        option.UseMySql(connectString, ServerVersion.AutoDetect(connectString));
                    else
                        option.UseSqlite(connectString);

                });

                builder.Services.AddControllers(options =>
                {
                    options.Filters.Add<ValidateModelFilter>();
                    options.Filters.Add<UnifiedResultFilter>();
                    //Handle requests up to 50 MB
                    options.Filters.Add(new RequestFormLimitsAttribute() { BufferBodyLengthLimit = 52428800 });
                }).ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                }).AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

                // NLog: Setup NLog for Dependency injection
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();

                builder.Services.AddSwaggerMooc();

                //Configure UploadFolderOption
                builder.Services.Configure<UploadFolderOption>(builder.Configuration.GetSection(UploadFolderOption.Section));
                var uploadFolderOption = builder.Configuration.GetSection(UploadFolderOption.Section).Get<UploadFolderOption>();


                //CORS
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy(defaultPolicy,
                        builder =>
                        {
                            builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                        });
                });

                var app = builder.Build();

                // Add detection statements: Check if API endpoints are correctly discovered
                var actionDescriptorProvider = app.Services.GetRequiredService<Microsoft.AspNetCore.Mvc.Infrastructure.IActionDescriptorCollectionProvider>();
                var actionDescriptors = actionDescriptorProvider.ActionDescriptors.Items;
                Console.WriteLine($"Number of discovered API endpoints: {actionDescriptors.Count}");
                
                // Group and count by controller
                var controllerGroups = actionDescriptors
                    .Where(ad => ad.RouteValues.ContainsKey("controller"))
                    .GroupBy(ad => ad.RouteValues["controller"]);
                
                foreach (var group in controllerGroups)
                {
                    Console.WriteLine($"Controller: {group.Key}, Number of endpoints: {group.Count()}");
                    foreach (var ad in group)
                    {
                        var routeTemplate = ad.AttributeRouteInfo?.Template ?? "No route template";
                        // Get HTTP method information
                        string httpMethod = "No HTTP method";
                        // Try to get HTTP method from endpoint metadata
                        var methodAttributes = ad.EndpointMetadata.OfType<Microsoft.AspNetCore.Mvc.Routing.HttpMethodAttribute>();
                        if (methodAttributes.Any())
                        {
                            httpMethod = string.Join(", ", methodAttributes.SelectMany(m => m.HttpMethods));
                        }
                        Console.WriteLine($"  - {httpMethod}: {routeTemplate}");
                    }
                }

                app.UseCors(defaultPolicy);
                app.UseMiddleware<ExceptionHandlingMiddleware>();
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwaggerMooc();
                }
                app.UseAuthentication();
                app.UseAuthorization();


                app.MapControllers();

                var uploadFolder = Path.Combine(builder.Environment.ContentRootPath, uploadFolderOption.RootFolder);
                var staticFile = new StaticFileOptions();
                staticFile.FileProvider = new PhysicalFileProvider(uploadFolder);
                app.UseStaticFiles(staticFile);


               // using (var socpe = app.Services.CreateScope())
               // {
               //     var dbSeedDataManagementService = socpe.ServiceProvider.GetRequiredService<IDBSeedDataManagementService>();
              //      dbSeedDataManagementService.IntiAsync().GetAwaiter().GetResult();
              //  }

                app.Run();
            }
            catch (Exception exception)
            {
                // NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }
    }
}
