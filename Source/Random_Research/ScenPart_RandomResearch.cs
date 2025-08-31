using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace Random_Research;

public class ScenPart_RandomResearch : ScenPart
{
    private string blindBuf;
    public float blindThreshold;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref blindThreshold, "blindThreshold");
    }

    public override void DoEditInterface(Listing_ScenEdit listing)
    {
        var scenPartRect = listing.GetScenPartRect(this, RowHeight);
        var rect = scenPartRect.LeftHalf().Rounded();
        var rect2 = scenPartRect.RightHalf().Rounded();
        Text.Anchor = TextAnchor.MiddleRight;
        Widgets.Label(rect, "TD.BlindUntilProgess".Translate());
        Text.Anchor = TextAnchor.UpperLeft;
        Widgets.TextFieldPercent(rect2, ref blindThreshold, ref blindBuf, 0f, 100f);
    }

    public override string Summary(Scenario scen)
    {
        string text = "TD.RandomResearchDesc".Translate();
        if (blindThreshold > 0f)
        {
            text = $"{text} {string.Format("TD.ProjectHiddenUntilDesc".Translate(), blindThreshold)}";
        }

        return text;
    }

    public override void Randomize()
    {
        blindThreshold = GenMath.RoundedHundredth(Rand.Range(0f, 1f));
    }

    public override void Tick()
    {
        base.Tick();
        if ((ResearchProjectDef)RandomResearchMod.CurrentProjFieldInfo.GetValue(Find.ResearchManager) == null &&
            DefDatabase<ResearchProjectDef>.AllDefsListForReading.Where(x => x.CanStartNow)
                .TryRandomElement(out var result))
        {
            RandomResearchMod.CurrentProjFieldInfo.SetValue(Find.ResearchManager, result);
        }
    }
}