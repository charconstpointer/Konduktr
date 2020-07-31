using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using WorkerService.Clients;
using WorkerService.Clients.Policies;

namespace WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IMongoClient>(new MongoClient());
                    services.AddHttpClient<IGddkiaClient, GddkiaClient>()
                        .AddPolicyHandler(GddkiaPolicy.GetRetryPolicy());
                    services.AddHostedService<Worker>();
                    services.AddHostedService<ReaderWorker>();
                });
    }
}