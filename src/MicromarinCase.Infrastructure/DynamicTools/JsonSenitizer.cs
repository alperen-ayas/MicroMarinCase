using MicroMarinCase.Application.Abstractions.DynamicTools;
using MicroMarinCase.Domain.AggregateRootModels.RecordModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MicroMarinCase.Infrastructure.DynamicTools
{
    public class JsonSenitizer : IJsonSenitizer
    {
        private readonly JsonSerializerOptions _jsonOptions;

        public JsonSenitizer(JsonSerializerOptions jsonOptions)
        {
            _jsonOptions = jsonOptions;
        }

        public List<Record> CollectRecords(JsonObject jsonObject, string recordType, string parentId = null, bool isFirst = false)
        {
            var records = new List<Record>();

            var filteredData = new JsonObject();
            foreach (var property in jsonObject)
            {
                if (property.Value is not JsonObject && property.Value is not JsonArray)
                {
                    filteredData.Add(property.Key, property.Value.DeepClone());
                }
            }

            var record = Record.Create(isFirst ? $"{recordType}_{Guid.NewGuid()}" : $"{recordType}_{Guid.NewGuid()}",
                recordType,
                isFirst ? null : parentId,
                JsonSerializer.Serialize(filteredData, _jsonOptions));

            records.Add(record);

            foreach (var property in jsonObject)
            {
                if (property.Value is JsonObject nestedObject)
                {
                    var newNestedObject = new JsonObject(nestedObject);
                    var nestedRecords = CollectRecords(newNestedObject, $"{recordType}_{property.Key}", record.Id, false);
                    records.AddRange(nestedRecords);
                }
                else if (property.Value is JsonArray jsonArray)
                {
                    foreach (var item in jsonArray)
                    {
                        if (item is JsonObject arrayObject)
                        {
                            var newArrayObject = new JsonObject();
                            foreach (var arrayProperty in arrayObject)
                            {
                                newArrayObject.Add(arrayProperty.Key, arrayProperty.Value.DeepClone());
                            }

                            var arrayRecords = CollectRecords(newArrayObject, $"{recordType}_{property.Key}", record.Id, false);
                            records.AddRange(arrayRecords);
                        }
                    }
                }
            }

            return records;
        }
    }
}
