using MicroMarinCase.Domain.SeedWorks;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace MicroMarinCase.Domain.AggregateRootModels.RecordModels
{
    public class Record : AggregateRoot
    {
        public override string Id { get; set; }
        public string RecordType { get; set; }
        public string? ParentId { get; set; }
        public Record? Parent { get; set; }
        public string Data { get; set; }
        public List<Record> Childs { get; set; }


        //For EfCore
        protected Record()
        {
            
        }

        public Record(string id,string recordType, string? parentId, string data, List<Record> childs)
        {
            Id = id;
            RecordType = recordType;
            ParentId = parentId;
            Data = data;
            Childs = new List<Record>();
        }

        public static Record Create(string id, string recordType, string parentId, string data, List<Record> childs = null)
        {
            return new Record(id, recordType, parentId, data, childs);
        }

        public void AddChilds(List<Record> childs = null)
        {
            Childs.AddRange(childs);
        }

        public void UpdateData(JsonObject data)
        {
            var currentData = JsonNode.Parse(Data)?.AsObject();
            

            if (currentData == null || data == null)
            {
                throw new ArgumentException("Geçersiz JSON verisi.");
            }

            foreach (var kvp in data)
            {
                if (!currentData.ContainsKey(kvp.Key))
                {
                    throw new Exception($"Güncelleme JSON'u geçersiz bir alan içeriyor: {kvp.Key}");
                }
                currentData[kvp.Key] = kvp.Value.DeepClone();
            }
            Data = currentData.ToJsonString(new JsonSerializerOptions { WriteIndented = true });

             
        }
    }
}
