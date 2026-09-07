using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Commands;
using GregTheSpire.GregTheSpireCode.Enchantments;
using GregTheSpire.GregTheSpireCode.Keywords;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class QuickLick() : GregTheSpireCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromKeyword(GregTheSpireKeywords.Stash)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var storage = Owner.Creature.GetPowerAmount<StoragePower>();
        int totalStashed;
        if (Owner.Creature.GetPowerAmount<BiteSizedPower>() == 1)
        {
            totalStashed = StashCardPile.StashPileType.GetPile(Owner).Cards.Where<CardModel>((Func<CardModel, bool>)(c =>
                !c.Keywords.Contains(GregTheSpireKeywords.Snack) ||c.Enchantment is not Stowaway)).Count();
        }
        else
        {
            totalStashed = StashCardPile.StashPileType.GetPile(Owner).Cards.Where<CardModel>((Func<CardModel, bool>)(c => c.Enchantment is not Stowaway)).Count();
        }

        foreach (CardModel card in PileType.Draw.GetPile(this.Owner).Cards.ToList<CardModel>().UnstableShuffle<CardModel>(this.Owner.RunState.Rng.CombatCardSelection).Take<CardModel>(storage - totalStashed))
        {
            await StashCmd.StashAsync(choiceContext, Owner, card);
        }
    }

    protected override void OnUpgrade() => AddKeyword(CardKeyword.Innate);
}