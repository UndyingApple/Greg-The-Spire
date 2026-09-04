using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Commands;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class DeviousLittleRat() : GregTheSpireCard(2,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(7, ValueProp.Move)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var player = this.Owner;
        await StealCmd.StealAsync(choiceContext, this.Owner, 2);
        //play stash
        if (this.IsUpgraded)
        {
            await PlayFromStashCmd.PlayFromStashCmdAsync(choiceContext, this.Owner, 0, 999,(c => c.Type == CardType.Attack));
        }
        else
        {
            await PlayFromStashCmd.PlayFromStashCmdAsync(choiceContext, this.Owner, 0, 1,(c => c.Type == CardType.Attack));
        }
        await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue).FromCard((CardModel) this, play).Targeting(play.Target).Execute(choiceContext);

    }
  
    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(3);
    }
}