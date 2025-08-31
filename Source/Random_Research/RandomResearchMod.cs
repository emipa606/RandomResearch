using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Random_Research;

public class RandomResearchMod : Mod
{
    public static readonly FieldInfo CurrentProjFieldInfo = AccessTools.Field(typeof(ResearchManager), "currentProj");

    public RandomResearchMod(ModContentPack content)
        : base(content)
    {
        new Harmony("Uuugggg.rimworld.Random_Research.main").PatchAll(Assembly.GetExecutingAssembly());
    }
}