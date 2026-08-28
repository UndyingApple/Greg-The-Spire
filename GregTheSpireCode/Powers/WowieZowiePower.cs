using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace GregTheSpire.GregTheSpireCode.Powers;


public class WowieZowiePower() : GregTheSpirePower
{
    public override PowerType Type =>
        PowerType.Debuff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    
    public override async Task AfterSideTurnStartLate(
        CombatSide side,
        IReadOnlyList<Creature> participants,
        ICombatState combatState)
    {
        if (side != CombatSide.Enemy)
            return;
        await PowerCmd.Decrement((PowerModel) this);
    }

    public override async Task AfterRemoved(Creature oldOwner)
    {
        if (oldOwner.IsDead)
            return;
        //The "ThrowingPlayerChoiceContext()" might not be the right thing to use here. Someone should look at this later.
        await PowerCmd.Apply<DoomPower>((PlayerChoiceContext) new ThrowingPlayerChoiceContext(), oldOwner, oldOwner.MaxHp, oldOwner, (CardModel)null);
    }



}