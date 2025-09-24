using Application.Interfaces;
using Application.UseCases;
using Infrastructure.DataSources;
using Microsoft.EntityFrameworkCore;
using Persistence.Repository;
using System.Reflection;
using Infrastructure.Context;

namespace SAV; 

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddDbContext<SavDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.CommandTimeout(300);
                }
            )
        );

        string executionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        string csvFolderPath = Path.Combine(executionPath, "CSVs");

        builder.Services.AddSingleton<IDataSource>(new CsvDataSource(csvFolderPath));
        builder.Services.AddScoped<IRepository, EfRepository>();
        builder.Services.AddScoped<ImportSalesUseCase>();

        builder.Services.AddHostedService<EtlWorker>();

        var host = builder.Build();
        await host.RunAsync();
    }
}