using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Random_Research.ResearchTreeSupport;

internal class CompletionLetter
{
    [HarmonyPatch(typeof(ResearchManager), "FinishProject")]
    [HarmonyBefore("Fluffy.ResearchTree", "rimworld.ResearchPal")]
    public class DoCompletionDialog
    {
        private static readonly Delegate doCompletionLetter;

        static DoCompletionDialog()
        {
            var methodInfo = AccessTools.Method(AccessTools.TypeByName("ResearchPal.Queue"), "DoCompletionLetter");
            if (methodInfo == null)
            {
                methodInfo = AccessTools.Method(AccessTools.TypeByName("FluffyResearchTree.Queue"),
                    "DoCompletionLetter");
            }

            if (!(methodInfo == null))
            {
                doCompletionLetter = methodInfo.CreateDelegate(typeof(Action<ResearchProjectDef, ResearchProjectDef>));
            }
        }

        private static void Prefix(ResearchProjectDef proj, bool doCompletionDialog)
        {
            if (!doCompletionDialog || !BlindResearch.Active())
            {
                return;
            }

            try
            {
                DoCompletionDialogEx(proj);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private static void DoCompletionDialogEx(ResearchProjectDef proj)
        {
            doCompletionLetter.DynamicInvoke(proj, null);
        }
    }
}