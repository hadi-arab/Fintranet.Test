using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Fintranet.Test.Domain.RemoteDataModel
{
    public class DocumentsRemoteData
    {
        [JsonPropertyName("documents")]
        public object Result { get; set; }
    }
}
