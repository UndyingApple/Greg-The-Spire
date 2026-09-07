using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Commands;
using GregTheSpire.GregTheSpireCode.Keywords;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class ItsSoBeautifulHere() : GregTheSpireCard(2,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    
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
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);

        //exhausts cards in your stash (THIS PART WORKS)
        List<CardModel> list1 = StashCardPile.StashPileType.GetPile(this.Owner).Cards.ToList<CardModel>();
        foreach (CardModel card in list1)
        {
            CardPileAddResult? nullable = await CardCmd.Exhaust(choiceContext, card);
        }
        List<CardModel> list2 = CardFactory.GetForCombat(this.Owner, this.Owner.Character.CardPool.GetUnlockedCards(this.Owner.UnlockState, this.Owner.RunState.CardMultiplayerConstraint), Owner.Creature.GetPowerAmount<StoragePower>(), this.Owner.RunState.Rng.CombatCardGeneration).ToList<CardModel>();
            if (this.IsUpgraded)
              CardCmd.Upgrade((IEnumerable<CardModel>) list2, CardPreviewStyle.None);
            IReadOnlyList<CardPileAddResult> combat = await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>) list2, StashCardPile.StashPileType, this.Owner);
            
        await Cmd.Wait(0.1f);
        //autoplays generated cards
        List<CardModel> list3 = StashCardPile.StashPileType.GetPile(Owner).Cards.ToList<CardModel>();

        foreach (CardModel card in list3)
        {
            await CardCmd.AutoPlay(choiceContext, card, null);
        }
    }
    
    protected override void OnUpgrade()
    {

    }
}