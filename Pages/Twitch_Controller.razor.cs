using JAWSWebApp.Shared;

namespace JAWSWebApp.Pages
{
    public partial class Twitch_Controller
    {
        static readonly HttpClient Http = new();

        public Twitch_Controller()
        {

        }

        private string userID = "";
        private string inputGameTitle = "";
        private string outputGameID = "";
        private string OAuthToken = "";
        private string refreshToken = "";
        private string channelInformation = "";
        private string inputTitle = "";
        private string inputGameID = "";
        private string inputTags = "";
        private string inputTwitchIDs = "";

        private string responseChannelInformation = "　";
        private string responseSetTags = "　";
        private string responseSetFeaturedChannels = "　";

        private async Task GetUserID()
        {
            var twitch_api = new Twitch_API();

            userID = await twitch_api.GetUsers(Http);
        }

        private async Task GetGameID()
        {
            if (inputGameTitle == null)
            {
                return;
            }

            var twitch_api = new Twitch_API();

            outputGameID = await twitch_api.SerchCategories(Http, inputGameTitle);
        }

        private async Task GetOAhtuToken()
        {
            var twitch_api = new Twitch_API();

            OAuthToken = "";

            OAuthToken = await twitch_api.GetTwitchOAuthToken(Http);
        }

        private async Task RefreshOAhtuToken()
        {
            var twitch_api = new Twitch_API();

            refreshToken = "";

            if (await twitch_api.RefreshTwitchOAuthToken(Http) == true)
            {
                refreshToken = "OAuthトークンを更新しました";
            }
            else
            {
                refreshToken = "更新失敗";
            }
        }

        private async Task GetChannelInformation()
        {
            var twitch_api = new Twitch_API();

            channelInformation = "";

            channelInformation = await twitch_api.GetChannelInformation(Http);
        }

        private async Task SetChannelInformation()
        {
            responseChannelInformation = "　";

            // 情報が両方空でボタンが押されたら何もしない
            if (inputTitle == "" && inputGameID == "")
            {
                return;
            }

            var twitch_api = new Twitch_API();

            var parameters = new Dictionary<string, string>();

            if (inputTitle != "")
            {
                parameters.Add("title", "JAWS PLAYERS 五月鮫 - " + inputTitle);

            }
            if (inputGameID != "")
            {
                parameters.Add("game_id", inputGameID);

            }

            var response = await twitch_api.ModifyChannelInformation(Http, parameters);

            if (response.ReasonPhrase == "No Content")
            {
                responseChannelInformation = "セット完了";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responseChannelInformation = response.ReasonPhrase;
                }
            }
        }

        private async Task SetTags()
        {
            responseSetTags = "";

            // 空でボタンが押されたら何もしない
            if (inputTags == "")
            {
                return;
            }

            var twitch_api = new Twitch_API();

            List<string> tags = new(inputTags.Split(','));

            var response = await twitch_api.SetStreamTags(Http, tags);

            if (response.ReasonPhrase == "No Content")
            {
                responseSetTags = "セット完了";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responseSetTags = response.ReasonPhrase;
                }
            }
        }

        private async Task SetFeaturedChannels()
        {
            responseSetFeaturedChannels = "　";

            var twitch_api = new Twitch_API();

            var response = await twitch_api.SetFeaturedChannels(Http, inputTwitchIDs);

            if (response.ReasonPhrase == "OK")
            {
                responseSetFeaturedChannels = "セット完了";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responseSetFeaturedChannels = response.ReasonPhrase;
                }
            }
        }

    }
}
