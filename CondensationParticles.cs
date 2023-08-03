using Assets.Scripts.Atmospherics;
using Assets.Scripts.Objects;
using HarmonyLib;
using System;
using System.Net.NetworkInformation;
using UnityEngine;

namespace ColoredGasses
{
    internal class CondensationParticles
    {
        [HarmonyPatch(typeof(AtmosphericFog), nameof(AtmosphericFog.EmitAtmosphericFogParticles))]
        public static class AtmosphericFog_EmitAtmosphericFogParticles_Patch
        {
            [HarmonyPrefix]
            static bool Prefix(AtmosphericFog __instance)
            {
                if (AtmosphericFog.AllAtmosphericFogs.Count <= 0)
                {
                    return false;
                }
                int num = AtmosphericFog.MAXFogParticles - AtmosphericFog.AtmosphericFogParticleSystem.particleCount;
                int num2 = (AtmosphericFog._lastIndex != -1) ? AtmosphericFog._lastIndex : (AtmosphericFog.AllAtmosphericFogs.Count - 1);
                if (num2 > AtmosphericFog.AllAtmosphericFogs.Count - 1)
                {
                    num2 = AtmosphericFog.AllAtmosphericFogs.Count - 1;
                }
                int num3 = 0;
                for (int i = num2; i >= 0; i--)
                {
                    try
                    {
                        if (num <= 0 || (float)num3 >= 30f)
                        {
                            AtmosphericFog._lastIndex = i;
                            break;
                        }
                        AtmosphericFog._lastIndex = i;
                        AtmosphericFog atmosphericFog = AtmosphericFog.AllAtmosphericFogs[i];
                        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
                        float maxValue = 0f;
                        if (atmosphericFog.IsEmitting() && !atmosphericFog.IsEmitCooldown)
                        {
                            if (BepinXLoader.toggleColorCondensationParticles.Value)
                            {
                                float water = atmosphericFog.Atmosphere.GetGasTypeRatio(Chemistry.GasType.Water);
                                float liquidCo2 = atmosphericFog.Atmosphere.GetGasTypeRatio(Chemistry.GasType.LiquidCarbonDioxide);
                                float liquidNitrogen = atmosphericFog.Atmosphere.GetGasTypeRatio(Chemistry.GasType.LiquidNitrogen);
                                float liquidNitrousOxide = atmosphericFog.Atmosphere.GetGasTypeRatio(Chemistry.GasType.LiquidNitrousOxide);
                                float liquidOxygen = atmosphericFog.Atmosphere.GetGasTypeRatio(Chemistry.GasType.LiquidOxygen);
                                float liquidPollutant = atmosphericFog.Atmosphere.GetGasTypeRatio(Chemistry.GasType.LiquidPollutant);
                                float liquidVolatiles = atmosphericFog.Atmosphere.GetGasTypeRatio(Chemistry.GasType.LiquidVolatiles);
                                AtmosphericFog.AtmosphereFogVisualizerTransform.position = atmosphericFog.EmissionPosition();
                                if (water / 100 > maxValue)
                                {
                                    emitParams.startColor = new Color32((byte)BepinXLoader.RGBRedWater.Value, (byte)BepinXLoader.RGBGreenWater.Value, (byte)BepinXLoader.RGBBlueWater.Value, 255);
                                    AtmosphericFog.AtmosphericFogParticleSystem.Emit(emitParams, 1);
                                }
                                if (liquidCo2 / 100 > maxValue)
                                {
                                    emitParams.startColor = new Color32((byte)BepinXLoader.RGBRedCarbon.Value, (byte)BepinXLoader.RGBGreenCarbon.Value, (byte)BepinXLoader.RGBBlueCarbon.Value, 255);
                                    AtmosphericFog.AtmosphericFogParticleSystem.Emit(emitParams, 1);
                                }
                                if (liquidNitrogen / 100 > maxValue)
                                {
                                    emitParams.startColor = new Color32((byte)BepinXLoader.RGBRedNitrogen.Value, (byte)BepinXLoader.RGBGreenNitrogen.Value, (byte)BepinXLoader.RGBBlueNitrogen.Value, 255);
                                    AtmosphericFog.AtmosphericFogParticleSystem.Emit(emitParams, 1);
                                }
                                if (liquidNitrousOxide / 100 > maxValue)
                                {
                                    emitParams.startColor = new Color32((byte)BepinXLoader.RGBRedNitrousOxide.Value, (byte)BepinXLoader.RGBGreenNitrousOxide.Value, (byte)BepinXLoader.RGBBlueNitrousOxide.Value, 255);
                                    AtmosphericFog.AtmosphericFogParticleSystem.Emit(emitParams, 1);
                                }
                                if (liquidOxygen / 100 > maxValue)
                                {
                                    emitParams.startColor = new Color32((byte)BepinXLoader.RGBRedOxygen.Value, (byte)BepinXLoader.RGBGreenOxygen.Value, (byte)BepinXLoader.RGBBlueOxygen.Value, 255);
                                    AtmosphericFog.AtmosphericFogParticleSystem.Emit(emitParams, 1);
                                }
                                if (liquidPollutant / 100 > maxValue)
                                {
                                    emitParams.startColor = new Color32((byte)BepinXLoader.RGBRedPollutant.Value, (byte)BepinXLoader.RGBGreenPollutant.Value, (byte)BepinXLoader.RGBBluePollutant.Value, 255);
                                    AtmosphericFog.AtmosphericFogParticleSystem.Emit(emitParams, 1);
                                }
                                if (liquidVolatiles / 100 > maxValue)
                                {
                                    emitParams.startColor = new Color32((byte)BepinXLoader.RGBRedVolatiles.Value, (byte)BepinXLoader.RGBGreenVolatiles.Value, (byte)BepinXLoader.RGBBlueVolatiles.Value, 255);
                                    AtmosphericFog.AtmosphericFogParticleSystem.Emit(emitParams, 1);
                                }
                            }
                            else
                            {
                                AtmosphericFog.AtmosphericFogParticleSystem.Emit(1);
                            }
                            atmosphericFog._lastEmitTime = Time.fixedTime;
                            num--;
                            num3++;
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        AtmosphericFog._lastIndex = i;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        AtmosphericFog._lastIndex = i;
                    }
                }
                if (num > 0)
                {
                    AtmosphericFog._lastIndex = -1;
                }
                return false;
            }
        }
    }
}
