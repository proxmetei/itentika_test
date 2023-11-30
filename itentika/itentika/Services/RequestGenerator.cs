using itentika.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace itentika.Services
{
    public static class RequestGenerator
    {
        public static void SendRequest(Event myEvent)
        {
            var client = new HttpClient();
            var myContent = JsonSerializer.Serialize(myEvent);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("http://localhost:5075/api/Process", byteContent).Result;
        }
    }
}
