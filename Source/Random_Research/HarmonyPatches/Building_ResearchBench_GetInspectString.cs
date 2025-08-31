using HarmonyLib;
using RimWorld;
using Verse;

namespace Random_Research;

[HarmonyPatch(typeof(Building_ResearchBench), nameof(Building_ResearchBench.GetInspectString))]
public static class Building_ResearchBench_GetInspectString
{
    public static void Postfix(ref string __result)
    {
        if (BlindResearch.CanSeeCurrent())
        {
            return;
        }

        __result = __result.Replace("CurrentProject".Translate(), "€").Split("€")[0].Trim();
    }
}