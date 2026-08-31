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

public class MouseTrap() : GregTheSpireCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{

    public override bool GainsBlock => true;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BlockVar(7, ValueProp.Move),
        (DynamicVar)new PowerVar<CheeseNextTurnPower>(1)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);

        if (!play.Target.Monster.IntendsToAttack)
            return;
        CheeseNextTurnPower? cheeseNextTurnPower = await PowerCmd.Apply<CheeseNextTurnPower>(choiceContext,
            this.Owner.Creature, DynamicVars.Power<CheeseNextTurnPower>().BaseValue, this.Owner.Creature,
            (CardModel)this);
    }


    protected override void OnUpgrade()
    {
        DynamicVars.Power<CheeseNextTurnPower>().UpgradeValueBy(1);
    }
}