using System.Reflection;
using System.Text.RegularExpressions;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;

namespace WSMapOnStartPlugin;

public partial class WSMapOnStartPlugin : BasePlugin, IPluginConfig<WSMapOnStartConfig>
{
  public override string ModuleName => "WS Map On Start";
  public override string ModuleVersion =>
    $"v{Assembly.GetExecutingAssembly().GetName().Version ??
    throw new Exception("Version not found.")}";
  public override string ModuleAuthor => "BuSheeZy";
  public override string ModuleDescription => "Change to a workshop map when a specific map starts.";
  public required WSMapOnStartConfig Config { get; set; }

  [GeneratedRegex("^[0-9]+$")]
  private static partial Regex MyRegex();

  public override void Load(bool hotReload)
  {
    RegisterListener<Listeners.OnMapStart>(OnMapStart);
  }

  void OnMapStart(string mapName)
  {
    if (mapName != Config.TriggerMap)
    {
      return;
    }

    Logger.LogInformation($"Map {mapName} has started. Changing to WS map {Config.WSMapId}.");
    ChangeToWsMap(Config.WSMapId);
  }

  public void OnConfigParsed(WSMapOnStartConfig config)
  {
    if (config.WSMapId == "")
    {
      throw new Exception("WSMapId is required in the config.");
    }

    if (!MyRegex().IsMatch(config.WSMapId))
    {
      throw new Exception("WSMapId must be a string containing only numbers.");
    }

    Config = config;
  }

  void ChangeToWsMap(string mapId)
  {
    Server.ExecuteCommand($"host_workshop_map {Config.WSMapId}");
  }
}
