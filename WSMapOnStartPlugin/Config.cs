using System.Text.Json.Serialization;
using CounterStrikeSharp.API.Core;

namespace WSMapOnStartPlugin;

public class WSMapOnStartConfig : BasePluginConfig
{
  [JsonPropertyName("TriggerMap")] public string TriggerMap { get; set; } = "de_dust2";
  [JsonPropertyName("WSMapId")] public string WSMapId { get; set; } = "";
}
