using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Random_Research;

public static class BlindResearch
{
    private static readonly AccessTools.FieldRef<MainTabWindow_Research, ResearchProjectDef> selectedProject =
        AccessTools.FieldRefAccess<MainTabWindow_Research, ResearchProjectDef>("selectedProject");

    public static bool Active()
    {
        return Find.Scenario.AllParts.Any(p => p is ScenPart_RandomResearch);
    }

    public static bool CanSeeCurrent()
    {
        var currentProject = (ResearchProjectDef)RandomResearchMod.CurrentProjFieldInfo.GetValue(Find.ResearchManager);

        return CanSeeProgress(currentProject?.ProgressPercent ?? 0f);
    }

    public static bool CanSeeProgress(float progress)
    {
        if (Active() && !DebugSettings.godMode)
        {
            return progress >= Find.Scenario.AllParts.Sum(p =>
                p is not ScenPart_RandomResearch scenPart_RandomResearch ? 0f : scenPart_RandomResearch.blindThreshold);
        }

        return true;
    }

    public static bool CanChangeTo(ResearchProjectDef toThis = null)
    {
        if (Active() && !CanSeeProgress(toThis?.ProgressPercent ?? 0f))
        {
            return DebugSettings.godMode;
        }

        return true;
    }

    public static ResearchProjectDef SelectedResearch()
    {
        if (Find.MainTabsRoot?.OpenTab?.TabWindow is MainTabWindow_Research instance)
        {
            return selectedProject(instance);
        }

        return null;
    }
}