using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using PasteMystNet.Internals;

namespace PasteMystNet
{
    
    public static class PasteMystService
    {

        public static PasteMystInfo Post(PasteMystForm form)
        {
            var formJson = new PasteMystFormJson
            {
                Code = Uri.EscapeDataString(form.Code),
                Expiration = form.Expiration.GetStringRepresentation(),
                Language = form.Language.GetStringRepresentation()
            };
            var infoJson = PostJson(formJson);
            var info = new PasteMystInfo
            {
                Id = infoJson.Id,
                Date = DateTimeOffset.FromUnixTimeSeconds(infoJson.Date).DateTime,
                Code = Uri.UnescapeDataString(infoJson.Code),
                Expiration = StringRepresentationExtensions.StringToExpiration(infoJson.Expiration),
                Language = StringRepresentationExtensions.StringToLanguage(infoJson.Language)
            };
            return info;
        }

        private static PasteMystInfoJson PostJson(PasteMystFormJson form)
        {
            var json = JsonConvert.SerializeObject(form);
            var request = WebRequest.Create(PasteMystConstants.PmPostEndpoint);
            request.ContentType = "application/json";
            request.Method = "POST";
            var writer = new StreamWriter(request.GetRequestStream());
            writer.Write(json);
            writer.Close();
            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream()!);
            var data = reader.ReadToEnd();
            reader.Close();
            return JsonConvert.DeserializeObject<PasteMystInfoJson>(data);
        }

        public static PasteMystInfo Get(string id)
        {
            var infoJson = GetJson(id);
            var info = new PasteMystInfo
            {
                Id = infoJson.Id,
                Date = DateTimeOffset.FromUnixTimeSeconds(infoJson.Date).DateTime,
                Code = Uri.UnescapeDataString(infoJson.Code),
                Expiration = StringRepresentationExtensions.StringToExpiration(infoJson.Expiration),
                Language = StringRepresentationExtensions.StringToLanguage(infoJson.Language)
            };
            return info;
        }

        private static PasteMystInfoJson GetJson(string id)
        {
            using var client = new WebClient();
            var data = client.DownloadString(PasteMystConstants.PmGetEndpoint + id);
            return JsonConvert.DeserializeObject<PasteMystInfoJson>(data);
        }

    }
    
}