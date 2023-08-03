using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UniverseLib;

namespace ColoredGasses
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    [BepInProcess("rocketstation.exe")]
    public class BepinXLoader : BaseUnityPlugin
    {
        public const string PLUGIN_GUID = "com.ihatetn931.ColoredGasses";
        public const string PLUGIN_NAME = "Colored Gasses";
        public const string PLUGIN_VERSION = "1.0.0";

        /*public static Color black = new Color32(0, 0, 0, 255);
        public static Color colorCarbon = new Color32(128, 128, 128, 255); //Carbon
        public static Color colorVolatiles = new Color32(232, 0, 254, 255); //volatiles
        public static Color colorPollutant = new Color32(255, 255, 0, 255); //pollutant
        public static Color colorNitrogen = new Color32(255, 140, 0, 255); //nitrogen
        public static Color colorOxygen = new Color32(0, 201, 254, 255); //oxygen
        public static Color colorNitrousOxide = new Color32(0, 255, 0, 255); //nitrousOxide
        public static Color colorWater = new Color32(0, 0, 255, 255); //water*/

        internal static ConfigEntry<bool> toggleColorGasParticles;
        internal static ConfigEntry<bool> toggleColorCondensationParticles;
        internal static ConfigEntry<int> RGBRedCarbon;
        internal static ConfigEntry<int> RGBGreenCarbon;
        internal static ConfigEntry<int> RGBBlueCarbon;
        internal static ConfigEntry<int> RGBRedVolatiles;
        internal static ConfigEntry<int> RGBGreenVolatiles;
        internal static ConfigEntry<int> RGBBlueVolatiles;
        internal static ConfigEntry<int> RGBRedPollutant;
        internal static ConfigEntry<int> RGBGreenPollutant;
        internal static ConfigEntry<int> RGBBluePollutant;
        internal static ConfigEntry<int> RGBRedNitrogen;
        internal static ConfigEntry<int> RGBGreenNitrogen;
        internal static ConfigEntry<int> RGBBlueNitrogen;
        internal static ConfigEntry<int> RGBRedOxygen;
        internal static ConfigEntry<int> RGBGreenOxygen;
        internal static ConfigEntry<int> RGBBlueOxygen;
        internal static ConfigEntry<int> RGBRedNitrousOxide;
        internal static ConfigEntry<int> RGBGreenNitrousOxide;
        internal static ConfigEntry<int> RGBBlueNitrousOxide;
        internal static ConfigEntry<int> RGBRedWater;
        internal static ConfigEntry<int> RGBGreenWater;
        internal static ConfigEntry<int> RGBBlueWater;
        internal static ConfigEntry<KeyboardShortcut> _keybind;

        public static ConfigurationManager.ConfigurationManager man;
        public static GameObject go;

        void Awake()
        {
            BepinXLoader.go = new GameObject("ConfigurationManager");
            BepinXLoader.man = BepinXLoader.go.AddComponent<ConfigurationManager.ConfigurationManager>();
            Logger.LogInfo($"[ColoredGasses] Plugin {PLUGIN_GUID} is loading!");
            Harmony harmony = new Harmony(PLUGIN_GUID);
            harmony.PatchAll();
            LoadConfig();
            Logger.LogInfo($"[ColoredGasses] Plugin {PLUGIN_GUID} is loaded!");
        }

        void LoadConfig()
        {
            Debug.Log("[ColoredGasses] Settings Loading...");
            AddToggles();
            AddSliderInputs();
            AddKeyInputs();
            Debug.Log("[ColoredGasses] Settings Loaded");
        }

        void AddToggles()
        {
            toggleColorGasParticles = Config.Bind("Gasses Color", "Enable", true, new ConfigDescription("Enables Gas Colors, use Advanced Settings to change the colors", null, new ConfigurationManagerAttributes { Order = 2 }));
            toggleColorCondensationParticles = Config.Bind("Condensation Color", "Enable", false, new ConfigDescription("Enables Condensation Colors, use Advanced Setting to change the colors", null, new ConfigurationManagerAttributes { Order = 1}));
        }

        public void AddSliderInputs()
        {
            RGBRedCarbon = Config.Bind("Carbon Color", "Red", 128, new ConfigDescription("Red for Carbon Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true,Order = 21 }));
            RGBGreenCarbon = Config.Bind("Carbon Color", "Green", 128, new ConfigDescription("Green for Carbon Color", new AcceptableValueRange<int>(0, 255),new ConfigurationManagerAttributes { IsAdvanced = true, Order = 20 }));
            RGBBlueCarbon = Config.Bind("Carbon Color", "Blue", 128, new ConfigDescription("Blue for Carbon Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true, Order = 19 }));

            RGBRedVolatiles = Config.Bind("Volatiles Color", "Red", 232, new ConfigDescription("Red for Volatile Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true, Order = 18 }));
            RGBGreenVolatiles = Config.Bind("Volatiles Color", "Green", 0, new ConfigDescription("Green for Volatile Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true, Order = 17 }));
            RGBBlueVolatiles = Config.Bind("Volatiles Color", "Blue", 254, new ConfigDescription("Blue for Volatile Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true, Order = 16 }));

            RGBRedPollutant = Config.Bind("Pollutant Color", "Red", 255, new ConfigDescription("Red for Pollutant Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true, Order = 15}));
            RGBGreenPollutant = Config.Bind("Pollutant Color", "Green", 255, new ConfigDescription("Green for Pollutant Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true, Order = 14 }));
            RGBBluePollutant = Config.Bind("Pollutant Color", "Blue", 0, new ConfigDescription("Blue for Pollutant Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true, Order = 13 }));

            RGBRedNitrogen = Config.Bind("Nitrogen Color", "Red", 255, new ConfigDescription("Red for Nitrogen Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true, Order = 12 }));
            RGBGreenNitrogen = Config.Bind("Nitrogen Color", "Green", 140, new ConfigDescription("Green for Nitrogen Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true, Order = 11 }));
            RGBBlueNitrogen = Config.Bind("Nitrogen Color", "Blue", 0, new ConfigDescription("Blue for Nitrogen Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true, Order = 10 }));

            RGBRedOxygen = Config.Bind("Oxygen Color", "Red", 0, new ConfigDescription("Red for Oxygen Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true, Order = 9 }));
            RGBGreenOxygen = Config.Bind("Oxygen Color", "Green", 201, new ConfigDescription("Green for Oxygen Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true, Order = 8 }));
            RGBBlueOxygen = Config.Bind("Oxygen Color", "Blue", 254, new ConfigDescription("Blue for Oxygen Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true, Order = 7 }));

            RGBRedNitrousOxide = Config.Bind("Nitrous Oxide Color", "Red", 0, new ConfigDescription("Red for Nitrous Oxide Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true, Order = 6 }));
            RGBGreenNitrousOxide = Config.Bind("Nitrous Oxide Color", "Green", 255, new ConfigDescription("Green for Nitrous Oxide Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true, Order = 5 }));
            RGBBlueNitrousOxide = Config.Bind("Nitrous Oxide Color", "Blue", 0, new ConfigDescription("Blue for Nitrous Oxide Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true, Order = 4 }));

            RGBRedWater = Config.Bind("Water Color", "Red", 0, new ConfigDescription("Red for Water Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true, Order = 3 }));
            RGBGreenWater = Config.Bind("Water Color", "Green", 0, new ConfigDescription("Green for Water Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true, Order = 1 }));
            RGBBlueWater = Config.Bind("Water Color", "Blue", 255, new ConfigDescription("Blue for Water Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { IsAdvanced = true, Order = 1 }));
        }

        void AddKeyInputs()
        {
            _keybind = Config.Bind("General", "Show this window hotkey", new KeyboardShortcut(KeyCode.F6), new ConfigDescription("Change which key you want to open this menu", null, null));
        }
    }
}


