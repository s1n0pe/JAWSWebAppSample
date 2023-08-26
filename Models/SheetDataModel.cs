using System.Text.Json.Serialization;

namespace JAWSWebApp.Models
{
    public class SheetDataModel
    {
        [JsonPropertyName("date")]
        public string? Date { get; set; }
        [JsonPropertyName("start_time")]
        public string? StartTime { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("sub_title")]
        public string? SubTitle { get; set; }
        [JsonPropertyName("scene_name")]
        public string? SceneName { get; set; }
        [JsonPropertyName("text_scene_name")]
        public string? TextSceneName { get; set; }
        [JsonPropertyName("category")]
        public string? Category { get; set; }
        [JsonPropertyName("category_id")]
        public string? CategoryId { get; set; }
        [JsonPropertyName("tags")]
        public string? Tags { get; set; }
        [JsonPropertyName("casts")]
        public string? Casts { get; set; }
        [JsonPropertyName("discord_ids")]
        public string? DiscordIds { get; set; }
        [JsonPropertyName("vmix_call_info")]
        public string? VMixCallInfo { get; set; }
        [JsonPropertyName("est")]
        public string? Est { get; set; }
        [JsonPropertyName("prev_title")]
        public string? PrevTitle { get; set; }
        [JsonPropertyName("next_title")]
        public string? NextTitle { get; set; }
        [JsonPropertyName("layer_settings")]
        public string? LayerSettings { get; set; }
        [JsonPropertyName("is_layer_ok")]
        public string? IsLayerOk { get; set; }
        [JsonPropertyName("srt_settings")]
        public string? SrtSettings { get; set; }
        [JsonPropertyName("is_srt_ok")]
        public string? IsSrtOk { get; set; }
        [JsonPropertyName("voice_settings")]
        public string? VoiceSettings { get; set; }
        [JsonPropertyName("is_voice_ok")]
        public string? IsVoiceOk { get; set; }
        [JsonPropertyName("featured_channels")]
        public string? FeaturedChannels { get; set; }
        [JsonPropertyName("is_done")]
        public string? IsDone { get; set; }
    }
}
