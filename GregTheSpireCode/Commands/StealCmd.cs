using GregTheSpire.GregTheSpireCode.CardPiles;
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
        
        //variable that represents the card on top of the deck
        CardModel topCard = PileType.Draw.GetPile(player).Cards.FirstOrDefault<CardModel>();
        //variable that represents the owner of the card
        //card_initial.Owner = player;

        for (int i = 0; i < amount; i++)
        {
            if (!player.PlayerCombatState.HasEnoughResourcesFor(topCard, out UnplayableReason reason)) continue;
            if (reason == UnplayableReason.None)
            {
                await CardPileCmd.AutoPlayFromDrawPile(choiceContext, player, 1, CardPilePosition.Top, false);
                if (topCard.Owner.Creature.Player == player && topCard.Type != CardType.Power && !topCard.IsDupe)
                {
                    location.pileType = PileType.Discard;
                    location.position = CardPilePosition.Top;
                    location.player = this.Owner.Player;
                }
                /*Creature? target,
                    AutoPlayType type = AutoPlayType.Default,
                    bool skipXCapture = false,
                    bool skipCardPileVisuals = false)
                    */
            }
            /*implement this once the stash has a max size limit
                 * else if ( )
                 */
            else
            {
                return;
            }
        }
    }
}