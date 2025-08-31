using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Verse;

namespace Random_Research.ResearchTreeSupport;

[StaticConstructorOnStartup]
public static class HideCurrent
{
    static HideCurrent()
    {
        try
        {
            Patch();
        }
        catch (Exception)
        {
            // ignored
        }
    }

    private static void Patch()
    {
        var methodInfo = AccessTools.Method(AccessTools.TypeByName("FluffyResearchTree.ResearchNode"), "Draw");
        if (methodInfo == null)
        {
            methodInfo = AccessTools.Method(AccessTools.TypeByName("ResearchPal.ResearchNode"), "Draw");
        }

        if (methodInfo != null)
        {
            new Harmony("Uuugggg.rimworld.Random_Research.ResearchTreeSupport").Patch(methodInfo, null, null,
                new HarmonyMethod(typeof(HideCurrent), "Transpiler"));
        }
    }

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var ProgressPercentInfo = AccessTools.Property(typeof(ResearchProjectDef), "ProgressPercent").GetGetMethod();
        var HideProgressPercentInfo = AccessTools.Method(typeof(HideCurrent), "HideProgressPercent");
        foreach (var i in instructions)
        {
            yield return i;
            if (i.Calls(ProgressPercentInfo))
            {
                yield return new CodeInstruction(OpCodes.Call, HideProgressPercentInfo);
            }
        }
    }

    public static float HideProgressPercent(float progress)
    {
        return !BlindResearch.CanSeeProgress(progress) ? 0f : progress;
    }
}