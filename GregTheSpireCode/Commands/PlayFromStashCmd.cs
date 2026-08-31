using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.ui;
using GregTheSpire.GregTheSpireCode.CardPiles;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Commands;

public static class PlayFromStashCmd
{
    public static async Task PlayFromStashCmdAsync(PlayerChoiceContext choiceContext, Player player, int min, int max)
    {
        if (max <= 0) return;
        
        CardSelectorPrefs prefs = new CardSelectorPrefs(StashSelectorPrefs.FromStashSelectionPrompt, min, max)
        {
            RequireManualConfirmation = true
        };
        CardModel card = (await CardSelectCmd.FromCombatPile(choiceContext, StashCardPile.StashPileType.GetPile(player), player, prefs)).FirstOrDefault<CardModel>();
        if (card == null)
            return;
        await CardCmd.AutoPlay(choiceContext, card, (Creature) null);
    }
}