using BaseLib.Abstracts;
using GregTheSpire.GregTheSpireCode.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Rooms;

namespace GregTheSpire.GregTheSpireCode.Singletons;

public class ResetNumStolenCombat() : CustomSingletonModel(HookType.Combat)
{
    public override Task AfterCombatEnd(CombatRoom room)
    {
        foreach (Creature creature in room.Allies)
        {
            if (creature.Player != null)
            {
                Stolen.NumStolenCombat.Set(creature.Player, 0);
            }
        }
        return Task.CompletedTask;
    }
}