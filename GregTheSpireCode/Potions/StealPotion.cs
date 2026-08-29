using BaseLib.Utils;
using Godot;
using GregTheSpire.GregTheSpireCode.Character;
using GregTheSpire.GregTheSpireCode.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace GregTheSpire.GregTheSpireCode.Potions;


[Pool(typeof(GregTheSpirePotionPool))]

public sealed class StealPotion : GregTheSpirePotion
{
    public override PotionRarity Rarity => PotionRarity.Uncommon;

    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AnyPlayer;


    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        GregTheSpirePotion.AssertValidForTargetedPotion(target);
        NCombatRoom.Instance?.PlaySplashVfx(target, new Color("45e6d0"));
        if (target.Player != null) await StealCmd.StealAsync(choiceContext, target.Player, 2);
    }
}