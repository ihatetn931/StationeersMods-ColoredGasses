using Assets.Scripts.Atmospherics;
using Assets.Scripts;
using HarmonyLib;
using UnityEngine;
using ConfigurationManager;
using Steamworks.ServerList;
using BepInEx;
using Assets.Scripts.Util;

namespace ColoredGasses
{
    internal class GasParticles
    {
        [HarmonyPatch(typeof(AtmosphericsManager), nameof(AtmosphericsManager.EmitAirVisualizerParticles))]
        public static class AtmosphericsManager_EmitAirVisualizerParticles_Patch
        {
            public static int id;
            [HarmonyPrefix]
            static bool Prefix(AtmosphericsController atmosphericsController, AtmosphericsManager __instance)
            {
                if (BepinXLoader.toggleColorGasParticles.Value)
                {
                    for (int j = 0; j < atmosphericsController.Atmospheres.Count; j++)
                    {
                        id = j;
                    }
                    if (atmosphericsController.Atmospheres[id].DisplayName.Contains("WorldAtmosphere"))
                    {
                        float carbon = atmosphericsController.Atmospheres[id].GetGasTypeRatio(Chemistry.GasType.CarbonDioxide);
                        float volatiles = atmosphericsController.Atmospheres[id].GetGasTypeRatio(Chemistry.GasType.Volatiles);
                        float pollutant = atmosphericsController.Atmospheres[id].GetGasTypeRatio(Chemistry.GasType.Pollutant);
                        float nitrogen = atmosphericsController.Atmospheres[id].GetGasTypeRatio(Chemistry.GasType.Nitrogen);
                        float oxygen = atmosphericsController.Atmospheres[id].GetGasTypeRatio(Chemistry.GasType.Oxygen);
                        float nitrousOxide = atmosphericsController.Atmospheres[id].GetGasTypeRatio(Chemistry.GasType.NitrousOxide);
                        float maxValue = 0f;
                        if (carbon / 100 > maxValue)
                        {
                            AtmosphericsManager.emitParams.startColor = new Color32((byte)BepinXLoader.RGBRedCarbon.Value, (byte)BepinXLoader.RGBGreenCarbon.Value, (byte)BepinXLoader.RGBBlueCarbon.Value, 255);
                            AtmosphericsManager.Emit(atmosphericsController.Atmospheres, atmosphericsController.GasVisualizerParticleSystem, UnityEngine.Random.insideUnitSphere, AtmosphericsManager.AirVisualizerEmitCondition, true);
                        }
                        if (volatiles / 100 > maxValue)
                        {
                            AtmosphericsManager.emitParams.startColor = new Color32((byte)BepinXLoader.RGBRedVolatiles.Value, (byte)BepinXLoader.RGBGreenVolatiles.Value, (byte)BepinXLoader.RGBBlueVolatiles.Value, 255);
                            AtmosphericsManager.Emit(atmosphericsController.Atmospheres, atmosphericsController.GasVisualizerParticleSystem, UnityEngine.Random.insideUnitSphere, AtmosphericsManager.AirVisualizerEmitCondition, true);
                        }
                        if (pollutant / 100 > maxValue)
                        {
                            AtmosphericsManager.emitParams.startColor = new Color32((byte)BepinXLoader.RGBRedPollutant.Value, (byte)BepinXLoader.RGBGreenPollutant.Value, (byte)BepinXLoader.RGBBluePollutant.Value, 255);
                            AtmosphericsManager.Emit(atmosphericsController.Atmospheres, atmosphericsController.GasVisualizerParticleSystem, UnityEngine.Random.insideUnitSphere, AtmosphericsManager.AirVisualizerEmitCondition, true);
                        }
                        if (nitrogen / 100 > maxValue)
                        {
                            AtmosphericsManager.emitParams.startColor = new Color32((byte)BepinXLoader.RGBRedNitrogen.Value, (byte)BepinXLoader.RGBGreenNitrogen.Value, (byte)BepinXLoader.RGBBlueNitrogen.Value, 255);
                            AtmosphericsManager.Emit(atmosphericsController.Atmospheres, atmosphericsController.GasVisualizerParticleSystem, UnityEngine.Random.insideUnitSphere, AtmosphericsManager.AirVisualizerEmitCondition, true);
                        }
                        if (oxygen / 100 > maxValue)
                        {
                            AtmosphericsManager.emitParams.startColor = new Color32((byte)BepinXLoader.RGBRedOxygen.Value, (byte)BepinXLoader.RGBGreenOxygen.Value, (byte)BepinXLoader.RGBBlueOxygen.Value, 255);
                            AtmosphericsManager.Emit(atmosphericsController.Atmospheres, atmosphericsController.GasVisualizerParticleSystem, UnityEngine.Random.insideUnitSphere, AtmosphericsManager.AirVisualizerEmitCondition, true);
                        }
                        if (nitrousOxide / 100 > maxValue)
                        {
                            AtmosphericsManager.emitParams.startColor = new Color32((byte)BepinXLoader.RGBRedNitrousOxide.Value, (byte)BepinXLoader.RGBGreenNitrousOxide.Value, (byte)BepinXLoader.RGBBlueNitrousOxide.Value, 255);
                            AtmosphericsManager.Emit(atmosphericsController.Atmospheres, atmosphericsController.GasVisualizerParticleSystem, UnityEngine.Random.insideUnitSphere, AtmosphericsManager.AirVisualizerEmitCondition, true);
                        }
                    }
                }
                else
                {
                    AtmosphericsManager.emitParams.startColor = new Color(255, 255, 255, 255);
                    AtmosphericsManager.Emit(atmosphericsController.Atmospheres, atmosphericsController.GasVisualizerParticleSystem, UnityEngine.Random.insideUnitSphere, AtmosphericsManager.AirVisualizerEmitCondition, true);
                }
                return false;
            }
        }

        [HarmonyPatch(typeof(CameraController), nameof(CameraController.ManagerAwake))]
        public static class ManagerBase_ManagerAwake_Patch
        {
            [HarmonyPostfix]
            static void Postfix(ManagerBase __instance)
            {
                BepinXLoader.go = new GameObject("ConfigurationManager");
                BepinXLoader.man = BepinXLoader.go.AddComponent<ConfigurationManager.ConfigurationManager>();
            }
        }

        [HarmonyPatch(typeof(CameraController), nameof(CameraController.LateUpdate))]
        public static class ManagerBase_ManagerUpdate_Patch
        {
            [HarmonyPostfix]
            static void Postfix(ManagerBase __instance)
            {
                //Debug.LogError("1");
                if (BepinXLoader._keybind.Value.IsDown())
                {
                    //Debug.LogError("2");
                    if (BepinXLoader.man != null)
                    {
                        //Debug.LogError("3");
                        BepinXLoader.man.DisplayingWindow = true;
                        //Debug.LogError("4");
                    }
                }
            }
        }
    }
}
