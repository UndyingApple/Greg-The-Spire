using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Cards;
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

public class ItsSoBeautifulHere() : GregTheSpireCard(3,
    CardType.Skill, CardRarity.Basic,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    
    

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
            List<CardModel> list1 = StashCardPile.StashPileType.GetPile(this.Owner).Cards.ToList<CardModel>();
            int exhaustCount = list1.Count;
            foreach (CardModel card in list1)
            {
              CardPileAddResult? nullable = await CardCmd.Exhaust(choiceContext, card);
            }
            List<CardModel> list2 = CardFactory.GetForCombat(this.Owner, this.Owner.Character.CardPool.GetUnlockedCards(this.Owner.UnlockState, this.Owner.RunState.CardMultiplayerConstraint), exhaustCount, this.Owner.RunState.Rng.CombatCardGeneration).ToList<CardModel>();
            if (this.IsUpgraded)
              CardCmd.Upgrade((IEnumerable<CardModel>) list2, CardPreviewStyle.None);
            IReadOnlyList<CardPileAddResult> combat = await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>) list2, StashCardPile.StashPileType, this.Owner);
            var flag = true;
        
            foreach (CardModel card in StashCardPile.StashPileType.GetPile(Owner).Cards)
            {
                await CardCmd.AutoPlay(choiceContext, card, (Creature)null, skipCardPileVisuals: !flag);
                flag = false;
            }
    }
    public override void ModifyShuffleOrder(
        Player player,
        List<CardModel> cards,
        bool isInitialShuffle)
    {
        if (isInitialShuffle || !cards.Contains(this))
            return;
        cards.Remove(this);
        cards.Insert(999, this);
    }
    protected override void OnUpgrade()
    {

    }
}