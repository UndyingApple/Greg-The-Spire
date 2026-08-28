using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class HumanMerryGoRound() : GregTheSpireCard(1,
    CardType.Attack, CardRarity.Basic,
    TargetType.RandomEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(1M, ValueProp.Move),  
        new RepeatVar(5)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        AttackCommand attackCommand = await DamageCmd.Attack(DynamicVars.Damage.BaseValue).WithHitCount(this.DynamicVars.Repeat.IntValue).FromCard((CardModel) this, play).TargetingRandomOpponents(this.CombatState).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        
        var confidenceAmount = this.Owner.Creature.GetPowerAmount<ConfidencePower>();
        if (confidenceAmount != null)
        {
            ConfidencePower confidencePower = await PowerCmd.Apply<ConfidencePower>(choiceContext, Owner.Creature, -confidenceAmount, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade() => this.DynamicVars.Repeat.UpgradeValueBy(1);
  
  
  
}