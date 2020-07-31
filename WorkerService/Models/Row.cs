using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace WorkerService.Models
{

    [BsonIgnoreExtraElements]
    public class Row
    {
        [JsonPropertyName("typ")]
        public string Type { get; set; }
        [JsonPropertyName("nr_drogi")]
        public string Road { get; set; }
        [JsonPropertyName("geo_lat")]
        public string Latitude { get; set; }
        [JsonPropertyName("geo_long")]
        public string Longitude { get; set; }

        public string Additional { get; set; } = "MS👔🧥🥼";
    }

    public class Data
    {
        [JsonPropertyName("utr")]
        public IEnumerable<Row> Rows { get; set; }
    }

    public class GddkiaResponse
    {
        [JsonPropertyName("utrudnienia")]
        public Data Data { get; set; }
    }
}