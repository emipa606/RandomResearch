using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;
using Verse;

namespace Random_Research.ResearchTreeSupport;

[StaticConstructorOnStartup]
public class PreventChoice
{
    private static FieldInfo Research;

    static PreventChoice()
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
        var type = AccessTools.TypeByName("FluffyResearchTree.ResearchNode") ??
                   AccessTools.TypeByName("ResearchPal.ResearchNode");
        Research = AccessTools.Field(type, "Research");
        var methodInfo = AccessTools.Method(type, "Draw");
        if (methodInfo != null)
        {
            new Harmony("Uuugggg.rimworld.Random_Research.ResearchTreeSupport").Patch(methodInfo, null, null,
                new HarmonyMethod(typeof(PreventChoice), "Transpiler"));
        }
    }

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var ButtonInvisibleInfo = AccessTools.Method(typeof(Widgets), "ButtonInvisible");
        var HideButtonInvisibleInfo = AccessTools.Method(typeof(PreventChoice), "HideButtonInvisible");
        foreach (var instruction in instructions)
        {
            if (instruction.Calls(ButtonInvisibleInfo))
            {
                yield return new CodeInstruction(OpCodes.Ldarg_0);
                yield return new CodeInstruction(OpCodes.Call, HideButtonInvisibleInfo);
            }
            else
            {
                yield return instruction;
            }
        }
    }

    public static bool HideButtonInvisible(Rect butRect, bool doMouseoverSound, object obj)
    {
        var researchProjectDef = Research.GetValue(obj) as ResearchProjectDef;
        if (researchProjectDef ==
            (ResearchProjectDef)RandomResearchMod.CurrentProjFieldInfo.GetValue(Find.ResearchManager))
        {
            researchProjectDef = null;
        }

        return BlindResearch.CanChangeTo(researchProjectDef) && Widgets.ButtonInvisible(butRect, doMouseoverSound);
    }
}