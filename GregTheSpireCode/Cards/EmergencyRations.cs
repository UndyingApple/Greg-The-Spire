using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Cards.Colorless;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class EmergencyRations() : GregTheSpireCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        HoverTipFactory.FromCard<Cracker>(IsUpgraded)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        IEnumerable<CardModel> list = (IEnumerable<CardModel>) PileType.Hand.GetPile(this.Owner).Cards.ToList<CardModel>();
        int handSize = list.Count<CardModel>();
        await CardCmd.Discard(choiceContext, list);
        await Cmd.CustomScaledWait(0.0f, 0.25f);
        IEnumerable<CardModel> inHand = await Cracker.CreateInHand(this.Owner, handSize, this.CombatState);
        if (!this.IsUpgraded)
            return;
        foreach (CardModel card in inHand)
            CardCmd.Upgrade(card);
    }

    protected override void OnUpgrade()
    {

    }
}