using System;
using HarmonyLib;
using UnityEngine;
using Verse;

namespace Random_Research.ResearchPal;

[StaticConstructorOnStartup]
public static class Cancel
{
    static Cancel()
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
        var methodInfo = AccessTools.Method(AccessTools.TypeByName("FluffyResearchTree.Queue"), "DrawQueue");
        if (methodInfo == null)
        {
            methodInfo = AccessTools.Method(AccessTools.TypeByName("ResearchPal.Queue"), "DrawQueue");
        }

        if (methodInfo != null)
        {
            new Harmony("Uuugggg.rimworld.Random_Research.ResearchPal").Patch(methodInfo, null,
                new HarmonyMethod(typeof(Cancel), "Postfix"));
        }
    }

    public static void Postfix(Rect canvas, bool interactible)
    {
        if (!BlindResearch.Active() || !BlindResearch.CanSeeCurrent())
        {
            return;
        }

        var butRect = canvas.ContractedBy(4f);
        butRect.width = butRect.height;
        if (Widgets.ButtonImage(butRect, ContentFinder<Texture2D>.Get("UI/Designators/Cancel")) && interactible)
        {
            RandomResearchMod.CurrentProjFieldInfo.SetValue(Find.ResearchManager, null);
        }
    }
}