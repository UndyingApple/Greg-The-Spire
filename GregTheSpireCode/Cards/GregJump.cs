using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class GregJump() : GregTheSpireCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        (DynamicVar)new PowerVar<ConfidencePower>(1),

        new BlockVar(4, ValueProp.Move)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);

        ConfidencePower confidencePower = await PowerCmd.Apply<ConfidencePower>(choiceContext, Owner.Creature, DynamicVars.Power<ConfidencePower>().BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3);
    }
}