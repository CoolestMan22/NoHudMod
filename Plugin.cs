using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using CookiesVPP.patches;
using HarmonyLib;
using UnityEngine.InputSystem;
using LethalCompanyInputUtils.Api;

namespace CookiesVPP
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class CookiesVPP_B : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        static CookiesVPP_B Instance;
        public static ConfigEntry<bool> configHideHud;

        public static InputAction hideHud;

        public static ManualLogSource mls = BepInEx.Logging.Logger.CreateLogSource(PluginInfo.PLUGIN_GUID);

        void Awake()
        {
            // Plugin startup logic
            if (Instance == null) Instance = this;

            configHideHud = Config.Bind("General", "hideHud", false, "Changes whether the hud is visible.");

            PlayerControllerBP.hideHud = configHideHud.Value;

            mls.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded :)");
            harmony.PatchAll(typeof(CookiesVPP_B));
            harmony.PatchAll(typeof(PlayerControllerBP));
            harmony.PatchAll(typeof(HudManagerP));
        }

        void Update()
        {
            CookiesVPP_InputClass.instance.HideHud.performed += context => HideHud();
        }

        private void HideHud()
        {
            PlayerControllerBP.hideHud = !PlayerControllerBP.hideHud;
            HUDManager.Instance.HideHUD(PlayerControllerBP.hideHud);
        }
    }

    public class CookiesVPP_InputClass : LcInputActions
    {
        [InputAction("HideHud", "<Keyboard>/F6", "", Name = "HideHud")]
        public InputAction HideHud { get; set; }

        public static CookiesVPP_InputClass instance = new CookiesVPP_InputClass();
    }

    public static class PluginInfo
    {
        public const string PLUGIN_GUID = "CookiesVPP";
        public const string PLUGIN_NAME = "CookiesVPP";
        public const string PLUGIN_VERSION = "1.0.0";
    }
}
