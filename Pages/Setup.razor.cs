using JAWSWebApp.Shared;
using System.Text.Json;
using Fluxor;
using Microsoft.AspNetCore.Components;
using JAWSWebApp.Models;
using System.Text;
using JAWSWebApp.Store;

namespace JAWSWebApp.Pages
{
    public partial class Setup
    {
        [Inject]
        private IState<SheetDataState> SheetDataState { get; set; }
        public Setup()
        {
            
        }

        private bool isLoading = false;
        
        private string setupStartTime = "";
        private string startTime = "";
        private string inputGameTitle = "";
        private string inputGameID = "";
        private string featuredChannels = "";
        private string inputTags = "";
        private string inputPreviewScene = "";
        private string castNames = "";
        // 画面テキスト書き換え
        // todo シノペ 変数名があいまいなのでリファクタリングする
        public string inputTextScene = "";
        public string imageButton = "";
        private string selectedName = "";
        private string textValue = "";
        private string mainTitle = "";
        private string subTitle = "";
        private string names = "";
        private string estTime = "";
        private bool isStinger1Disabled = false;
        private bool isStinger1ConfirmDisabled = true;

        private string toastMessage = "";
        private string responseSetChannelInformation = "";
        private string responseSetFeaturedChannels = "";
        private string responseSetTags = "";
        private string responsePreviewInput = "";
        private string responseChangeDisplayText = "";
        private string responseStinger1 = "";

        private bool isLayerOk = false;
        private bool isSrtOk = false;
        private bool isVoiceOk = false;


        static readonly HttpClient Http = new();
        [Parameter]
        public string? pageNum { get; set; }
        private int rowIndex;
        private string nextpage;
        private string prevpage;

        [Inject] public IDispatcher Dispatcher { get; set; }
        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            var client = new HttpClient();
            var result = await client.GetAsync(@"https://xxxxxx.com/getSheet");

            var resultStr = await result.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<SheetDataModel>>(resultStr);
            var action = new GetSheetDataAction(data);
            Dispatcher.Dispatch(action);

            await InvokeAsync(StateHasChanged);

            int.TryParse(pageNum, out var num);
            rowIndex = num > 0 ? num - 1 : 0;
            nextpage = $"/onair/{num}";
            prevpage = $"/onair/{num - 1}";

            if (SheetDataState?.Value?.SheetData.Count > 0)
            {
                // 演目の開始日時をDateTimeに変換する
                DateTime.TryParse((SheetDataState?.Value?.SheetData[rowIndex].Date + " " + SheetDataState?.Value?.SheetData[rowIndex].StartTime), out DateTime dtStart);
                // 出演者の名前にさん付けする
                string[] casts = SheetDataState?.Value?.SheetData[rowIndex]?.Casts.Split(",");
                string[] castsWithHonorific = casts.Select(x => x + "さん").ToArray();

                setupStartTime = dtStart.AddMinutes(-10).ToString("HH:mm");
                startTime = dtStart.ToString("MM/dd HH:mm");
                inputGameTitle = SheetDataState?.Value?.SheetData[rowIndex].Title ?? "";
                inputGameID = SheetDataState?.Value?.SheetData[rowIndex]?.CategoryId ?? "";
                featuredChannels = SheetDataState?.Value?.SheetData[rowIndex]?.FeaturedChannels ?? "";
                inputTags = SheetDataState?.Value?.SheetData[rowIndex]?.Tags ?? "";
                inputPreviewScene = SheetDataState?.Value?.SheetData[rowIndex]?.SceneName ?? "";
                inputTextScene = SheetDataState?.Value?.SheetData[rowIndex]?.TextSceneName ?? "";
                imageButton = $"images/{inputPreviewScene.Replace("/", "").Replace("\"", "").Replace("%", "")}.png";
                castNames = string.Join("、", castsWithHonorific);
                mainTitle = SheetDataState?.Value?.SheetData[rowIndex]?.Title ?? "";
                subTitle = SheetDataState?.Value?.SheetData[rowIndex]?.SubTitle ?? "";
                names = SheetDataState?.Value?.SheetData[rowIndex]?.Casts ?? "";
                estTime = SheetDataState?.Value?.SheetData[rowIndex]?.Est ?? "";
                isLayerOk = Convert.ToBoolean(SheetDataState?.Value?.SheetData[rowIndex]?.IsLayerOk.ToLower());
                isSrtOk = Convert.ToBoolean(SheetDataState?.Value?.SheetData[rowIndex]?.IsSrtOk.ToLower());
                isVoiceOk = Convert.ToBoolean(SheetDataState?.Value?.SheetData[rowIndex]?.IsVoiceOk.ToLower());
            }

            isLoading = false;

            base.OnInitialized();
        }

        private async Task updateCell(string column, string rowNo, string value)
        {
            var client = new HttpClient();
            var param = new Dictionary<string, string>
            {
                { "column", column },
                { "rowIndex", rowNo },
                { "value", value }
            };
            var content = new StringContent(JsonSerializer.Serialize(param), Encoding.UTF8, "application/json");

            await client.PostAsync(@"https://xxxxxx.com/updateCell", content);
        }

        private async Task RefreshOAhtuToken()
        {
            var twitch_api = new Twitch_API();

            if (await twitch_api.RefreshTwitchOAuthToken(Http) == true)
            {
                toastMessage = "トークン更新完了";
            }
            else
            {
                toastMessage = "更新失敗";
            }
        }

        private async Task SetChannelInformation()
        {
            await RefreshOAhtuToken();

            // 情報が両方空でボタンが押されたら何もしない
            if (inputGameTitle == "" && inputGameID == "")
            {
                return;
            }

            var twitch_api = new Twitch_API();

            var parameters = new Dictionary<string, string>();

            if (inputGameTitle != "")
            {
                parameters.Add("title", "JAWS PLAYERS 五月鮫 - " + inputGameTitle);

            }
            if (inputGameID != "")
            {
                parameters.Add("game_id", inputGameID);

            }

            var response = await twitch_api.ModifyChannelInformation(Http, parameters);

            if (response.ReasonPhrase == "No Content")
            {
                responseSetChannelInformation = "セット完了";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responseSetChannelInformation = response.ReasonPhrase;
                }
            }
        }

        private async Task SetFeaturedChannels()
        {
            responseSetFeaturedChannels = "";

            var twitch_api = new Twitch_API();

            var response = await twitch_api.SetFeaturedChannels(Http, featuredChannels);

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

        public async Task SetPreviewInput()
        {
            var api = new HTTP_API();

            var parameters = new Dictionary<string, string>()
            {
                { "Function", "PreviewInput" },
                { "Input", inputPreviewScene },
            };

            var response = await api.PostHttpWebApi(Http, parameters);

            if (response.ReasonPhrase == "OK")
            {
                responsePreviewInput = "セット完了";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responsePreviewInput = response.ReasonPhrase;
                }
            }
        }

        public async Task<string> SetMainTitle()
        {
            selectedName = "MainTitle.Text";
            textValue = mainTitle;

            var resultMessage = await SetText();
            return resultMessage;
        }

        public async Task<string> SetSubTitle()
        {
            selectedName = "SubTitle.Text";
            textValue = subTitle;

            var resultMessage = await SetText();
            return resultMessage;
        }

        public async Task<string> SetNames()
        {
            string[] destBox = { " ", " ", " ", " ", " ", " ", " ", " " };
            string[] srcBox = names.Split(',');
            string resultMessage = "";
            Array.Copy(srcBox, destBox, srcBox.Length);

            for (int i = 0; i < 8; i++)
            {
                selectedName = "Name" + i.ToString() + ".Text";
                textValue = destBox[i];
                resultMessage = await SetText();
                if (resultMessage != "OK")
                {
                    return resultMessage;
                }
            }
            return resultMessage;
        }

        public async Task<string> SetEstTime()
        {
            selectedName = "EstTime.Text";
            textValue = estTime;

            var resultMessage = await SetText();
            return resultMessage;
        }

        private async Task<string> SetText()
        {
            var api = new HTTP_API();

            var parameters = new Dictionary<string, string>()
            {
                { "Function", "SetText" },
                { "Input", inputTextScene },
                { "SelectedName", selectedName },
                { "Value", textValue },
            };

            var response = await api.PostHttpWebApi(Http, parameters);

            if (response.ReasonPhrase != null)
            {
                return response.ReasonPhrase;
            }
            else
            {
                return "null";
            }
        }

        private async void ChangeDisplayText()
        {
            var setMainTitle = SetMainTitle();
            var setSubTitle = SetSubTitle();
            var setEstTime = SetEstTime();
            var setNames = SetNames();

            string[] results = await Task.WhenAll(setMainTitle, setSubTitle, setEstTime, setNames);

            if (results.Any(result => result != "OK")) {
                responseChangeDisplayText = "セット失敗";
            }
            else
            {
                responseChangeDisplayText = "セット完了";
            }
        }

        

        public void Stinger1Click()
        {
            isStinger1Disabled = true;
            isStinger1ConfirmDisabled = false;
            responseStinger1 = "　";
        }

        public async Task Stinger1Transition()
        {
            isStinger1ConfirmDisabled = true;

            var api = new HTTP_API();

            var parameters = new Dictionary<string, string>()
            {
                { "Function", "Stinger1" },
            };

            var response = await api.PostHttpWebApi(Http, parameters);

            if (response.ReasonPhrase == "OK")
            {
                responseStinger1 = "画面遷移完了";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responseStinger1 = response.ReasonPhrase;
                }
            }

            isStinger1Disabled = false;
        }
    }
}
