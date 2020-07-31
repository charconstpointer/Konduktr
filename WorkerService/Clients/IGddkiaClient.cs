using System.Threading.Tasks;
using WorkerService.Models;

namespace WorkerService.Clients
{
    public interface IGddkiaClient
    {
        Task<GddkiaResponse> GetReport();
    }
}