using HarmonyLib;
using RimWorld;
using Verse;

namespace Random_Research;

[HarmonyPatch(typeof(MainTabWindow_Research), nameof(MainTabWindow_Research.PreOpen))]
public static class MainTabWindow_Research_PreOpen
{
    private static readonly CurTabDel CurTabSet =
        AccessTools.MethodDelegate<CurTabDel>(
            AccessTools.Property(typeof(MainTabWindow_Research), nameof(MainTabWindow_Research.CurTab))
                .GetSetMethod(true), null, true);

    public static void Prefix()
    {
        ResearchManager_ResearchPerformed.RemoveLetter();
    }

    public static void Postfix(MainTabWindow_Research __instance, ref ResearchProjectDef ___selectedProject)
    {
        if (BlindResearch.CanSeeCurrent())
        {
            return;
        }

        ___selectedProject = null;
        CurTabSet(__instance, ResearchTabDefOf.Main);
    }

    private delegate void CurTabDel(MainTabWindow_Research tab, ResearchTabDef def);
}