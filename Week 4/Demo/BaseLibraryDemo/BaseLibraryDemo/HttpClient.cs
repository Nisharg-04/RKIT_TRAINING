using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Json;
using System.Web;

namespace HttpClientDemo
{
    internal class HttpClientDemo
    {
        static async Task Main(string[] args)
        {
            // Constructors
            HttpClient httpClient1 = new HttpClient(); // Default
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient httpClient2 = new HttpClient(handler); // With handler
            HttpClient httpClient3 = new HttpClient(handler, disposeHandler: true); // With dispose option

            // Properties
            string url = "https://jsonplaceholder.typicode.com/posts/1";
            httpClient1.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            Console.WriteLine(httpClient1.BaseAddress); // BaseAddress

            Console.WriteLine(httpClient1.DefaultRequestVersion); // DefaultRequestVersion
            Console.WriteLine(httpClient1.Timeout); // Timeout
            Console.WriteLine(httpClient1.MaxResponseContentBufferSize); // Max buffer size in byted

            //  Cancel all requests
            httpClient1.CancelPendingRequests();

            // GET request
            HttpResponseMessage response = await httpClient1.GetAsync("posts/1");
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.IsSuccessStatusCode);
            Console.WriteLine(response.Version);

            string strResponse = await httpClient1.GetStringAsync("posts/1");
            Console.WriteLine(strResponse);

            byte[] byteResponse = await httpClient1.GetByteArrayAsync("posts/1");
            Console.WriteLine("Bytes length: " + byteResponse.Length);

            Stream streamResponse = await httpClient1.GetStreamAsync("posts/1");
            Console.WriteLine("Stream can read: " + streamResponse.CanRead);

            // POST request
            string urlPost = "https://jsonplaceholder.typicode.com/posts";
            var postData = new { Title = "Demo", Body = "Learning HttpClient", UserId = 1 };
            string json = System.Text.Json.JsonSerializer.Serialize(postData);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            response = await httpClient1.PostAsync(urlPost, content);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // PostAsJsonAsync
            response = await httpClient1.PostAsJsonAsync(urlPost, postData);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            //  DELETE request
            response = await httpClient1.DeleteAsync("posts/1");
            Console.WriteLine(response.StatusCode);

            // SendAsync (custom request)
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "posts/1");
            request.Headers.Add("Custom-Header", "MyValue");
            response = await httpClient1.SendAsync(request);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Multipart request (file upload demo)
            // MultipartFormDataContent multiPartContent = new MultipartFormDataContent();
            // FileStream file = new FileStream("path/to/file.txt", FileMode.Open, FileAccess.Read);
            // multiPartContent.Add(new StreamContent(file), "fileField", "file.txt");
            // response = await httpClient1.PostAsync(urlPost, multiPartContent);

            // Disposing
            httpClient1.Dispose();
            httpClient2.Dispose();
            httpClient3.Dispose();
        }
    }
}

