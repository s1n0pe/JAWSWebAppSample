using Fluxor;
using JAWSWebApp.Shared;
using JAWSWebApp.Store;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace JAWSWebApp.Pages
{
    public partial class OnAir
    {
        [Inject]
        private IState<SheetDataState> SheetDataState { get; set; }

        [Inject]
        protected NavigationManager NavManager { get; set; }

        [Parameter]
        public string? pageNum { get; set; }

        public int rowIndex;
        public string nextpage;
        public string prevpage;
        public int nextTitleRowNo;
        private string column = "is_done";
        private string rowNo = "";
        private string value = "true";
        public string inputTimer = "";

        static readonly HttpClient Http = new();

        // タイマー処理
        public bool isStartDisabled = false;
        public bool isPauseDisabled = false;
        public bool isStopDisabled = false;

        // タイマー修正の確認を厳格に
        public bool isChangeDisabled = false;
        public bool isChangeConfirmDisabled = true;
        public bool isChangeExecuteDisabled = true;

        public string responseTimer = "";
        public string responseTimerChange = "";
        public string responseTimerReset = "";

        // プレビュー遷移
        // todo シノペ 実際の待機画面のシーン名を入力する
        public string inputPreviewScene = "00セットアップ";
        public string responsePreviewInput = "";

        public string responseTitleList = "";

        public bool isStinger1Disabled = false;
        public bool isStinger1ConfirmDisabled = true;
        public string responseStinger1 = "";

        protected override void OnInitialized()
        {
            int.TryParse(pageNum, out var num);
            rowIndex = num > 0 ? num - 1 : 0;
            rowNo = num > 0 ? (num + 1).ToString() : "1";
            nextpage = $"/setup/{num + 1}";
            prevpage = $"/setup/{num}";
            nextTitleRowNo = num + 1;
            if (SheetDataState?.Value?.SheetData.Count <= 0)
            {
                NavManager.NavigateTo(prevpage);
            }
            else
            {
                inputTimer = SheetDataState?.Value?.SheetData[rowIndex]?.Est ?? "";
            }
            base.OnInitialized();
        }
        public OnAir()
        {

        }
        private async Task updateCell()
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

            // タイマーの色を変更する（色は背景に合わせて要調整）
            var param_color = new Dictionary<string, string>()
            {
                { "Function", "SetTextColour" },
                { "Input", "タイマー" },
                { "SelectedName", "Timer.Text" },
                { "Value", "#000000" },
            };

            var res_color = await api.PostHttpWebApi(Http, param_color);

            if (res_color.ReasonPhrase == "OK")
            {
                responseTimerChange = "";
                responseTimerReset = "";
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
                responseTimerChange = "";
                responseTimerReset = "";
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

            // タイマーの色を変更する（色は要調整）
            var param_color = new Dictionary<string, string>()
            {
                { "Function", "SetTextColour" },
                { "Input", "タイマー" },
                { "SelectedName", "Timer.Text" },
                { "Value", "#FF8000" },
            };

            var res_color = await api.PostHttpWebApi(Http, param_color);

            if (res_color.ReasonPhrase == "OK")
            {
                responseTimerChange = "";
                responseTimerReset = "";
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

        public void ChangeClick()
        {
            responseTimerChange = "";
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
            if (DateTime.TryParseExact(inputTimer, "H:mm:ss", ci, dts, out dt) == false)
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
                { "Value", inputTimer },
            };

            var response = await api.PostHttpWebApi(Http, parameters);

            if (response.ReasonPhrase == "OK")
            {
                responseTimer = "";
                responseTimerReset = "";
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
            responseTimerReset = "";
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
            { "Value", "#000000" },
        };

            var res_color = await api.PostHttpWebApi(Http, param_color);

            if (res_color.ReasonPhrase == "OK")
            {
                responseTimer = "";
                responseTimerChange = "";
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

        public async Task TitleListRowSet()
        {
            int row;
            try
            {
                row = nextTitleRowNo;
            }
            catch (Exception ex)
            {
                responseTitleList = "数値を入力してください。" + ex.Message;
                return;
            }

            if (row < 2)
            {
                responseTitleList = "数値が小さすぎます";
                return;
            }

            if (row > 50)
            {
                responseTitleList = "数値が大きすぎます";
                return;
            }

            row -= 1;

            responseTitleList = "　";

            var api = new HTTP_API();

            var parameters = new Dictionary<string, string>()
            {
                { "Function", "DataSourceSelectRow" },
                { "Value", "SetupText,table," + row.ToString() },
            };

            await updateCell();

            var response = await api.PostHttpWebApi(Http, parameters);

            if (response.ReasonPhrase == "OK")
            {
                responseTitleList = "タイトル一覧をセットしました";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responseTitleList = response.ReasonPhrase;
                }
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
