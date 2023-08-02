using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using tmdb.DataAccess.Concrete.EntityFramework.Contexts;
using tmdb.Service.DependencyResolvers.Autofac;
using tmdb.Core.Extensions;
using tmdb.WebAPI.AppServices;
using tmdb.WebAPI.FacadeServices.Abstract;
using tmdb.WebAPI.FacadeServices.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacServiceModule());
    });


string sqlLocalDb = builder.Configuration.GetConnectionString("RepositoryConnectionString");

builder.Services.AddDbContext<AppRepositoryContext>(options =>
{
    options.UseSqlServer(sqlLocalDb);
});

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("RepositoryConnectionString"), new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    }));

builder.Services.AddHangfireServer();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IWatchListFacadeService, WatchListFacadeService>();
builder.Services.AddScoped<IMovieFacadeService, MovieFacadeService>();
builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddCors(o => o.AddPolicy("AllowOrigin", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//                           /\
//________________________  //\\
//     UP IS Services    |   ||
//------------------------   ||
//                           --


//                           --
//________________________   ||
//  Down IS MiddleWares  |   ||
//------------------------  \\//
//                           \/



var app = builder.Build();

app.UseHangfireServer();
app.UseHangfireDashboard();

var serviceProvider = app.Services;
using var scope = serviceProvider.CreateScope();
var recurringJob = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
var movieService = scope.ServiceProvider.GetRequiredService<IMovieService>();

recurringJob.AddOrUpdate(
    "CheckWatchlist",
    () => movieService.CheckWatchlistAndSendEmailAsync(),
    Cron.Weekly(DayOfWeek.Sunday, 19, 30)); // Here we removed the timezone parameter


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

app.UseCors(options => options.WithOrigins("*").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
