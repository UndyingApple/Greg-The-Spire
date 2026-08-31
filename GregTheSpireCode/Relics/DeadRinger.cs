using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.Powers;
using GregTheSpire.GregTheSpireCode.Relics;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Relics;

public class DeadRinger() : GregTheSpireRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Shop;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        (DynamicVar) new PowerVar<ConfidencePower>(2)
    ];
    

    public override async Task AfterEnergyResetLate(Player player)
    {
        if (this.Status == RelicStatus.Active)
        {
            await PowerCmd.Apply<ConfidencePower>((PlayerChoiceContext) null, Owner.Creature, DynamicVars.Power<ConfidencePower>().BaseValue, Owner.Creature, null, false);
        }
        
        this.Status = RelicStatus.Active;
    }


    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (!CombatManager.Instance.IsInProgress || target != this.Owner.Creature || dealer == this.Owner.Creature || result.UnblockedDamage <= 0)
            return;
        this.Status = RelicStatus.Disabled;

    }

    public override Task AfterCombatEnd(CombatRoom _)
    {
        this.Status = RelicStatus.Normal;
        return Task.CompletedTask;
    }
}