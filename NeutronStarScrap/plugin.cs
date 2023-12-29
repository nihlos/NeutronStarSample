using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LethalLib.Modules;
using UnityEngine;

namespace NeutronStarSample
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class NeutronStarSampleBase : BaseUnityPlugin
    {
        private const string modGUID = "nihl.NeutronStarSample";
        private const string modName = "NeutronStarSample";
        private const string modVersion = "1.0.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static NeutronStarSampleBase instance;

        internal ManualLogSource MLS;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            MLS = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            AssetBundle scrap = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "NeutronStarSample"));
            Item NeutronStar = scrap.LoadAsset<Item>("Assets/NeutronStarSample.asset");

            if (NeutronStar == null)
            {
                MLS.LogInfo("Failed to load Star prefab,");
            }
            else
            {
                NetworkPrefabs.RegisterNetworkPrefab(NeutronStar.spawnPrefab);
                Items.RegisterScrap(NeutronStar, 3, Levels.LevelTypes.All);
            }

            MLS.LogInfo("NeutronStarSample Loaded!");
        }
    }
}
