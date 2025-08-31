using HarmonyLib;
using RimWorld;

namespace Random_Research;

[HarmonyPatch(typeof(ResearchManager), nameof(ResearchManager.FinishProject))]
public static class ResearchManager_FinishProject
{
    public static void Prefix()
    {
        ResearchManager_ResearchPerformed.RemoveLetter();
    }
}