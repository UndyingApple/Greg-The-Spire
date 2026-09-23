using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class Swallow() : GregTheSpireCard(2,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(12, ValueProp.Move),
        new PowerVar<SwallowPower>(4)
    ];

    public override bool GainsBlock => true;
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Decimal amount = await CreatureCmd.GainBlock(this.Owner.Creature, this.DynamicVars.Block, play);
        await PowerCmd.Apply<SwallowPower>(choiceContext, this.Owner.Creature, DynamicVars.Power<SwallowPower>().BaseValue, this.Owner.Creature, (CardModel) this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(4);
        DynamicVars.Power<SwallowPower>().UpgradeValueBy(2);
    }
}