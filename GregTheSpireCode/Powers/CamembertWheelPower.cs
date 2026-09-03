using Godot;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.TestSupport;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Powers;

public class CamembertWheelPower() : GregTheSpirePower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override PowerInstanceType InstanceType =>
        PowerInstanceType.Instanced;
    
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != this.Owner.Player)
            ;
        else
        {
            this.Flash();
            if (TestMode.IsOn)
            {
                IEnumerable<DamageResult> damageResults = await this.DoDamage(choiceContext, (IEnumerable<Creature>) this.CombatState.HittableEnemies);
            }
            else
            {
                List<Task> damageTasks = new List<Task>();
                NRollingBoulderVfx source = NRollingBoulderVfx.Create((IEnumerable<Creature>) this.CombatState.HittableEnemies, (Decimal) this.Amount);
                // ISSUE: object of a compiler-generated type is created
                long num = (long) source.Connect(NRollingBoulderVfx.SignalName.HitCreature, Callable.From<NCreature>((Action<NCreature>) (c => damageTasks.Add((Task) this.DoDamage(choiceContext, [c.Entity])))));
                SignalAwaiter signal = source.ToSignal((GodotObject) source, NRollingBoulderVfx.SignalName.Finished);
                NCombatRoom.Instance?.CombatVfxContainer.CallDeferred(Node.MethodName.AddChild, (Variant) (GodotObject) source);
                Variant[] variantArray = await signal;
                await Task.WhenAll((IEnumerable<Task>) damageTasks);
            }
            await PowerCmd.Remove(this);
        }
    }

    private Task<IEnumerable<DamageResult>> DoDamage(
        PlayerChoiceContext choiceContext,
        IEnumerable<Creature> targets)
    {
        return CreatureCmd.Damage(choiceContext, targets, (Decimal) this.Amount, ValueProp.Unpowered, this.Owner);
    }
}