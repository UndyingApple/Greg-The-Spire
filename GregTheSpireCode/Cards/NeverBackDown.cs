using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class NeverBackDown() : GregTheSpireCard(2,
    CardType.Attack, CardRarity.Basic,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<NeverBackDownPower>(1),
        new PowerVar<NeverBackDownUpgradedPower>(1)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (this.IsUpgraded)
        {
            await PowerCmd.Apply<NeverBackDownUpgradedPower>(choiceContext, Owner.Creature, DynamicVars.Power<NeverBackDownUpgradedPower>().BaseValue, Owner.Creature, this);
        }
        else
        {
            await PowerCmd.Apply<NeverBackDownPower>(choiceContext, Owner.Creature,
                DynamicVars.Power<NeverBackDownPower>().BaseValue, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);
}