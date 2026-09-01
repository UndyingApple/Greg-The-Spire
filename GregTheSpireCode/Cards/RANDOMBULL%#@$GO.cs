using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Commands;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class RANDOMBULL____GO() : GregTheSpireCard(0,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override bool HasEnergyCostX => true;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        (DynamicVar) new EnergyVar(2)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int count = this.ResolveEnergyXValue();
        await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
        if (this.IsUpgraded)
            ++count;
        await StealCmd.StealAsync(choiceContext, this.Owner, count);
    }

    protected override void OnUpgrade()
    {

    }
}