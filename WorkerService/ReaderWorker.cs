using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using WorkerService.Models;

namespace WorkerService
{
    public class ReaderWorker : BackgroundService
    {
        private readonly IMongoCollection<Row> _rows;
        private readonly ILogger<ReaderWorker> _logger;

        public ReaderWorker(IMongoClient client, ILogger<ReaderWorker> logger)
        {
            _rows = client.GetDatabase("gddkia").GetCollection<Row>("rows");
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var rows =await  _rows.AsQueryable()
                    .Select(x=>x.Road)
                    .ToListAsync(stoppingToken);
                foreach (var row in rows)
                {
                    Console.WriteLine($"{row}");
                }
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}