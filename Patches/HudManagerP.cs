using GameNetcodeStuff;
using HarmonyLib;
using System.Linq;
namespace CookiesVPP.patches
{
    internal class HudManagerP
    {
        [HarmonyPatch(typeof(HUDManager), "SubmitChat_performed")]
        [HarmonyPrefix]
        static bool ChatCommands(HUDManager __instance)
        {
            string text = __instance.chatTextField.text;
            string prefix = "!";
            CookiesVPP_B.mls.LogInfo("Said: " + text);

            if (text.ToLower().StartsWith(prefix))
            {
                if (text.ToLower().StartsWith(prefix + "hidehud"))
                {
                    PlayerControllerBP.hideHud = !PlayerControllerBP.hideHud;
                    __instance.HideHUD(PlayerControllerBP.hideHud);
                    return true;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
    }
}