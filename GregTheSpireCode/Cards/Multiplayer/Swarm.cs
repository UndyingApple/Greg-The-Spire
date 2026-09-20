using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Enchantments;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Cards.Multiplayer;

public class Swarm() : GregTheSpireCard(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AnyAlly)
{
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => HoverTipFactory.FromEnchantment<Infested>(1);

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        foreach (CardModel card in PileType.Hand.GetPile(play.Target.Player).Cards
                     .Where<CardModel>((Func<CardModel, bool>)(c =>
                         c.Enchantment == null && !c.Keywords.Contains(CardKeyword.Unplayable))).ToList<CardModel>())
        {
            CardCmd.Enchant<Infested>(card, 1);
        }
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}