using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using WorkerService.Clients;
using WorkerService.Models;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IGddkiaClient _gddkia;
        private readonly IMongoCollection<Row> _rows;

        public Worker(ILogger<Worker> logger, IGddkiaClient gddkia, IMongoClient client)
        {
            _logger = logger;
            _gddkia = gddkia;
            _rows = client.GetDatabase("gddkia").GetCollection<Row>("rows");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // _logger.LogInformation("Fetching GDDKIA");
                var report = await _gddkia.GetReport();
                // _logger.LogInformation("Persisting rows");
                await _rows.InsertManyAsync(report.Data.Rows, cancellationToken: stoppingToken);
                // _logger.LogInformation("Waiting");
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}