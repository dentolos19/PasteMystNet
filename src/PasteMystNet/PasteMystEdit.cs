using Newtonsoft.Json;

namespace PasteMystNet;

public class PasteMystEdit
{
    [JsonProperty("editedAt")] private readonly long _editedAt;
    [JsonProperty("editType")] private readonly string _editType;

    [JsonProperty("_id")] public string Id { get; private set; }
    [JsonProperty("editId")] public string EditId { get; private set; }
    [JsonProperty("metadata")] public string[] Metadata { get; private set; }
    [JsonProperty("edit")] public string Edit { get; private set; }
    [JsonIgnore] public PasteMystEditType EditType => (PasteMystEditType)Enum.Parse(typeof(PasteMystEditType), _editType);
    [JsonIgnore] public DateTime EditedTime => DateTimeOffset.FromUnixTimeSeconds(_editedAt).DateTime;
}