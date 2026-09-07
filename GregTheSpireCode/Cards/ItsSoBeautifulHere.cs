using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Commands;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
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
        
        List<CardModel> list2 = CardFactory.GetForCombat(this.Owner, this.Owner.Character.CardPool.GetUnlockedCards(
            this.Owner.UnlockState, this.Owner.RunState.CardMultiplayerConstraint), Owner.Creature.GetPowerAmount<ConfidencePower>(), this.Owner.RunState.Rng.CombatCardGeneration).ToList<CardModel>();

        if (this.IsUpgraded)
            CardCmd.Upgrade((IEnumerable<CardModel>) list2, CardPreviewStyle.None);
        
        for (int i = 0; i < Owner.Creature.GetPowerAmount<StoragePower>(); i++)
        {
            await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>)list2, StashCardPile.StashPileType, this.Owner);
        }

        //autoplays generated cards
        List<CardModel> list3 = StashCardPile.StashPileType.GetPile(Owner).Cards.ToList<CardModel>();
        int playCount = list3.Count;
        var flag = true;
        foreach (CardModel card in list3)
        {
            flag = true;
            await CardCmd.AutoPlay(choiceContext, card, null, skipCardPileVisuals: !flag);
            flag = false;
        }
    }
    
    protected override void OnUpgrade()
    {

    }
}