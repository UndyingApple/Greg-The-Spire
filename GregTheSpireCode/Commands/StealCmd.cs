using BaseLib.Utils;
using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Commands;

public static class StealCmd
{
    public static async Task StealAsync(PlayerChoiceContext choiceContext, Player player, int amount)
    {
        //ends early if the draw pile is empty
        if (PileType.Draw.GetPile(player).Cards.FirstOrDefault<CardModel>() == null)
        {
            return;
        }

        //Checks if can steal this turn
        if (player.Creature.GetPowerAmount<NoStealThisTurn>() == 1)
        {
            await PowerCmd.Apply<NoStealThisTurn>(choiceContext, player.Creature, 1, player.Creature,
            null);
            return;
        }
    //variable that represents the owner of the card
    //card_initial.Owner = player;

    for (int i = 0; i < amount; i++)
        {
            //variable that represents the card on top of the deck
            CardModel topCard = PileType.Draw.GetPile(player).Cards.FirstOrDefault<CardModel>();
            if (!player.PlayerCombatState.HasEnoughResourcesFor(topCard, out UnplayableReason reason))
            {
                Stolen.IsStolen.Set(topCard, true);
                await StashCmd.StashAsync(choiceContext, player, topCard);
                Stolen.IsStolen.Set(topCard, false);
            }
            if (reason == UnplayableReason.None)
            {
                Stolen.IsStolen.Set(topCard, true);
                await CardPileCmd.AutoPlayFromDrawPile(choiceContext, player, 1, CardPilePosition.Top, false);
                Stolen.IsStolen.Set(topCard, false);
                /*Creature? target,
                    AutoPlayType type = AutoPlayType.Default,
                    bool skipXCapture = false,
                    bool skipCardPileVisuals = false)
                    */
            }
            /*implement this once the stash has a max size limit
                 * else if ( )
                 */
        }
    }
}

public class Stolen
{
    public static readonly SpireField<CardModel, bool> IsStolen = new(() => false);
}