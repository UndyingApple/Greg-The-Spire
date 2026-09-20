using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Cards.Colorless;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;


public class Kickflip() : GregTheSpireCard(2,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(8),
        new ExtraDamageVar(1),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((Func<CardModel, Creature, Decimal>) ((card, _) => (Decimal)card.Owner.Creature.GetPowerAmount<ConfidencePower>()))
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<ConfidencePower>()
    ];
    
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull((object) cardPlay.Target, "play.Target");
        AttackCommand attackCommand = await DamageCmd.Attack(this.DynamicVars.CalculatedDamage).FromCard((CardModel) this, cardPlay).Targeting(cardPlay.Target).WithHitFx("vfx/vfx_attack_slash", tmpSfx: "blunt_attack.mp3").Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(3);
        DynamicVars.ExtraDamage.UpgradeValueBy(1);
    }
}