using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using LudeonTK;
using RimWorld;
using Verse;

namespace Random_Research;

public static class Debug_AddRandomResearch
{
    private static readonly FieldInfo partsInfo = AccessTools.Field(typeof(Scenario), "parts");

    [DebugAction("General", allowedGameStates = AllowedGameStates.Playing)]
    public static void MakeResearchRandom()
    {
        if (!BlindResearch.Active())
        {
            ((List<ScenPart>)partsInfo.GetValue(Find.Scenario)).Add(
                ScenarioMaker.MakeScenPart(ScenPartDefOf.RandomResearch));
        }
    }

    [DebugAction("General", allowedGameStates = AllowedGameStates.Playing)]
    public static void RemoveRandomResearch()
    {
        if (BlindResearch.Active())
        {
            ((List<ScenPart>)partsInfo.GetValue(Find.Scenario)).RemoveAll(p => p is ScenPart_RandomResearch);
        }
    }
}