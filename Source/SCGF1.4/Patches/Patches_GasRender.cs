using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;

namespace SCGF
{
    [HarmonyPatch]
    public static class Patches_GasRender
    {
        /*
        [HarmonyPatch(typeof(SectionLayer_Gas))]
        [HarmonyPatch("DensityAt")]
        public static class Patches_DensityAt
        {
            private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {

                MethodInfo target = AccessTools.Method(typeof(SectionLayer_Gas), "DensityAt");

                foreach (var c in instructions)
                {

                    if (c.opcode == OpCodes.Callvirt && c.operand as MethodInfo == target)
                    {

                        yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Patches_DensityAt), "DensityToGasDef"));

                    }
                    else yield return c;

                }

            }


            public static byte DensityToGasDef(ExtendedGasGrid __instance, ref int cellIndex, ref GasDef __result)
            {
                Log.Message("Changing density check to GasDef!");
                return __instance.DensityAt(cellIndex, __result);

            }

        }

        [HarmonyPatch(typeof(SectionLayer_Gas))]
        [HarmonyPatch(nameof(SectionLayer_Gas.ColorAt))]
        public static class Patches_ColorAt
        {
            private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {

                MethodInfo target = AccessTools.Method(typeof(UnityEngine.Color), "UnityEngine.Vector4");

                foreach (var c in instructions)
                {

                    if (c.opcode == OpCodes.Call && c.operand as MethodInfo == target)
                    {

                        yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Patches_ColorAt), "ReturnColors"));

                    }
                    else yield return c;

                }

            }

            public static Color ReturnColors(ExtendedGasGrid __instance, IntVec3 cell, ref Color __result, ref FloatRange ___AlphaRange, ref Color ___SmokeColor, ref Color ___ToxColor, ref Color ___RotColor, ref Color ___DeadlifeColor)
            {
                Log.Message("Returning proper colors to gas grid!");
                return __result = __instance.ColorAt(cell, ___AlphaRange, ___SmokeColor, ___ToxColor, ___RotColor, ___DeadlifeColor);

            }

        }
        */
    }
}
