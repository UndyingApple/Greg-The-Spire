using BaseLib.Abstracts;
using GregTheSpire.GregTheSpireCode.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Singletons;

public class StealExhaust() : CustomSingletonModel(HookType.Combat)
{
    public override CardLocation ModifyCardPlayResultLocation(CardModel card, bool isAutoPlay, ResourceInfo resources,
        CardLocation location)
    {
        if (card.Type == CardType.Power || card.IsDupe || !Stolen.IsStolen.Get(card))
            return location;
        location.pileType = PileType.Discard;
        location.position = CardPilePosition.Top;
        return location;
    }
}