using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using WorkerService.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WorkerService.Clients
{
    public class GddkiaClient : IGddkiaClient
    {
        private readonly HttpClient _httpClient;

        public GddkiaClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GddkiaResponse> GetReport()
        {
            var data = await _httpClient.GetStringAsync("https://www.gddkia.gov.pl/dane/zima_html/utrdane.xml");
            var doc = new XmlDocument();
            doc.LoadXml(data);
            Console.WriteLine(doc.FirstChild.Attributes);
            var json = JsonConvert.SerializeXmlNode(doc);
            var foo = JsonSerializer.Deserialize<GddkiaResponse>(json);
            return foo;
        }
    }
}