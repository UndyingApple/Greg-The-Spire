using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Keywords;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class GregJump() : GregTheSpireCard(0,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromKeyword(GregTheSpireKeywords.Stash)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        foreach (CardModel card in StashCardPile.StashPileType.GetPile(Owner).Cards.ToList<CardModel>())
        {
            await CardPileCmd.Add(card, PileType.Hand);
        }
        if(IsUpgraded) await CardPileCmd.Draw(choiceContext, 1, this.Owner);
    }

    protected override void OnUpgrade()
    {

    }
}