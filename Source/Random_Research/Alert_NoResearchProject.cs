using RimWorld;
using Verse;

namespace Random_Research;

public class Alert_NoResearchProject : Alert
{
    public Alert_NoResearchProject()
    {
        defaultLabel = "TD.NeedResearchEquipment".Translate();
        defaultExplanation = "TD.NeedResearchEquipmentDesc".Translate();
    }

    public override AlertReport GetReport()
    {
        if (Find.AnyPlayerHomeMap == null ||
            RandomResearchMod.CurrentProjFieldInfo.GetValue(Find.ResearchManager) != null || !BlindResearch.Active())
        {
            return false;
        }

        foreach (var map in Find.Maps)
        {
            if (map.IsPlayerHome && map.listerBuildings.ColonistsHaveResearchBench())
            {
                return !Find.ResearchManager.AnyProjectIsAvailable &&
                       DefDatabase<ResearchProjectDef>.AllDefsListForReading.Any(x => !x.IsFinished);
            }
        }

        return false;
    }
}