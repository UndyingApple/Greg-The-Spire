using BaseLib.Extensions;
using Godot;
using GregTheSpire.GregTheSpireCode.Enchantments;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes;


namespace GregTheSpire.GregTheSpireCode.Relics;

public class SturdyKnapsack() : GregTheSpireRelic
{
   public override RelicRarity Rarity =>
       RelicRarity.Ancient;


   protected override IEnumerable<IHoverTip> ExtraHoverTips =>
       [
           HoverTipFactory.FromPower<StoragePower>(),
           //HoverTipFactory.
       ];




       protected override IEnumerable<DynamicVar> CanonicalVars =>
       [
           new IntVar("Rounds", 1),
           new PowerVar<StoragePower>(5),
           new CardsVar(3)
       ];





/*
       public override bool TryModifyRestSiteOptions(Player player, ICollection<RestSiteOption> options)
       {
           if (player != this.Owner)
               return false;
           options.Add((RestSiteOption) new StoreRestSiteAction(player));
           return true;
       }

*/


public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, ICombatState combatState)

   {
              if (player != Owner || player.PlayerCombatState?.TurnNumber > 1) return;
              await PowerCmd.Apply<StoragePower>(
                  new ThrowingPlayerChoiceContext(), Owner.Creature,
                  DynamicVars.Power<StoragePower>().BaseValue, Owner.Creature, null);
    }
   


    public override bool TryModifyRestSiteOptions(Player player, ICollection<RestSiteOption> options)
    {
        if (player != this.Owner)
            return false;
        options.Add((RestSiteOption) new StoreRestSiteOption(player));
        return true;
    }
}





