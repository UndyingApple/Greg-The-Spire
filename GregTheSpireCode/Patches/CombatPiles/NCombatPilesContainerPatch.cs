using HarmonyLib;
using GregTheSpire.GregTheSpireCode.ui;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace GregTheSpire.GregTheSpireCode.Patches.CombatPiles;

[HarmonyPatch(typeof(NCombatPilesContainer))]
public static class NCombatPilesContainerPatch
{
    [HarmonyPatch("Enable")]
    [HarmonyPostfix]
    public static void EnablePostfix(NCombatPilesContainer __instance)
    {
        __instance.GetNodeOrNull<NStashPile>("_StashPile")?.Enable();
    }

    [HarmonyPatch("Disable")]
    [HarmonyPostfix]
    public static void DisablePostfix(NCombatPilesContainer __instance)
    {
        __instance.GetNodeOrNull<NStashPile>("_StashPile")?.Disable();
    }
}

[HarmonyPatch(typeof(NCombatUi), "Activate")]
public static class NCombatUiActivatePatch
{
    [HarmonyPostfix]
    public static void ActivatePostfix(NCombatUi __instance, CombatState state)
    {
        var container = __instance.GetNode<NCombatPilesContainer>("%CombatPileContainer");
        var stashPile = container.GetNodeOrNull<NStashPile>("_StashPile");
        var player = LocalContext.GetMe(state);
        if (player == null) return;

        stashPile?.Initialize(player);
    }
}