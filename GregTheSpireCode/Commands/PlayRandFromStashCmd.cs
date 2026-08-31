using GregTheSpire.GregTheSpireCode.ui;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Commands;

public static class PlayRandFromStashCmd
{
    
    public static async Task PlayRandFromStashCmdAsync(PlayerChoiceContext choiceContext, Player player, int amount)
    {/*
        if (amount <= 0) return;
        
        Decimal num = await CreatureCmd.GainBlock(this.Owner.Creature, this.DynamicVars.Block, cardPlay);
        if (this.IsUpgraded)
        { 
            CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1);
            CardModel card = (await CardSelectCmd.FromHand(choiceContext, this.Owner, prefs, (Func<CardModel, bool>) null, (AbstractModel) this)).FirstOrDefault<CardModel>();
            if (card == null)
                return;
            CardPileAddResult? nullable = await CardCmd.Exhaust(choiceContext, card);
        }
        else
        {
            CardModel card = this.Owner.RunState.Rng.CombatCardSelection.NextItem<CardModel>((IEnumerable<CardModel>) PileType.Hand.GetPile(this.Owner).Cards);
            if (card == null) 
                return;
            CardPileAddResult? nullable = await CardCmd.Exhaust(choiceContext, card); 
        }
        */
    }
    
}