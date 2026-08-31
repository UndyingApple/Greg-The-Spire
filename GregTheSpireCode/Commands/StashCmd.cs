using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Keywords;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Commands;

public static class StashCmd
{
    public static async Task StashAsync(PlayerChoiceContext choiceContext, Player player, int min, int max, AbstractModel source)
    {
        var stashedCards = (await CardSelectCmd.FromHand(choiceContext, player, 
            new CardSelectorPrefs(StashSelectorPrefs.ToStashSelectionPrompt, min, max),
            null,
            source)).ToList();

        var storage = player.Creature.GetPowerAmount<StoragePower>();
        int totalStashed;
        if (player.Creature.GetPowerAmount<BiteSizedPower>() == 1)
        {
            totalStashed = StashCardPile.StashPileType.GetPile(player).Cards.Where<CardModel>((Func<CardModel, bool>)(c =>
                !c.Keywords.Contains(GregTheSpireKeywords.Snack))).Count();
        }
        else
        {
            totalStashed = StashCardPile.StashPileType.GetPile(player).Cards.Count;
        }
        for (var i = 1; i <= stashedCards.Count; i++)
        {
            if (i + totalStashed <= storage)
            {
                await CardPileCmd.Add(stashedCards[i - 1], StashCardPile.StashPileType);
            }
            else
            {
                await CardCmd.Discard(choiceContext, stashedCards[i - 1]);
            }
        }
    }
}