using System;
using Newtonsoft.Json;

namespace PasteMystNet
{

    public class PasteMystEdit
    {

        [JsonProperty(PropertyName = "editType")] private readonly string _editType;
        [JsonProperty(PropertyName = "editedAt")] private readonly long _editedAt;

        [JsonProperty(PropertyName = "_id")] public string Id { get; private set; }
        [JsonProperty(PropertyName = "editId")] public string EditId { get; private set; }
        [JsonProperty(PropertyName = "metadata")] public string[] Metadata { get; private set; }
        [JsonProperty(PropertyName = "edit")] public string Edit { get; private set; }
        [JsonIgnore] public PasteMystEditType EditType => (PasteMystEditType)Enum.Parse(typeof(PasteMystEditType), _editType);
        [JsonIgnore] public DateTime EditedTime => DateTimeOffset.FromUnixTimeSeconds(_editedAt).DateTime;

    }

}