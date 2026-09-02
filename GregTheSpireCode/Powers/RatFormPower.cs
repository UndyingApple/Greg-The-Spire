
using GregTheSpire.GregTheSpireCode.CardPiles;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;

using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Powers;


public class RatFormPower() : GregTheSpirePower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Single;
   

    public override async Task AfterAutoPrePlayPhaseEntered(
        PlayerChoiceContext choiceContext,
        Player player)
    {
        List<CardModel> list1 = StashCardPile.StashPileType.GetPile(player).Cards.ToList<CardModel>();
        int playCount = list1.Count;
        var flag = true;
        foreach (CardModel card in list1)
        {
            flag = true;
            await CardCmd.AutoPlay(choiceContext, card, null, skipCardPileVisuals: !flag);
            flag = false;
        }
           
        }
        
    }



