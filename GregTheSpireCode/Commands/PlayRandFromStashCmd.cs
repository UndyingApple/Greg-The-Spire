using GregTheSpire.GregTheSpireCode.CardPiles;
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
    
    public static async Task PlayRandFromStashCmdAsync(PlayerChoiceContext choiceContext, Player player)
    {
        CardModel card = player.RunState.Rng.CombatCardSelection.NextItem<CardModel>((IEnumerable<CardModel>) StashCardPile.StashPileType.GetPile(player).Cards);
        if (card == null) 
            return;
        await CardCmd.AutoPlay(choiceContext, card, null);
    }
}