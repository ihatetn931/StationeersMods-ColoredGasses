using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using UniverseLib.Config;

namespace ColoredGasses
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    //[BepInProcess("rocketstation.exe")]
    public class ColoredGasses : BaseUnityPlugin
    {
        public const string PLUGIN_GUID = "com.ihatetn931.ColoredGasses";
        public const string PLUGIN_NAME = "Colored Gasses";
        public const string PLUGIN_VERSION = "1.0.0";
        public static GameObject ColoredGassesGameObject;

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

        public ColoredGasses()
        {
            ColoredGassesGameObject = new GameObject("ColoredGasses");
        }

        void Awake()
        {
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
            Debug.Log("[ColoredGasses] Settings Loaded");
        }

        void AddToggles()
        {
            toggleColorGasParticles = Config.Bind("AColoredGasses Toggles", "AColoredGasses Color Enable", true, new ConfigDescription("Enables Gas Colors, use RGB sliders to change the colors",null, new ConfigurationManagerAttributes { Order = 999 }));
            toggleColorCondensationParticles = Config.Bind("AColoredGasses Toggles", "ACondensation Color Enable", false, new ConfigDescription("Enables Condensation Colors, use Advanced Setting to change the colors", null, new ConfigurationManagerAttributes { Order = 1,Browsable = false}));
        }

        public void AddSliderInputs()
        {
            RGBRedCarbon = Config.Bind("ColoredGasses Carbon RGB Color Sliders", "Carbon Color Red", 128, new ConfigDescription("Red for Carbon Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes {Browsable = true }));
            RGBGreenCarbon = Config.Bind("ColoredGasses Carbon RGB Color Sliders", "Carbon Color Green", 128, new ConfigDescription("Green for Carbon Color", new AcceptableValueRange<int>(0, 255),new ConfigurationManagerAttributes { Browsable = true }));
            RGBBlueCarbon = Config.Bind("ColoredGasses Carbon RGB Color Sliders", "Carbon Color Blue", 128, new ConfigDescription("Blue for Carbon Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes { Browsable = true }));

            RGBRedVolatiles = Config.Bind("ColoredGasses Volatiles RGB Color Sliders", "Volatiles Color Red", 232, new ConfigDescription("Red for Volatile Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes {  Browsable = true }));
            RGBGreenVolatiles = Config.Bind("ColoredGasses Volatiles RGB Color Sliders", "Volatiles Color Green", 0, new ConfigDescription("Green for Volatile Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes {  Browsable = true }));
            RGBBlueVolatiles = Config.Bind("ColoredGasses Volatiles RGB Color Sliders", "Volatiles Color Blue", 254, new ConfigDescription("Blue for Volatile Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes {   Browsable = true }));

            RGBRedPollutant = Config.Bind("ColoredGasses Pollutant RGB Color Sliders", "Pollutant Color Red", 255, new ConfigDescription("Red for Pollutant Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes {  Browsable = true }));
            RGBGreenPollutant = Config.Bind("ColoredGasses Pollutant RGB Color Sliders", "Pollutant Color Green", 255, new ConfigDescription("Green for Pollutant Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes {  Browsable = true }));
            RGBBluePollutant = Config.Bind("ColoredGasses Pollutant RGB Color Sliders", "Pollutant Color Blue", 0, new ConfigDescription("Blue for Pollutant Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes {   Browsable = true }));

            RGBRedNitrogen = Config.Bind("ColoredGasses Nitrogen RGB Color Sliders", "Nitrogen Color Red", 255, new ConfigDescription("Red for Nitrogen Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes {   Browsable = true }));
            RGBGreenNitrogen = Config.Bind("ColoredGasses Nitrogen RGB Color Sliders", "Nitrogen Color Green", 140, new ConfigDescription("Green for Nitrogen Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes {   Browsable = true }));
            RGBBlueNitrogen = Config.Bind("ColoredGasses Nitrogen RGB Color Sliders", "Nitrogen Color Blue", 0, new ConfigDescription("Blue for Nitrogen Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes {   Browsable = true }));

            RGBRedOxygen = Config.Bind("ColoredGasses Oxygen RGB Color Sliders", "Oxygen Color Red", 0, new ConfigDescription("Red for Oxygen Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes {  Browsable = true }));
            RGBGreenOxygen = Config.Bind("ColoredGasses Oxygen RGB Color Sliders", "Oxygen Color Green", 201, new ConfigDescription("Green for Oxygen Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes {   Browsable = true }));
            RGBBlueOxygen = Config.Bind("ColoredGasses Oxygen RGB Color Sliders", "Oxygen Color Blue", 254, new ConfigDescription("Blue for Oxygen Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes {   Browsable = true }));

            RGBRedNitrousOxide = Config.Bind("ColoredGasses Nitrous RGB Color Sliders", "Nitrous Oxide Color Red", 0, new ConfigDescription("Red for Nitrous Oxide Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes {  Browsable = true }));
            RGBGreenNitrousOxide = Config.Bind("ColoredGasses Nitrous RGB Color Sliders", "Nitrous Oxide Color Green", 255, new ConfigDescription("Green for Nitrous Oxide Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes {  Browsable = true }));
            RGBBlueNitrousOxide = Config.Bind("ColoredGasses Nitrous RGB Color Sliders", "Nitrous Oxide Color Blue", 0, new ConfigDescription("Blue for Nitrous Oxide Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes {   Browsable = true }));

            RGBRedWater = Config.Bind("ColoredGasses Water RGB Color Sliders", "Water Color Red", 0, new ConfigDescription("Red for Water Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes {   Browsable = true }));
            RGBGreenWater = Config.Bind("ColoredGasses Water RGB Color Sliders", "Water Color Green", 0, new ConfigDescription("Green for Water Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes {   Browsable = true }));
            RGBBlueWater = Config.Bind("ColoredGasses Water RGB Color Sliders", "Water Color Blue", 255, new ConfigDescription("Blue for Water Color", new AcceptableValueRange<int>(0, 255), new ConfigurationManagerAttributes {  Browsable = true }));
        }
    }
}


