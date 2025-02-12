using System.IO;
using BepInEx;
using BepInEx.Logging;
using BuyableToyRobot.Patches;
using HarmonyLib;
using UnityEngine;

namespace BuyableToyRobot
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class RobotBuyable : BaseUnityPlugin
    {
        public static RobotBuyable Instance;

        private const string modGUID = "BuyableToyRobotWulff";
        private const string modName = "BuyableToyRobot";
        private const string modVersion = "1.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        internal ManualLogSource logSource;

        public static AssetBundle ModAssets;

        public Item val;

        private void Awake()
        {
            if (Instance == null) { Instance = this; }

            logSource = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            logSource.LogInfo("Mod has correctly started!");

            string path = "robottoy";
            ModAssets = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(((BaseUnityPlugin)this).Info.Location), path));
            logSource.LogInfo("Asset has been loaded!");
            //if ((UnityEngine.Object)(object)ModAssets == null)
            //{
            //    Logger.LogError("Failed to load custom assets.");
            //    return;
            //}
            val = ModAssets.LoadAsset<Item>("RobotToyBuyable");
            logSource.LogInfo("Robot has been loaded!");

            harmony.PatchAll(typeof(RobotBuyable));
            logSource.LogInfo("Plugin has been patched!");
            harmony.PatchAll(typeof(BuyableItemsPatch));
            logSource.LogInfo("Patch has been patched!");
        }
    }
}
