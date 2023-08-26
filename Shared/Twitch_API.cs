using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using JAWSWebApp.Models;

namespace JAWSWebApp.Shared
{
    public class Twitch_API
    {
        private string OAuth_token = "";
        private readonly string Reflesh_token = "";
        private readonly string client_ID = "";
        private readonly string client_secret = "";

        private record Token(string access_token);

        public class TagsJson
        {
            [JsonPropertyName("tags")] public List<string>? Tags { get; set; }
        }

        public Twitch_API()
        {
        }

        // OAuthトークン取得
        public async Task<string> GetTwitchOAuthToken(HttpClient client)
        {
            HttpResponseMessage response = new();
            var parameters = new Dictionary<string, string>()
            {
                { "client_id", client_ID },
                { "client_secret", client_secret },
                { "redirect_uri", "http://localhost"},
                { "code", "xxxxxxxxxx" },
                { "grant_type", "authorization_code" },
            };
            var content = new FormUrlEncodedContent(parameters);

            var baseUri = new Uri("https://id.twitch.tv/oauth2/token");

            try
            {
                response = await client.PostAsync(baseUri, content);
            }
            catch (Exception ex)
            {
                response.ReasonPhrase = ex.Message;
            }

            if (response.IsSuccessStatusCode != true)
            {
                return "";
            }

            return await response.Content.ReadAsStringAsync();
        }

        // OAuthトークン更新
        public async Task<bool> RefreshTwitchOAuthToken(HttpClient client)
        {
            HttpResponseMessage response = new();
            var parameters = new Dictionary<string, string>()
            {
                { "client_id", client_ID },
                { "client_secret", client_secret },
                { "grant_type", "refresh_token" },
                { "refresh_token", Reflesh_token },
            };
            var content = new FormUrlEncodedContent(parameters);

            var baseUri = new Uri("https://id.twitch.tv/oauth2/token");

            try
            {
                response = await client.PostAsync(baseUri, content);
            }
            catch (Exception ex)
            {
                response.ReasonPhrase = ex.Message;
            }

            if (response.IsSuccessStatusCode != true)
            {
                return false;
            }

            // OAuthトークンを更新したら変数に入れるように変更 - リヒト
            // return await response.Content.ReadAsStringAsync();
            var result = await response.Content.ReadAsStringAsync();
            OAuthTokenResModel? responseOAuthToken = JsonSerializer.Deserialize<OAuthTokenResModel?>(result);
            if (responseOAuthToken != null && responseOAuthToken.AccessToken != null)
            {
                OAuth_token = responseOAuthToken.AccessToken;
                return true;
            }
            else
            {
                return false;
            }
        }

        // jaws_players のUserIDを取得する
        public async Task<string> GetUsers(HttpClient client)
        {
            if (await RefreshTwitchOAuthToken(client) == false)
            {
                return "";
            }

            HttpResponseMessage response = new();

            var baseUri = new Uri("https://api.twitch.tv/helix/users?login=jaws_players");
            var request = new HttpRequestMessage(HttpMethod.Get, baseUri);

            request.Headers.Add("Authorization", "Bearer " + OAuth_token);
            request.Headers.Add("Client-Id", client_ID);

            try
            {
                response = await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                response.ReasonPhrase = ex.Message;
            }

            var resultString = "";

            if (response.IsSuccessStatusCode == true)
            {
                //            OAuthResult = await response.Content.ReadFromJsonAsync<IEnumerable<OAuth>>();
                resultString = await response.Content.ReadAsStringAsync();
            }

            return resultString;
        }

        // 文字列引数からカテゴリーを取得する
        public async Task<string> SerchCategories(HttpClient client, string title)
        {
            if (await RefreshTwitchOAuthToken(client) == false)
            {
                return "";
            }

            HttpResponseMessage response = new();

            var baseUri = new Uri(@"https://api.twitch.tv/helix/search/categories?query=" + title);
            var request = new HttpRequestMessage(HttpMethod.Get, baseUri);

            request.Headers.Add("Authorization", "Bearer " + OAuth_token);
            request.Headers.Add("Client-Id", client_ID);

            try
            {
                response = await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                response.ReasonPhrase = ex.Message;
            }

            var resultString = "";

            if (response.IsSuccessStatusCode == true)
            {
                //            OAuthResult = await response.Content.ReadFromJsonAsync<IEnumerable<OAuth>>();
                resultString = await response.Content.ReadAsStringAsync();
            }

            return resultString;
        }

        // タグIDの内容が合っているか確認する
        public async Task<string> GetAllStreamTags(HttpClient client, string tag_id)
        {
            if (await RefreshTwitchOAuthToken(client) == false)
            {
                return "";
            }

            HttpResponseMessage response = new();

            var baseUri = new Uri(@"https://api.twitch.tv/helix/tags/streams?tag_id=" + tag_id);
            var request = new HttpRequestMessage(HttpMethod.Get, baseUri);

            request.Headers.Add("Authorization", "Bearer " + OAuth_token);
            request.Headers.Add("Client-Id", client_ID);

            try
            {
                response = await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                response.ReasonPhrase = ex.Message;
            }

            var resultString = "";

            if (response.IsSuccessStatusCode == true)
            {
                //            OAuthResult = await response.Content.ReadFromJsonAsync<IEnumerable<OAuth>>();
                resultString = await response.Content.ReadAsStringAsync();
            }

            return resultString;
        }

        // チャンネル情報の取得
        public async Task<string> GetChannelInformation(HttpClient client)
        {
            if (await RefreshTwitchOAuthToken(client) == false)
            {
                return "";
            }

            HttpResponseMessage response = new();

            var baseUri = new Uri("https://api.twitch.tv/helix/channels?broadcaster_id=");
            var request = new HttpRequestMessage(HttpMethod.Get, baseUri);

            request.Headers.Add("Authorization", "Bearer " + OAuth_token);
            request.Headers.Add("Client-Id", client_ID);

            try
            {
                response = await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                response.ReasonPhrase = ex.Message;
            }

            var resultString = "";

            if (response.IsSuccessStatusCode == true)
            {
                //            OAuthResult = await response.Content.ReadFromJsonAsync<IEnumerable<OAuth>>();
                resultString = await response.Content.ReadAsStringAsync();
            }

            return resultString;
        }

        // チャンネル情報の書き換え
        public async Task<HttpResponseMessage> ModifyChannelInformation(HttpClient client, Dictionary<string, string> parameters)
        {
            HttpResponseMessage response = new();

            if (await RefreshTwitchOAuthToken(client) == false)
            {
                return response;
            }

            var baseUri = new Uri("https://api.twitch.tv/helix/channels?broadcaster_id=");
            var request = new HttpRequestMessage(HttpMethod.Patch, baseUri);

            request.Headers.Add("Authorization", "Bearer " + OAuth_token);
            request.Headers.Add("Client-Id", client_ID);
            request.Content = new FormUrlEncodedContent(parameters);

            try
            {
                response = await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                response.ReasonPhrase = ex.Message;
            }

            return response;
        }

        // タグの書き換え
        public async Task<HttpResponseMessage> ReplaceStreamTags(HttpClient client, List<string> tags)
        {
            HttpResponseMessage response = new();

            if (await RefreshTwitchOAuthToken(client) == false)
            {
                return response;
            }

            var tagsJson = new List<TagsJson>
            {
                new TagsJson { Tags = tags }
            };

            var baseUri = new Uri("https://api.twitch.tv/helix/streams/tags?broadcaster_id=");
            var request = new HttpRequestMessage(HttpMethod.Put, baseUri);

            request.Headers.Add("Authorization", "Bearer " + OAuth_token);
            request.Headers.Add("Client-Id", client_ID);

            var jsonString = JsonSerializer.Serialize(tagsJson);
            jsonString = jsonString[1..^1];

            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            try
            {
                response = await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                response.ReasonPhrase = ex.Message;
            }

            return response;
        }

        public async Task<HttpResponseMessage> SetStreamTags(HttpClient client, List<string> tags)
        {
            HttpResponseMessage response = new();

            if (await RefreshTwitchOAuthToken(client) == false)
            {
                return response;
            }

            var tagsJson = new List<TagsJson>
            {
                new TagsJson { Tags = tags }
            };

            var baseUri = new Uri("https://api.twitch.tv/helix/channels?broadcaster_id=");
            var request = new HttpRequestMessage(HttpMethod.Patch, baseUri);

            request.Headers.Add("Authorization", "Bearer " + OAuth_token);
            request.Headers.Add("Client-Id", client_ID);

            var jsonString = JsonSerializer.Serialize(tagsJson);
            jsonString = jsonString[1..^1];

            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            try
            {
                response = await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                response.ReasonPhrase = ex.Message;
            }

            return response;
        }

        // おすすめチャンネルフォロー機能のチャンネル書き換え
        public async Task<HttpResponseMessage> SetFeaturedChannels(HttpClient client, string channels)
        {
            HttpResponseMessage response = new();

            var baseUri = new Uri("https://api.furious.pro/featuredchannels/bot/xxxx:xxxx/" + channels);
            var request = new HttpRequestMessage(HttpMethod.Post, baseUri);

            try
            {
                response = await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                response.ReasonPhrase = ex.Message;
            }

            return response;
        }
    }
}
