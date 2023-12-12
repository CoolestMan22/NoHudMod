using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using CookiesVPP.patches;
using HarmonyLib;

namespace CookiesVPP
{

    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class CookiesVPP_B : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        static CookiesVPP_B Instance;
        public static ConfigEntry<bool> configHideHud;
        //public static ConfigEntry<bool> configDisableSpeakerIntro;

        public static ManualLogSource mls = BepInEx.Logging.Logger.CreateLogSource(PluginInfo.PLUGIN_GUID);

        void Awake()
        {
            // Plugin startup logic
            if (Instance == null) Instance = this;

            configHideHud = Config.Bind("General", "hideHud", false, "Changes whether the first person visor is visible.");
            //configDisableSpeakerIntro = Config.Bind("General", "disableSpeakerIntro", true, "When true, the intro will not play from the speaker.");
            PlayerControllerBP.hideHud = configHideHud.Value;
            //StartOfRoundP.disableIntro = configDisableSpeakerIntro.Value;

            mls.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded :)");
            harmony.PatchAll(typeof(CookiesVPP_B));
            harmony.PatchAll(typeof(PlayerControllerBP));
            //harmony.PatchAll(typeof(StartOfRoundP));
            harmony.PatchAll(typeof(HudManagerP));
        }
    }

    
    public static class PluginInfo {
        public const string PLUGIN_GUID = "CookiesVPP";
        public const string PLUGIN_NAME = "CookiesVPP";
        public const string PLUGIN_VERSION = "1.0.0";
    }
}
