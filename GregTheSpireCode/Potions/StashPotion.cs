using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace GregTheSpire.GregTheSpireCode.Potions;
using BaseLib.Utils;
using Godot;
using GregTheSpire.GregTheSpireCode.Character;
using GregTheSpire.GregTheSpireCode.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.Rooms;




[Pool(typeof(GregTheSpirePotionPool))]

public sealed class StashPotion : GregTheSpirePotion
{
    public override PotionRarity Rarity => PotionRarity.Rare;

    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AnyPlayer;
    

    //no work still, idk
    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        GregTheSpirePotion.AssertValidForTargetedPotion(target);
        NCombatRoom.Instance?.PlaySplashVfx(target, new Color("45e6d0"));
        if (target.Player != null) await StashCmd.StashAsync(choiceContext, target.Player, 0, (target.GetPowerAmount<StoragePower>()), this);
    }
}