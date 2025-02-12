using HarmonyLib;
using UnityEngine;

namespace BuyableToyRobot.Patches
{
    [HarmonyPatch(typeof(Terminal))]
    internal class BuyableItemsPatch
    {
        [HarmonyPatch(nameof(Terminal.BeginUsingTerminal))]
        [HarmonyPostfix]
        static void addRobotToyPatch(ref Item[] ___buyableItemsList)
        {
            RobotBuyable pluginBase = RobotBuyable.Instance;
            Item robot = pluginBase.val;
            ___buyableItemsList.AddItem(robot);
            pluginBase.logSource.LogInfo("Tried loading asset");
            //___buyableItemsList.AddItem(Resources.Load<Item>("RobotToy"));
            //pluginBase.logSource.LogInfo("Tried loading robot from reference");
            //___buyableItemsList.AddItem(Resources.Load<Item>("Ring"));
            //pluginBase.logSource.LogInfo("Tried loading ring from reference");
        }
    }
}
