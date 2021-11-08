using System;
using Newtonsoft.Json;

namespace PasteMystNet
{

    public class PasteMystEdit
    {

        [JsonProperty("editedAt")] private readonly long _editedAt;
        [JsonProperty("editType")] private readonly string _editType;

        [JsonProperty("_id")] public string Id { get; init; }
        [JsonProperty("editId")] public string EditId { get; init; }
        [JsonProperty("metadata")] public string[] Metadata { get; init; }
        [JsonProperty("edit")] public string Edit { get; init; }
        [JsonIgnore] public PasteMystEditType EditType => (PasteMystEditType)Enum.Parse(typeof(PasteMystEditType), _editType);
        [JsonIgnore] public DateTime EditedTime => DateTimeOffset.FromUnixTimeSeconds(_editedAt).DateTime;

    }

}