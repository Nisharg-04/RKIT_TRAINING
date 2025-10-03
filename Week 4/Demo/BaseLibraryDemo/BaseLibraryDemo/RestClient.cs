using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Threading.Tasks;
using RestSharp;

namespace RestSharpDemo
{
    internal class RestClientPractise
    {
        static async Task Main(string[] args)
        {
            
            RestClientOptions options = new RestClientOptions("https://jsonplaceholder.typicode.com/")
            {
                Timeout = TimeSpan.FromSeconds(20), 
                ThrowOnAnyError = false, // not throw exceptions automatically
            };

            RestClient client = new RestClient(options);

          //get
            RestRequest getRequest = new RestRequest("posts/1", Method.Get);
            RestResponse getResponse = await client.GetAsync(getRequest);

            Console.WriteLine(getResponse.StatusCode);
            Console.WriteLine(getResponse.Content);

            // get with query parameters
            RestRequest queryRequest = new RestRequest("posts", Method.Get);
            queryRequest.AddParameter("userId", 1);
            RestResponse queryResponse = await client.ExecuteAsync(queryRequest);
            Console.WriteLine(queryResponse.Content);

          
            // add header
            //headerRequest.AddHeader("Custom-Header", "HeaderValue");
         

            //post request with JSON body
            RestRequest postRequest = new RestRequest("posts", Method.Post);
            var postData = new { Title = "New Post", Body = "Post body", UserId = 1 };
            postRequest.AddJsonBody(postData);

            RestResponse postResponse = await client.PostAsync(postRequest);
            Console.WriteLine(postResponse.Content);


            // DELETE request
            RestRequest deleteRequest = new RestRequest("posts/1", Method.Delete);
            RestResponse deleteResponse = await client.DeleteAsync(deleteRequest);
            Console.WriteLine(deleteResponse.StatusCode);

            // 9. File upload (multipart form-data)
            // RestRequest fileRequest = new RestRequest("upload", Method.Post);
            // fileRequest.AddFile("fileField", "path/to/file.txt");
            // RestResponse fileResponse = await client.ExecuteAsync(fileRequest);
            // Console.WriteLine(fileResponse.Content);

            // 10. Custom ExecuteAsync 
            RestRequest customRequest = new RestRequest("posts/1", Method.Get);
            var data = await client.GetAsync<Post>(customRequest);
            Console.WriteLine(data.Title);
        }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
    }
}
