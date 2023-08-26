using Microsoft.AspNetCore.Components;

namespace JAWSWebApp.Shared
{
    public class PageOperations : ComponentBase
    {
        static readonly HttpClient Http = new();

        // Input につける接尾語
        // public string InputSuffix = "";

        // Twitch に配信情報をセット
        public string responseInformation = "　";

        protected string game_id = "";
        protected string title = "";

        public async Task UpdateInformation()
        {
            var twitch_api = new Twitch_API();

            var parameters = new Dictionary<string, string>()
            {
                { "game_id", game_id },
                { "title", "JAWS PLAYERS 五月鮫 - " + title },
            };

            var response = await twitch_api.ModifyChannelInformation(Http, parameters);

            if (response.ReasonPhrase == "No Content")
            {
                responseInformation = "セット完了";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responseInformation = response.ReasonPhrase;
                }
            }
        }

        public string responseTags = "　";

        protected List<string> tags = new();

        public async Task UpdateTags()
        {
            var twitch_api = new Twitch_API();

            var response = await twitch_api.ReplaceStreamTags(Http, tags);

            if (response.ReasonPhrase == "No Content")
            {
                responseTags = "セット完了";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responseTags = response.ReasonPhrase;
                }
            }
        }

        public string featuredChannels = "";
        public string responseFeaturedChannels = "　";

        public async Task RegistFeaturedChannels()
        {
            var twitch_api = new Twitch_API();

            var response = await twitch_api.SetFeaturedChannels(Http, featuredChannels);

            if (response.ReasonPhrase == "OK")
            {
                responseFeaturedChannels = "セット完了";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responseFeaturedChannels = response.ReasonPhrase;
                }
            }
        }

        // 画面に表示するテキストを変更
        public DateTime estTime = DateTime.Parse("0:00:00");
        public string titleLine1 = "";
        public string titleLine2 = "";
        public string titleLine3 = "";
        public string subTitleLine1 = "";
        public string subTitleLine2 = "";
        public string subTitleLine3 = "";
        public string responseText = "　";

        public async Task ChangeDisplayText(string inputSuffix)
        {
            var api = new HTTP_API();

            // 予定時間を変更
            var param_est = new Dictionary<string, string>()
            {
                { "Function", "SetText" },
                { "Input", "テキスト" + inputSuffix },
                { "SelectedName", "Est.Text" },
                { "Value", estTime.ToString("H:mm:ss") },
            };

            var res_est = await api.PostHttpWebApi(Http, param_est);

            if (res_est.ReasonPhrase != "OK")
            {
                if (res_est.ReasonPhrase != null)
                {
                    responseText = res_est.ReasonPhrase;
                }
                return;
            }

            var param_title1 = new Dictionary<string, string>()
            {
                { "Function", "SetText" },
                { "Input", "テキスト" + inputSuffix },
                { "SelectedName", "TitleLine1.Text" },
                { "Value", titleLine1 },
            };

            var res_title1 = await api.PostHttpWebApi(Http, param_title1);

            if (res_title1.ReasonPhrase != "OK")
            {
                if (res_title1.ReasonPhrase != null)
                {
                    responseText = res_title1.ReasonPhrase;
                }
                return;
            }

            var param_title2 = new Dictionary<string, string>()
            {
                { "Function", "SetText" },
                { "Input", "テキスト" + inputSuffix },
                { "SelectedName", "TitleLine2.Text" },
                { "Value", titleLine2 },
            };

            var res_title2 = await api.PostHttpWebApi(Http, param_title2);

            if (res_title2.ReasonPhrase != "OK")
            {
                if (res_title2.ReasonPhrase != null)
                {
                    responseText = res_title2.ReasonPhrase;
                }
                return;
            }

            var param_title3 = new Dictionary<string, string>()
            {
                { "Function", "SetText" },
                { "Input", "テキスト" + inputSuffix },
                { "SelectedName", "TitleLine3.Text" },
                { "Value", titleLine3 },
            };

            var res_title3 = await api.PostHttpWebApi(Http, param_title3);

            if (res_title3.ReasonPhrase != "OK")
            {
                if (res_title3.ReasonPhrase != null)
                {
                    responseText = res_title3.ReasonPhrase;
                }
                return;
            }

            var param_subTitle1 = new Dictionary<string, string>()
            {
                { "Function", "SetText" },
                { "Input", "テキスト" + inputSuffix },
                { "SelectedName", "SubTitleLine1.Text" },
                { "Value", subTitleLine1 },
            };

            var res_subTitle1 = await api.PostHttpWebApi(Http, param_subTitle1);

            if (res_subTitle1.ReasonPhrase != "OK")
            {
                if (res_subTitle1.ReasonPhrase != null)
                {
                    responseText = res_subTitle1.ReasonPhrase;
                }
                return;
            }

            var param_subTitle2 = new Dictionary<string, string>()
            {
                { "Function", "SetText" },
                { "Input", "テキスト" + inputSuffix },
                { "SelectedName", "SubTitleLine2.Text" },
                { "Value", subTitleLine2 },
            };

            var res_subTitle2 = await api.PostHttpWebApi(Http, param_subTitle2);

            if (res_subTitle2.ReasonPhrase != "OK")
            {
                if (res_subTitle2.ReasonPhrase != null)
                {
                    responseText = res_subTitle2.ReasonPhrase;
                }
                return;
            }

            var param_subTitle3 = new Dictionary<string, string>()
            {
                { "Function", "SetText" },
                { "Input", "テキスト" + inputSuffix },
                { "SelectedName", "SubTitleLine3.Text" },
                { "Value", subTitleLine3 },
            };

            var res_subTitle3 = await api.PostHttpWebApi(Http, param_subTitle3);

            if (res_subTitle3.ReasonPhrase == "OK")
            {
                responseText = "テキストを変更しました！";
            }
            else
            {
                if (res_subTitle3.ReasonPhrase != null)
                {
                    responseText = res_subTitle3.ReasonPhrase;
                }
            }
        }

        // メイン画面遷移
        public string responsePreviewInput = "　";

        public async Task SetPreviewInput(string inputSuffix)
        {
            var api = new HTTP_API();

            var parameters = new Dictionary<string, string>()
            {
                { "Function", "PreviewInput" },
                { "Input", "ベース" + inputSuffix },
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

        public void FadeClick()
        {
            isFadeDisabled = true;
            isFadeConfirmDisabled = false;
        }

        public string responseFade = "　";

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

            // isFadeDisabled = false;
        }

        public bool isCutDisabled = false;
        public bool isCutConfirmDisabled = true;

        public void CutClick()
        {
            isCutDisabled = true;
            isCutConfirmDisabled = false;
        }

        public string responseCut = "　";

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

            // isCutDisabled = false;
        }

        public bool isStinger1Disabled = false;
        public bool isStinger1ConfirmDisabled = true;

        public bool isMergeDisabled = false;
        public bool isMergeConfirmDisabled = true;

        public void MergeClick()
        {
            isMergeDisabled = true;
            isMergeConfirmDisabled = false;
        }

        public string responseMerge = "　";

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

            responsePreviewInput = "　";
        }

        public void Stinger1Click()
        {
            isStinger1Disabled = true;
            isStinger1ConfirmDisabled = false;
        }

        public string responseStinger1 = "　";

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

            // isStinger1Disabled = false;
        }

        public bool isStinger2Disabled = false;
        public bool isStinger2ConfirmDisabled = true;

        public void Stinger2Click()
        {
            isStinger2Disabled = true;
            isStinger2ConfirmDisabled = false;
        }

        public string responseStinger2 = "　";

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

        public async Task TimerStart(string inputSuffix)
        {
            await TimerStart(inputSuffix, "Timer.Text");
        }

        public async Task TimerStart(string inputSuffix, string timer_name)
        {
            isStartDisabled = true;
            isPauseDisabled = false;
            isStopDisabled = false;

            var api = new HTTP_API();

            var param_start = new Dictionary<string, string>()
            {
                { "Function", "StartCountdown" },
                { "Input", "テキスト" + inputSuffix },
                { "SelectedName", timer_name },
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
                { "Input", "テキスト" + inputSuffix },
                { "SelectedName", timer_name },
                { "Value", "#FFFFFF" },
            };

            var res_color = await api.PostHttpWebApi(Http, param_color);

            if (res_color.ReasonPhrase == "OK")
            {
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

        public async Task TimerPause(string inputSuffix)
        {
            await TimerPause(inputSuffix, "Timer.Text");
        }

        public async Task TimerPause(string inputSuffix, string timer_name)
        {
            isStartDisabled = false;
            isStopDisabled = false;
            isPauseDisabled = true;

            var api = new HTTP_API();

            // タイマーを一時停止にする
            var param_pause = new Dictionary<string, string>()
            {
                { "Function", "PauseCountdown" },
                { "Input", "テキスト" + inputSuffix },
                { "SelectedName", timer_name },
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
                { "Input", "テキスト" + inputSuffix },
                { "SelectedName", timer_name },
                { "Value", "#808080" },
            };

            var res_color = await api.PostHttpWebApi(Http, param_color);

            if (res_color.ReasonPhrase == "OK")
            {
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

        public async Task TimerStop(string inputSuffix)
        {
            await TimerStop(inputSuffix, "Timer.Text");
        }

        public async Task TimerStop(string inputSuffix, string timer_name)
        {
            isStartDisabled = false;
            isPauseDisabled = false;
            isStopDisabled = true;

            var api = new HTTP_API();

            // タイマーを一時停止にする
            var param_pause = new Dictionary<string, string>()
            {
                { "Function", "PauseCountdown" },
                { "Input", "テキスト" + inputSuffix },
                { "SelectedName", timer_name },
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
                { "Input", "テキスト" + inputSuffix },
                { "SelectedName", timer_name },
                { "Value", "#FFFF00" },
            };

            var res_color = await api.PostHttpWebApi(Http, param_color);

            if (res_color.ReasonPhrase == "OK")
            {
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
        public string responseTimerChange = "　";

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

        public async Task TimerChangeExecute(string inputSuffix)
        {
            isChangeExecuteDisabled = true;

            var api = new HTTP_API();

            // タイマーを修正
            var parameters = new Dictionary<string, string>()
            {
                { "Function", "ChangeCountdown" },
                { "Input", "テキスト" + inputSuffix },
                { "SelectedName", "Timer.Text" },
                { "Value", timerChange },
            };

            var response = await api.PostHttpWebApi(Http, parameters);

            if (response.ReasonPhrase == "OK")
            {
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
        public string responseTimerReset = "　";

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

        public async Task TimerResetExecute(string inputSuffix)
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
                { "Input", "テキスト" + inputSuffix },
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
            { "Input", "テキスト" + inputSuffix },
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

        // セットアップ時にメイン音量をフェードアウトする
        public bool isFadeoutBGMDisabled = false;
        public bool isFadeoutBGMConfirmDisabled = true;

        public void FadeoutBGMClick()
        {
            isFadeoutBGMDisabled = true;
            isFadeoutBGMConfirmDisabled = false;
        }

        public string responseFadeoutBGM = "　";

        public async Task FadeoutBGM(string input)
        {
            isFadeoutBGMConfirmDisabled = true;

            var api = new HTTP_API();

            var parameters = new Dictionary<string, string>()
            {
                { "Function", "SetVolumeFade" },
                { "Value", "0,3000" },
                { "Input", input },
            };

            var response = await api.PostHttpWebApi(Http, parameters);

            if (response.ReasonPhrase == "OK")
            {
                responseFadeoutBGM = "BGMフェードアウトまで 3 秒お待ちください";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responseFadeoutBGM = response.ReasonPhrase;
                }
            }

            isFadeoutBGMDisabled = false;
        }

        // 画面切り替え前にゲームの音量を0にする
        public bool isSetVolumeZeroDisabled = false;
        public bool isSetVolumeZeroConfirmDisabled = false;

        public void SetVolumeZeroClick()
        {
            isSetVolumeZeroDisabled = true;
            isSetVolumeZeroConfirmDisabled = false;
        }

        public string responseSetVolumeZero = "　";

        public async Task SetVolumeZeroBGM(string srt_input)
        {
            isSetVolumeZeroConfirmDisabled = true;

            var api = new HTTP_API();

            var parameters = new Dictionary<string, string>()
            {
                { "Function", "SetVolume" },
                { "Value", "0" },
                { "Input", srt_input },
            };

            var response = await api.PostHttpWebApi(Http, parameters);

            if (response.ReasonPhrase == "OK")
            {
                responseSetVolumeZero = "ゲーム音量を0にしました（画面切り替えで自動フェードインします）";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responseSetVolumeZero = response.ReasonPhrase;
                }
            }

            isSetVolumeZeroDisabled = false;
        }

        // セットアップのタイトル一覧を操作する
        public string responseTitleList = "　";

        public async Task TitleListNext()
        {
            responseTitleList = "　";

            var api = new HTTP_API();

            var parameters = new Dictionary<string, string>()
            {
                { "Function", "DataSourceNextRow" },
                { "Value", "SetupText,vMix" },
            };

            var response = await api.PostHttpWebApi(Http, parameters);

            if (response.ReasonPhrase == "OK")
            {
                responseTitleList = "タイトル一覧を次に送りました";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responseTitleList = response.ReasonPhrase;
                }
            }
        }

        public async Task TitleListPrevious()
        {
            responseTitleList = "　";

            var api = new HTTP_API();

            var parameters = new Dictionary<string, string>()
            {
                { "Function", "DataSourcePreviousRow" },
                { "Value", "SetupText,vMix" },
            };

            var response = await api.PostHttpWebApi(Http, parameters);

            if (response.ReasonPhrase == "OK")
            {
                responseTitleList = "タイトル一覧を前に送りました";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responseTitleList = response.ReasonPhrase;
                }
            }
        }

        public string titleListRow = "";

        public async Task TitleListRowSet()
        {
            int row;
            try
            {
                row = int.Parse(titleListRow);
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

            if (row > 30)
            {
                responseTitleList = "数値が大きすぎます";
                return;
            }

            row -= 2;

            responseTitleList = "　";

            var api = new HTTP_API();

            var parameters = new Dictionary<string, string>()
            {
                { "Function", "DataSourceSelectRow" },
                { "Value", "SetupText,vMix," + row.ToString() },
            };

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

        // チェックボックスによる表示・非表示
        public string displayBefore = "";
        public string displayAfter = "";

        public bool display = false;
        public bool sound1 = false;
        public bool sound2 = false;
        public bool sound3 = false;

        public void CheckChecBoxes()
        {
            if (display == false) return;
            if (sound1 == false) return;
            if (sound2 == false) return;
            if (sound3 == false) return;

            displayBefore = "none";
            displayAfter = "all";
        }

        // 左下枠を表示・非表示
        public string responseSetFrame = "　";

        public async Task SetFrame(string frame)
        {
            if (!string.IsNullOrEmpty(frame))
            {
                var api = new HTTP_API();

                var parameters = new Dictionary<string, string>()
                {
                    { "Function", "LayerOff" },
                    { "Input", "左下枠オーバーレイ" },
                    { "Value", "1" },
                };

                var response = await api.PostHttpWebApi(Http, parameters);

                if (response.ReasonPhrase != "OK")
                {
                    if (response.ReasonPhrase != null)
                    {
                        responseSetFrame = response.ReasonPhrase;
                    }
                }

                parameters = new Dictionary<string, string>()
                {
                    { "Function", "LayerOff" },
                    { "Input", "左下枠オーバーレイ" },
                    { "Value", "2" },
                };

                response = await api.PostHttpWebApi(Http, parameters);

                if (response.ReasonPhrase == "OK")
                {
                    responseSetFrame = "ワイプ枠を非表示にしました";
                }
                else
                {
                    if (response.ReasonPhrase != null)
                    {
                        responseSetFrame = response.ReasonPhrase;
                    }
                }

                if (frame == "RTA")
                {
                    parameters = new Dictionary<string, string>()
                    {
                        { "Function", "LayerOn" },
                        { "Input", "左下枠オーバーレイ" },
                        { "Value", "1" },
                    };

                    response = await api.PostHttpWebApi(Http, parameters);

                    if (response.ReasonPhrase == "OK")
                    {
                        responseSetFrame = "ワイプ枠をピンク色にセット";
                    }
                    else
                    {
                        if (response.ReasonPhrase != null)
                        {
                            responseSetFrame = response.ReasonPhrase;
                        }
                    }
                }
                else if (frame == "PLAN")
                {
                    parameters = new Dictionary<string, string>()
                    {
                        { "Function", "LayerOff" },
                        { "Input", "左下枠オーバーレイ" },
                        { "Value", "2" },
                    };

                    response = await api.PostHttpWebApi(Http, parameters);

                    if (response.ReasonPhrase == "OK")
                    {
                        responseSetFrame = "ワイプ枠を黄色にセット";
                    }
                    else
                    {
                        if (response.ReasonPhrase != null)
                        {
                            responseSetFrame = response.ReasonPhrase;
                        }
                    }
                }
            }
        }

        // オーバーレイ2のアイコンを再生
        public string responsePlayStamps = "　";

        public async Task PlayStamps()
        {
            var api = new HTTP_API();

            var parameters = new Dictionary<string, string>()
                {
                    { "Function", "Play" },
                    { "Input", "スタンプ" },
                };

            var response = await api.PostHttpWebApi(Http, parameters);

            if (response.ReasonPhrase == "OK")
            {
                responsePlayStamps = "スタンプを再生しました";
            }
            else
            {
                if (response.ReasonPhrase != null)
                {
                    responsePlayStamps = response.ReasonPhrase;
                }
            }
        }
    }
}
