using HarmonyLib;
using GregTheSpire.GregTheSpireCode.ui;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.GodotExtensions;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Runs;

namespace GregTheSpire.GregTheSpireCode.Patches.Input;

internal static class ModShortcutHelpers
{
    // TODO: move back to publicizer once reflib bug is resolved
    internal static readonly Action<NClickableControl> OnPressHandler =
        AccessTools.MethodDelegate<Action<NClickableControl>>(
            AccessTools.Method(typeof(NClickableControl), "OnPressHandler"));

    internal static readonly Action<NClickableControl> OnReleaseHandler =
        AccessTools.MethodDelegate<Action<NClickableControl>>(
            AccessTools.Method(typeof(NClickableControl), "OnReleaseHandler"));

    internal static NStashPile? GetStashPile()
    {
        return NCombatRoom.Instance?.Ui
            .GetNode<NCombatPilesContainer>("%CombatPileContainer")
            ?.GetNodeOrNull<NStashPile>("_StashPile");
    }

    /*
    internal static NAmmoButton? GetAmmoButton()
    {
        if (RunManager.Instance.State == null) return null;
        var creature = LocalContext.GetMe(RunManager.Instance.State.Players)?.Creature;
        if (creature == null) return null;
        return NCombatRoom.Instance?
            .GetCreatureNode(creature)
            ?.GetNodeOrNull<NAmmoButton>("AmmoButton");
    }
    */
}