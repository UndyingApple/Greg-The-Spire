
using MegaCrit.Sts2.Core.Commands;

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
        
        var flag = true;
        
        foreach (CardModel card in this.Owner.Player.PlayerCombatState.none.Cards)
                    
        await CardCmd.AutoPlay(choiceContext, card, (Creature)null, skipCardPileVisuals: !flag);
        flag = false;
    }

}

