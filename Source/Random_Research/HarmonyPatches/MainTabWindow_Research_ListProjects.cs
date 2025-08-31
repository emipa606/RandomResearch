using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace Random_Research;

[HarmonyPatch(typeof(MainTabWindow_Research), "ListProjects")]
internal class MainTabWindow_Research_ListProjects
{
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var activeResearchColorInfo = AccessTools.Field(typeof(TexUI), nameof(TexUI.ActiveResearchColor));
        var activeProjectColorInfo = AccessTools.Field(typeof(MainTabWindow_Research), "ActiveProjectLabelColor");
        var borderResearchingColorInfo = AccessTools.Field(typeof(TexUI), nameof(TexUI.BorderResearchingColor));
        var progressPercentInfo =
            AccessTools.PropertyGetter(typeof(ResearchProjectDef), nameof(ResearchProjectDef.ProgressPercent));
        var replaceColorActiveResearchInfo =
            AccessTools.Method(typeof(MainTabWindow_Research_ListProjects), nameof(ReplaceColorActiveResearch));
        var replaceColorActiveProjectInfo =
            AccessTools.Method(typeof(MainTabWindow_Research_ListProjects), nameof(ReplaceColorActiveProject));
        var replaceColorBorderResearchingInfo =
            AccessTools.Method(typeof(MainTabWindow_Research_ListProjects), nameof(ReplaceColorBorderResearching));
        var progressPercentZeroerInfo =
            AccessTools.Method(typeof(MainTabWindow_Research_ListProjects), nameof(ZeroProgress));
        foreach (var i in instructions)
        {
            yield return i;
            if (i.LoadsField(activeResearchColorInfo))
            {
                yield return new CodeInstruction(OpCodes.Call, replaceColorActiveResearchInfo);
            }

            if (i.LoadsField(activeProjectColorInfo))
            {
                yield return new CodeInstruction(OpCodes.Call, replaceColorActiveProjectInfo);
            }

            if (i.LoadsField(borderResearchingColorInfo))
            {
                yield return new CodeInstruction(OpCodes.Call, replaceColorBorderResearchingInfo);
            }

            if (i.Calls(progressPercentInfo))
            {
                yield return new CodeInstruction(OpCodes.Call, progressPercentZeroerInfo);
            }
        }
    }

    public static Color ReplaceColorActiveResearch(Color activeTex)
    {
        return !BlindResearch.CanSeeCurrent() ? TexUI.AvailResearchColor : activeTex;
    }

    public static Color ReplaceColorActiveProject(Color activeTex)
    {
        return !BlindResearch.CanSeeCurrent() ? Widgets.NormalOptionColor : activeTex;
    }

    public static Color ReplaceColorBorderResearching(Color activeTex)
    {
        return !BlindResearch.CanSeeCurrent() ? TexUI.DefaultBorderResearchColor : activeTex;
    }

    public static float ZeroProgress(float progress)
    {
        return !BlindResearch.CanSeeProgress(progress) ? 0f : progress;
    }
}