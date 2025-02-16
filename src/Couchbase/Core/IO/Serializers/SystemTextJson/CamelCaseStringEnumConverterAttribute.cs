using System;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable enable

namespace Couchbase.Core.IO.Serializers.SystemTextJson
{
    internal class CamelCaseStringEnumConverter: JsonStringEnumConverter
    {
        public CamelCaseStringEnumConverter() : base(JsonNamingPolicy.CamelCase)
        {
        }
    }
}
