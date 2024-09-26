using MicroMarinCase.Domain.AggregateRootModels.RecordModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;

namespace MicroMarinCase.Application.Abstractions.DynamicTools
{
    public interface IJsonSenitizer
    {
        public List<Record> CollectRecords(JsonObject jsonObject, string recordType, string parentId = null, bool isFirst = false);
    }
}
