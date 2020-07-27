using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PasteMystNet.Internals;

namespace PasteMystNet
{

    public static class PasteMystService
    {

        public static PasteMystInfo Post(PasteMystForm form)
        {
            if (string.IsNullOrEmpty(form.Code))
                throw new Exception("The input form code content is empty!");
            var json = PasteMystFormJson.ToJson(form);
            var info = PostJson(json);
            return PasteMystInfo.FromJson(info);
        }

        public static async Task<PasteMystInfo> PostAsync(PasteMystForm form)
        {
            if (string.IsNullOrEmpty(form.Code))
                throw new Exception("The input form code content is empty!");
            var json = PasteMystFormJson.ToJson(form);
            var info = await PostJsonAsync(json).ConfigureAwait(false);
            return PasteMystInfo.FromJson(info);
        }

        private static PasteMystInfoJson PostJson(PasteMystFormJson form)
        {
            var json = form.ToString();
            var request = WebRequest.Create(PasteMystConstants.PmPostEndpoint);
            request.ContentType = "application/json";
            request.Method = "POST";
            var writer = new StreamWriter(request.GetRequestStream());
            writer.Write(json);
            writer.Close();
            var response = (HttpWebResponse)request.GetResponse();
            var stream = response.GetResponseStream();
            if (stream == null)
                return null;
            var reader = new StreamReader(stream);
            var data = reader.ReadToEnd();
            reader.Close();
            return JsonSerializer.Deserialize<PasteMystInfoJson>(data);
        }

        private static async Task<PasteMystInfoJson> PostJsonAsync(PasteMystFormJson form)
        {
            var client = new HttpClient();
            var content = new StringContent(form.ToString(), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(PasteMystConstants.PmPostEndpoint, content);
            client.Dispose();
            var responseContent = response.Content.ReadAsStringAsync().Result;
            return PasteMystInfoJson.FromJson(responseContent);
        }

        public static PasteMystInfo Get(string id)
        {
            var json = GetJson(id);
            return PasteMystInfo.FromJson(json);
        }

        public static async Task<PasteMystInfo> GetAsync(string id)
        {
            var json = await GetJsonAsync(id).ConfigureAwait(false);
            return PasteMystInfo.FromJson(json);
        }

        private static PasteMystInfoJson GetJson(string id)
        {
            var client = new WebClient();
            var data = client.DownloadString(PasteMystConstants.PmGetEndpoint + id);
            client.Dispose();
            return PasteMystInfoJson.FromJson(data);
        }

        private static async Task<PasteMystInfoJson> GetJsonAsync(string id)
        {
            var client = new HttpClient();
            var data = await client.GetStringAsync(PasteMystConstants.PmGetEndpoint + id);
            client.Dispose();
            return PasteMystInfoJson.FromJson(data);
        }

    }

}