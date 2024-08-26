using System.Net.Http;
using System.Net.Http.Headers;

namespace MyMvcProject.Helpers
{
    public class ApiClient
    {
        private readonly HttpClient _client;

        public ApiClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("~/api/"); // or Url.Content("~/api/")
        }

        public async Task CreateShortcut(byte[] documentBytes)
        {
            ByteArrayContent content = new ByteArrayContent(documentBytes);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            HttpResponseMessage response = await _client.PostAsync("shortcut", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Shortcut created successfully");
            }
            else
            {
                Console.WriteLine("Error creating shortcut: " + response.StatusCode);
            }
        }
    }
}