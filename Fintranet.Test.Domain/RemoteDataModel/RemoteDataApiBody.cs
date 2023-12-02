using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Fintranet.Test.Domain.RemoteDataModel
{
    public class RemoteDataApiBody
    {
        [JsonPropertyName("dataSource")]
        public string DataSource { get; set; }

        [JsonPropertyName("database")]
        public string Database { get; set; }

        [JsonPropertyName("collection")]
        public string Collection { get; set; }

    }
}
