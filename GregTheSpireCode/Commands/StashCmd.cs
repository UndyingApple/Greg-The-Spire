using GregTheSpire.GregTheSpireCode.CardPiles;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Commands;

public static class StashCmd
{
    public static async Task StashAsync(PlayerChoiceContext choiceContext, Player player, int min, int max, CardModel card_initial)
    {
        var stashedCards = (await CardSelectCmd.FromHand(choiceContext, card_initial.Owner, 
            new CardSelectorPrefs(StashSelectorPrefs.ToStashSelectionPrompt, min, max),
            null,
            card_initial)).ToList();
        
        await CardPileCmd.Add(stashedCards, StashCardPile.StashPileType);
    }
}