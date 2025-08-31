using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace Random_Research;

[HarmonyPatch(typeof(MainTabWindow_Research), "DrawStartButton")]
internal class MainTabWindow_Research_DrawStartButton
{
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var HideButtonTextInfo =
            AccessTools.Method(typeof(MainTabWindow_Research_DrawStartButton), nameof(HideButtonText));
        var ButtonTextInfo = AccessTools.Method(typeof(Widgets), nameof(Widgets.ButtonText), [
            typeof(Rect),
            typeof(string),
            typeof(bool),
            typeof(bool),
            typeof(bool),
            typeof(TextAnchor?)
        ]);
        var inProgessStringInfo =
            AccessTools.Method(typeof(MainTabWindow_Research_DrawStartButton), nameof(InProgessString));
        foreach (var i in instructions)
        {
            if (i.Calls(ButtonTextInfo))
            {
                yield return new CodeInstruction(OpCodes.Call, HideButtonTextInfo);
            }
            else
            {
                yield return i;
            }

            if (i.opcode == OpCodes.Ldstr && ((string)i.operand).Equals("InProgress"))
            {
                yield return new CodeInstruction(OpCodes.Call, inProgessStringInfo);
            }
        }
    }

    public static bool HideButtonText(Rect rect, string label, bool drawBackground, bool doMouseoverSound, bool active,
        TextAnchor? overrideTextAnchor)
    {
        var result = false;
        if (BlindResearch.CanChangeTo(BlindResearch.SelectedResearch()))
        {
            result = Widgets.ButtonText(rect, label, drawBackground, doMouseoverSound, active, overrideTextAnchor);
        }
        else if (rect.height > 30f)
        {
            Widgets.DrawHighlight(rect);
        }

        return result;
    }

    public static string InProgessString(string inProgress)
    {
        return !BlindResearch.CanSeeCurrent() ? "" : inProgress;
    }
}