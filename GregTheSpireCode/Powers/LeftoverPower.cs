using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Enchantments;
using GregTheSpire.GregTheSpireCode.Keywords;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Powers;

public class LeftoverPower() : GregTheSpirePower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    
    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        
        if (!cardPlay.Card.Keywords.Contains(GregTheSpireKeywords.Snack) || cardPlay.Card.Owner.Creature != this.Owner)
            return;
        for (int i = 0; i < this.Amount; ++i)
        {
            this.Flash();
            int totalStashed;
            if (Owner.GetPowerAmount<BiteSizedPower>() == 1)
            {
                totalStashed = StashCardPile.StashPileType.GetPile(Owner.Player).Cards.Where<CardModel>((Func<CardModel, bool>)(c =>
                    !c.Keywords.Contains(GregTheSpireKeywords.Snack) || (c.Enchantment is not Stowaway) )).Count();
            }
            else
            {
                totalStashed = StashCardPile.StashPileType.GetPile(Owner.Player).Cards.Where<CardModel>((Func<CardModel, bool>)(c => c.Enchantment is not Stowaway)).Count();
            }

            if (totalStashed >= Owner.GetPowerAmount<StoragePower>()) return;
            CardCmd.PreviewCardPileAdd(
                await CardPileCmd.AddGeneratedCardToCombat(cardPlay.Card.CreateClone(), StashCardPile.StashPileType,
                    cardPlay.Card.Owner), 0.2f);
        }
    }
    
}
