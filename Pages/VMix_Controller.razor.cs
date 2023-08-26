using System.Globalization;
using JAWSWebApp.Shared;

namespace JAWSWebApp.Pages
{
    public partial class VMix_Controller
    {
        static readonly HttpClient Http = new();

        public VMix_Controller()
        {
        }

        // 画面テキスト書き換え
        public string inputTextScene = "";
        private string selectedName = "";
        private string textValue = "";

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

        public string mainTitle = "";
        public string responseMainTitle = "　";

        public async void SetMainTitle()
        {
            selectedName = "MainTitle.Text";
            textValue = mainTitle;

            responseMainTitle = await SetText();
        }

        public string subTitle = "";
        public string responseSubTitle = "　";

        public async void SetSubTitle()
        {
            selectedName = "SubTitle.Text";
            textValue = subTitle;

            responseSubTitle = await SetText();
        }

        public string names = "";
        public string responseName = "　";

        public async void SetNames()
        {
            string[] destBox = { "", "", "", "", "", "", "", "" };
            string[] srcBox = names.Split(',');
            Array.Copy(srcBox, destBox, srcBox.Length);

            for (int i = 0; i < 8; i++)
            {
                selectedName = "Name" + i.ToString() + ".Text";
                textValue = destBox[i];
                responseName = await SetText();
                if (responseName != "OK")
                {
                    return;
                }
            }
        }

        public string estTime = "";
        public string responseEst = "　";

        public async Task SetEstTime()
        {
            selectedName = "EstTime.Text";
            textValue = estTime;

            responseEst = await SetText();
        }

        // メイン画面遷移
        public string inputPreviewScene = "";
        public string responsePreviewInput = "　";

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

        public bool isFadeDisabled = false;
        public bool isFadeConfirmDisabled = true;
        public string responseFade = "　";

        public void FadeClick()
        {
            isFadeDisabled = true;
            isFadeConfirmDisabled = false;
            responseFade = "　";
        }

        public async Task FadeTransition()
        {
            isFadeConfirmDisabled = true;

            var api = new HTTP_API();

            var parameters = new Dictionary<string, string>()
            {
                { "Function", "Fade" },
                { "Duration", "1000" },
            };

            var response = await api.PostHttpWebApi(Http, parameters);

            if (response.ReasonPhrase == "OK")
            {
                responseFade = "画面遷移完了";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responseFade = response.ReasonPhrase;
                }
            }

            isFadeDisabled = false;
        }

        public bool isCutDisabled = false;
        public bool isCutConfirmDisabled = true;
        public string responseCut = "　";


        public void CutClick()
        {
            isCutDisabled = true;
            isCutConfirmDisabled = false;
            responseCut = "　";
        }

        public async Task CutTransition()
        {
            isCutConfirmDisabled = true;

            var api = new HTTP_API();

            var parameters = new Dictionary<string, string>()
            {
                { "Function", "Cut" },
            };

            var response = await api.PostHttpWebApi(Http, parameters);

            if (response.ReasonPhrase == "OK")
            {
                responseCut = "画面遷移完了";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responseCut = response.ReasonPhrase;
                }
            }

            isCutDisabled = false;
        }

        public bool isMergeDisabled = false;
        public bool isMergeConfirmDisabled = true;
        public string responseMerge = "　";


        public void MergeClick()
        {
            isMergeDisabled = true;
            isMergeConfirmDisabled = false;
            responseMerge = "　";
        }

        public async Task MergeTransition()
        {
            isMergeConfirmDisabled = true;

            var api = new HTTP_API();

            var parameters = new Dictionary<string, string>()
            {
                { "Function", "Merge" },
                { "Duration", "500" },
            };

            var response = await api.PostHttpWebApi(Http, parameters);


            if (response.ReasonPhrase == "OK")
            {
                responseMerge = "画面遷移完了";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responseMerge = response.ReasonPhrase;
                }
            }

            isMergeDisabled = false;
        }

        public bool isStinger1Disabled = false;
        public bool isStinger1ConfirmDisabled = true;
        public string responseStinger1 = "　";

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

        public bool isStinger2Disabled = false;
        public bool isStinger2ConfirmDisabled = true;
        public string responseStinger2 = "　";

        public void Stinger2Click()
        {
            isStinger2Disabled = true;
            isStinger2ConfirmDisabled = false;
            responseStinger2 = "　";
        }

        public async Task Stinger2Transition()
        {
            isStinger2ConfirmDisabled = true;

            var api = new HTTP_API();

            var parameters = new Dictionary<string, string>()
            {
                { "Function", "Stinger2" },
            };

            var response = await api.PostHttpWebApi(Http, parameters);

            if (response.ReasonPhrase == "OK")
            {
                responseStinger2 = "画面遷移完了";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responseStinger2 = response.ReasonPhrase;
                }
            }

            isStinger2Disabled = false;
        }

        // タイマー処理
        public bool isStartDisabled = false;
        public bool isPauseDisabled = false;
        public bool isStopDisabled = false;

        public string responseTimer = "　";
        public string responseTimerChange = "　";
        public string responseTimerReset = "　";

        public async Task TimerStart()
        {
            isStartDisabled = true;
            isPauseDisabled = false;
            isStopDisabled = false;

            var api = new HTTP_API();

            var param_start = new Dictionary<string, string>()
            {
                { "Function", "StartCountdown" },
                { "Input", "タイマー" },
                { "SelectedName", "Timer.Text" },
            };

            var res_start = await api.PostHttpWebApi(Http, param_start);

            if (res_start.ReasonPhrase != "OK")
            {
                if (res_start.ReasonPhrase != null)
                {
                    responseTimer = res_start.ReasonPhrase;
                }
                return;
            }

            // タイマーの色を白色にする（色は要調整）
            var param_color = new Dictionary<string, string>()
            {
                { "Function", "SetTextColour" },
                { "Input", "タイマー" },
                { "SelectedName", "Timer.Text" },
                { "Value", "#FFFFFF" },
            };

            var res_color = await api.PostHttpWebApi(Http, param_color);

            if (res_color.ReasonPhrase == "OK")
            {
                responseTimerChange = "　";
                responseTimerReset = "　";
                responseTimer = "タイマーをスタートしました！";
            }
            else
            {
                if (res_color.ReasonPhrase != null)
                {
                    responseTimer = res_color.ReasonPhrase;
                }
            }
        }

        public async Task TimerPause()
        {
            isStartDisabled = false;
            isStopDisabled = false;
            isPauseDisabled = true;

            var api = new HTTP_API();

            // タイマーを一時停止にする
            var param_pause = new Dictionary<string, string>()
            {
                { "Function", "PauseCountdown" },
                { "Input", "タイマー" },
                { "SelectedName", "Timer.Text" },
            };

            var res_pause = await api.PostHttpWebApi(Http, param_pause);

            if (res_pause.ReasonPhrase != "OK")
            {
                if (res_pause.ReasonPhrase != null)
                {
                    responseTimer = res_pause.ReasonPhrase;
                }
                return;
            }

            // タイマーの色を灰色にする（色は要調整）
            var param_color = new Dictionary<string, string>()
            {
                { "Function", "SetTextColour" },
                { "Input", "タイマー" },
                { "SelectedName", "Timer.Text" },
                { "Value", "#808080" },
            };

            var res_color = await api.PostHttpWebApi(Http, param_color);

            if (res_color.ReasonPhrase == "OK")
            {
                responseTimerChange = "　";
                responseTimerReset = "　";
                responseTimer = "タイマーを一時停止しました！";
            }
            else
            {
                if (res_color.ReasonPhrase != null)
                {
                    responseTimer = res_color.ReasonPhrase;
                }
            }
        }

        public async Task TimerStop()
        {
            isStartDisabled = false;
            isPauseDisabled = false;
            isStopDisabled = true;

            var api = new HTTP_API();

            // タイマーを一時停止にする
            var param_pause = new Dictionary<string, string>()
            {
                { "Function", "PauseCountdown" },
                { "Input", "タイマー" },
                { "SelectedName", "Timer.Text" },
            };

            var res_pause = await api.PostHttpWebApi(Http, param_pause);

            if (res_pause.ReasonPhrase != "OK")
            {
                if (res_pause.ReasonPhrase != null)
                {
                    responseTimer = res_pause.ReasonPhrase;
                }
                return;
            }

            // タイマーの色を黄色にする（色は要調整）
            var param_color = new Dictionary<string, string>()
            {
                { "Function", "SetTextColour" },
                { "Input", "タイマー" },
                { "SelectedName", "Timer.Text" },
                { "Value", "#FFFF00" },
            };

            var res_color = await api.PostHttpWebApi(Http, param_color);

            if (res_color.ReasonPhrase == "OK")
            {
                responseTimerChange = "　";
                responseTimerReset = "　";
                responseTimer = "タイマーをストップしました！";
            }
            else
            {
                if (res_color.ReasonPhrase != null)
                {
                    responseTimer = res_color.ReasonPhrase;
                }
            }
        }

        // タイマー修正の確認を厳格に
        public bool isChangeDisabled = false;
        public bool isChangeConfirmDisabled = true;
        public bool isChangeExecuteDisabled = true;
        public string timerChange = "";

        public void ChangeClick()
        {
            responseTimerChange = "　";
            isChangeDisabled = true;
            isChangeConfirmDisabled = false;
            isChangeExecuteDisabled = true;
        }

        public void TimerChangeConfirm()
        {
            isChangeDisabled = true;
            isChangeConfirmDisabled = true;
            isChangeExecuteDisabled = false;
        }

        public async Task TimerChangeExecute()
        {
            // 入力された文字列が H:mm:ss になっているかどうかチェック
            CultureInfo ci = CultureInfo.CurrentCulture;
            DateTimeStyles dts = DateTimeStyles.None;
            DateTime dt;
            if (DateTime.TryParseExact(timerChange, "H:mm:ss", ci, dts, out dt) == false)
            {
                responseTimerChange = "正しい時間を入力してください";
                return;
            }

            isChangeExecuteDisabled = true;

            var api = new HTTP_API();

            // タイマーを修正
            var parameters = new Dictionary<string, string>()
            {
                { "Function", "ChangeCountdown" },
                { "Input", "タイマー" },
                { "SelectedName", "Timer.Text" },
                { "Value", timerChange },
            };

            var response = await api.PostHttpWebApi(Http, parameters);

            if (response.ReasonPhrase == "OK")
            {
                responseTimer = "　";
                responseTimerReset = "　";
                responseTimerChange = "タイマーを修正しました！";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responseTimerChange = response.ReasonPhrase;
                }
            }

            isChangeDisabled = false;
        }

        // タイマーリセットの確認を厳格に
        public bool isResetDisabled = false;
        public bool isResetConfirmDisabled = true;
        public bool isResetExecuteDisabled = true;

        public void ResetClick()
        {
            responseTimerReset = "　";
            isResetDisabled = true;
            isResetConfirmDisabled = false;
            isResetExecuteDisabled = true;
        }

        public void TimerResetConfirm()
        {
            isResetDisabled = true;
            isResetConfirmDisabled = true;
            isResetExecuteDisabled = false;
        }

        public async Task TimerResetExecute()
        {
            isStartDisabled = false;
            isPauseDisabled = false;
            isStopDisabled = false;

            isResetExecuteDisabled = true;

            var api = new HTTP_API();

            // タイマーをリセットする
            var param_reset = new Dictionary<string, string>()
            {
                { "Function", "StopCountdown" },
                { "Input", "タイマー" },
                { "SelectedName", "Timer.Text" },
            };

            var res_reset = await api.PostHttpWebApi(Http, param_reset);

            if (res_reset.ReasonPhrase != "OK")
            {
                if (res_reset.ReasonPhrase != null)
                {
                    responseTimerReset = res_reset.ReasonPhrase;
                }
                return;
            }

            // タイマーの色を白色にする（色は要調整）
            var param_color = new Dictionary<string, string>()
        {
            { "Function", "SetTextColour" },
                { "Input", "タイマー" },
            { "SelectedName", "Timer.Text" },
            { "Value", "#FFFFFF" },
        };

            var res_color = await api.PostHttpWebApi(Http, param_color);

            if (res_color.ReasonPhrase == "OK")
            {
                responseTimer = "　";
                responseTimerChange = "　";
                responseTimerReset = "タイマーをリセットしました！";
            }
            else
            {
                if (res_color.ReasonPhrase != null)
                {
                    responseTimerReset = res_color.ReasonPhrase;
                }
            }

            isResetDisabled = false;
        }

    }
}
