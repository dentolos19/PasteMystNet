using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PasteMystNet
{

    public class PasteMystEditForm
    {

        [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)] private string _tags;

        [JsonProperty(PropertyName = "title")] public string Title { get; set; }
        [JsonProperty(PropertyName = "isPrivate")] public bool IsPrivate { get; set; }
        [JsonProperty(PropertyName = "isPublic")] public bool IsPublic { get; set; }
        [JsonProperty(PropertyName = "pasties")] public PasteMystPastyForm[] Pasties { get; set; }
        [JsonIgnore] internal string Id { get; set; }
        [JsonIgnore] public string[] Tags { get; set; }

        internal PasteMystEditForm(PasteMystPaste paste)
        {
            // TODO
        }

        public async Task<PasteMystPaste> PatchPasteAsync(PasteMystAuth auth)
        {
            return null; // TODO
        }

    }

}