using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace Random_Research;

[HarmonyPatch(typeof(MainTabWindow_Research), "DrawProjectProgress")]
internal static class MainTabWindow_Research_DrawProjectProgress
{
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var LabelInfoString = AccessTools.Method(typeof(Widgets), nameof(Widgets.Label), [
            typeof(Rect),
            typeof(string)
        ]);
        var LabelInfoTaggedString = AccessTools.Method(typeof(Widgets), nameof(Widgets.Label), [
            typeof(Rect),
            typeof(TaggedString)
        ]);
        var HideLabelInfoString =
            AccessTools.Method(typeof(MainTabWindow_Research_DrawProjectProgress), nameof(LabelString));
        var HideLabelInfoTaggedString =
            AccessTools.Method(typeof(MainTabWindow_Research_DrawProjectProgress), nameof(LabelTaggedString));

        var FillableBarInfo = AccessTools.Method(typeof(Widgets), nameof(Widgets.FillableBar), [
            typeof(Rect),
            typeof(float),
            typeof(Texture2D),
            typeof(Texture2D),
            typeof(bool)
        ]);
        var HideFillableBarInfo =
            AccessTools.Method(typeof(MainTabWindow_Research_DrawProjectProgress), nameof(HideFillableBar));
        foreach (var i in instructions)
        {
            if (i.Calls(LabelInfoString))
            {
                yield return new CodeInstruction(OpCodes.Call, HideLabelInfoString);
            }
            else if (i.Calls(LabelInfoTaggedString))
            {
                yield return new CodeInstruction(OpCodes.Call, HideLabelInfoTaggedString);
            }
            else
            {
                yield return i;
            }

            if (i.Calls(FillableBarInfo) || i.Calls(HideFillableBarInfo))
            {
                yield return new CodeInstruction(OpCodes.Call,
                    AccessTools.Method(typeof(MainTabWindow_Research_DrawProjectProgress), nameof(DrawCancelButton)));
            }
        }
    }

    public static Rect HideFillableBar(Rect rect, float fillPercent, Texture2D fillTex, Texture2D bgTex, bool doBorder)
    {
        if (!BlindResearch.CanSeeProgress(fillPercent))
        {
            fillPercent = 0f;
        }

        return Widgets.FillableBar(rect, fillPercent, fillTex, bgTex, doBorder);
    }

    public static Rect DrawCancelButton(Rect rect)
    {
        if (!BlindResearch.Active() || !BlindResearch.CanSeeCurrent())
        {
            return rect;
        }

        var butRect = rect.ContractedBy(2f);
        butRect.width = butRect.height;
        if (Widgets.ButtonImage(butRect, ContentFinder<Texture2D>.Get("UI/Designators/Cancel")))
        {
            RandomResearchMod.CurrentProjFieldInfo.SetValue(Find.ResearchManager, null);
        }

        return rect;
    }

    public static void LabelString(Rect rect, string str)
    {
        if (BlindResearch.CanSeeCurrent())
        {
            Widgets.Label(rect, str);
        }
    }

    public static void LabelTaggedString(Rect rect, TaggedString str)
    {
        if (BlindResearch.CanSeeCurrent())
        {
            Widgets.Label(rect, str);
        }
    }
}