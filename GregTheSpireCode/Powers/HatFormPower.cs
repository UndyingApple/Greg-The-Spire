using GregTheSpire.GregTheSpireCode.Commands;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace GregTheSpire.GregTheSpireCode.Powers;


public class HatFormPower() : GregTheSpirePower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext,
        ICombatState combatState)
    {
        if (player != this.Owner.Player)
            return;
        await StealCmd.StealAsync(choiceContext, player, Amount);
    }
}



