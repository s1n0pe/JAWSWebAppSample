namespace JAWSWebApp.Shared
{
    public class HTTP_API
    {

        public HTTP_API()
        {
        }

        public async Task<HttpResponseMessage> PostHttpWebApi(HttpClient client, Dictionary<string, string> parameters)
        {
            HttpResponseMessage response = new();
            var content = new FormUrlEncodedContent(parameters);

            var baseUri = new Uri("");

            try
            {
                response = await client.PostAsync(baseUri, content);
            }
            catch (Exception ex)
            {
                response.ReasonPhrase = ex.Message;
            }

            return response;
        }
    }
}
