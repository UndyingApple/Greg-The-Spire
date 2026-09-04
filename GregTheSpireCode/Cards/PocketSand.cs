using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Commands;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class PocketSand() : GregTheSpireCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<WeakPower>(1)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<WeakPower>(choiceContext, play.Target, this.DynamicVars.Weak.BaseValue, this.Owner.Creature, (CardModel) this);

        if (Stolen.IsStolen.Get(this))
        {
            await StealCmd.StealAsync(choiceContext, this.Owner, 2);
        }
        else await StealCmd.StealAsync(choiceContext, this.Owner, 1);




    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Weak.UpgradeValueBy(1);
    }
}