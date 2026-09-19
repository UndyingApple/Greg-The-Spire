using GregTheSpire.GregTheSpireCode.Enchantments;
using GregTheSpire.GregTheSpireCode.Keywords;
using GregTheSpire.GregTheSpireCode.Relics;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace GregTheSpire.GregTheSpireCode.Relics;


public class MoldySandwich() : GregTheSpireRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Rare;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        ..HoverTipFactory.FromEnchantment<Infested>(),
    ];
    
    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != this.Owner)
            return Task.CompletedTask;
        bool flag;
        switch (cardPlay.Card.Type)
        {
            case CardType.Attack:
            case CardType.Skill:
                flag = true;
                break;
            default:
                flag = false;
                break;
        }
        if (!flag || cardPlay.Card.Enchantment != null)
            return Task.CompletedTask;
        CardCmd.Enchant<Infested> (cardPlay.Card, 1);
        return Task.CompletedTask;
    }
}

    

