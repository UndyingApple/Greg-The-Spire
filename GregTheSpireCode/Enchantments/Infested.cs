using GregTheSpire.GregTheSpireCode.Cards.Colorless;
using GregTheSpire.GregTheSpireCode.Character;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace GregTheSpire.GregTheSpireCode.Enchantments;

public sealed class Infested : GregTheSpireEnchantment
{
    public override bool HasExtraCardText => false;

    public override bool ShowAmount => false;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromCard<Fly>()
    ];

    public override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay? cardPlay)
    {
        await Fly.CreateInHand(Card.Owner, 1, Card.CombatState);
    }
}