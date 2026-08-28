using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using System;
using BaseLib.Commands;
using GregTheSpire.GregTheSpireCode.CardPiles;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class SnackOVision() : GregTheSpireCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        
        CardSelectorPrefs prefs = new CardSelectorPrefs(StashSelectorPrefs.FromStashSelectionPrompt, 1);
        
        
        
        CardModel card = (await MultiPileCardSelect.Select(choiceContext, this.Owner, prefs, (Func<CardModel, bool>?) null, new PileType[]
        {
            PileType.Discard,
            PileType.Draw,
            PileType.Hand,
            StashCardPile.StashPileType
        })).FirstOrDefault<CardModel>();

        if (card == null)
        {
            return;
        }
        CardPileAddResult cardPileAddResult = await CardPileCmd.Add(card, PileType.Draw,  CardPilePosition.Top);
        
        EnergyCost.AddThisCombat(1);
        
    }

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);
}