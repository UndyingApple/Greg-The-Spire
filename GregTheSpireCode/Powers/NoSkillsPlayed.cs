using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Afflictions;

namespace GregTheSpire.GregTheSpireCode.Powers;

public class NoSkillsPlayed() : GregTheSpirePower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Single;
    
    public override bool ShouldPlay(CardModel card, AutoPlayType _)
    {
        return card.Owner != this.Owner.Player || (card.Type != CardType.Skill);
    }

    
}