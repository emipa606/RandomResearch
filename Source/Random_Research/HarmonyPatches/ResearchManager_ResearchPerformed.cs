using HarmonyLib;
using RimWorld;
using Verse;

namespace Random_Research;

[HarmonyPatch(typeof(ResearchManager), nameof(ResearchManager.ResearchPerformed))]
internal static class ResearchManager_ResearchPerformed
{
    public const int msgCount = 3;
    private static int msgNum;

    private static Letter lastLetter;

    public static void Prefix(ResearchManager __instance, ref bool __state)
    {
        var currentResearchProject =
            (ResearchProjectDef)RandomResearchMod.CurrentProjFieldInfo.GetValue(__instance);

        __state = BlindResearch.CanSeeProgress(currentResearchProject?.ProgressPercent ?? 0f);
    }

    public static void RemoveLetter()
    {
        if (lastLetter == null)
        {
            return;
        }

        Find.LetterStack.RemoveLetter(lastLetter);
        lastLetter = null;
    }

    public static void Postfix(ResearchManager __instance, bool __state)
    {
        var currentResearchProject =
            (ResearchProjectDef)RandomResearchMod.CurrentProjFieldInfo.GetValue(__instance);
        if (currentResearchProject == null)
        {
            return;
        }

        if (__state || !BlindResearch.CanSeeProgress(currentResearchProject.ProgressPercent))
        {
            return;
        }

        RemoveLetter();
        var num = msgNum++;
        if (msgNum == 3)
        {
            msgNum = 0;
        }

        string text = "TD.ResearchKnown".Translate(currentResearchProject.LabelCap);
        lastLetter = LetterMaker.MakeLetter(
            text: (string)($"TD.ResearchKnownMsg{num}".Translate(currentResearchProject.LabelCap) + "\n\n" +
                           "TD.ResearchKnownDesc".Translate() + "\n\n" + currentResearchProject.LabelCap + ": " +
                           currentResearchProject.description), label: text, def: LetterDefOf.NeutralEvent);
        Find.LetterStack.ReceiveLetter(lastLetter);
    }
}