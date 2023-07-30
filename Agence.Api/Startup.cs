using Microsoft.OpenApi.Models;
using System.Reflection;
using Agence.Api.Application.Services.Interfaces;
using Agence.Api.Application.Services;
using Agence.Api.Application.Repositories;
using Agence.Api.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using SendGrid;
using Agence.Api.Infrastructure;

public class Startup {

    private readonly string origins = "origins";
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllers();
        services.AddSingleton<IListingService, ListingService>();
        services.AddSingleton<IListingRepository, ListingRepository>();
        services.AddSingleton<IFavoriteService, FavoriteService>();
        services.AddSingleton<IFavoriteRepository, FavoriteRepository>();
        services.AddSingleton<IAppointmentService, AppointmentService>();
        services.AddSingleton<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IEmailRepository, EmailRepository>();
        services.AddScoped<IEmailService, EmailService>();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        services.AddCors(options =>
        {
            options.AddPolicy(name: origins, policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });
        });
        services.AddHttpClient();

        services.AddHealthChecksUI(setup =>
        {
            setup.SetEvaluationTimeInSeconds(60);
        })
            .AddInMemoryStorage();

        services.AddControllers();

        // Register the Swagger services
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Service Agence.Api",
                Description = "Api Agence.Api",
            });

            // Set the comments path for the Swagger JSON and UI. 
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
                c.IncludeXmlComments(xmlPath);
        });

        // Add SendGrid configuration
        IConfigurationSection sendGridConfig = Configuration.GetSection("SendGrid");
        services.Configure<SendGridOptions>(sendGridConfig);

        services.AddScoped(provider =>
        {
            var sendGridOptions = provider.GetRequiredService<IOptions<SendGridOptions>>();
            return new SendGridClient(sendGridOptions.Value.ApiKey);
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(origins);

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        // Enable middleware to serve generated Swagger as a JSON endpoint.
        app.UseSwagger();

        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
        // specifying the Swagger JSON endpoint.
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agence.Api");
        });
    }
}