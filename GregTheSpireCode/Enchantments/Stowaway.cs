using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Cards.Colorless;
using GregTheSpire.GregTheSpireCode.Character;
using GregTheSpire.GregTheSpireCode.Commands;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;


namespace GregTheSpire.GregTheSpireCode.Enchantments;


public sealed class Stowaway : GregTheSpireEnchantment
{
   public override bool HasExtraCardText => false;


   public override bool ShowAmount => false;


   protected override IEnumerable<IHoverTip> ExtraHoverTips =>
   [
       
   ];


   protected override IEnumerable<DynamicVar> CanonicalVars =>
   [
      
   ];


    public override async Task BeforeHandDrawLate(Player player, PlayerChoiceContext choiceContext, ICombatState combatState)
    {
       CardModel card = Card;
       if (player != card.Owner || card.Owner.PlayerCombatState.TurnNumber > 1)
           return;
       await CardPileCmd.Add(card, StashCardPile.StashPileType);
    }
}



