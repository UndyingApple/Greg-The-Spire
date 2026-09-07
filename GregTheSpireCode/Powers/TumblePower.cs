using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace GregTheSpire.GregTheSpireCode.Powers;


public class TumblePower() : GregTheSpirePower
{
    public override PowerType Type =>
        PowerType.Buff;
 
    public override PowerStackType StackType =>
        PowerStackType.Counter;

    private bool shouldModify = false;

    public override Task BeforeCardAutoPlayed(CardModel card, Creature? target, AutoPlayType type)
    {
        if (card == null || card.Owner.Creature != this.Owner || card.Pile.Type != StashCardPile.StashPileType)
        {
            return Task.CompletedTask;
        }

        shouldModify = true;
        return Task.CompletedTask;
    }

    public override int ModifyCardPlayCount(CardModel card, Creature? target, int playCount)
    {
        bool shouldModify2 = shouldModify;
        shouldModify = false;
        return card.Owner.Creature != this.Owner || !shouldModify2 ? playCount : playCount + Amount;
    }
}