using System.Collections;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class HatForm() : GregTheSpireCard(3,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<HatFormPower>(1)
    ];


protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
{
    await CreatureCmd.TriggerAnim(this.Owner.Creature, "PowerUp", this.Owner.Character.PowerUpAnimDelay);
    HatFormPower hatFormPower = await PowerCmd.Apply<HatFormPower>(choiceContext, this.Owner.Creature, this.DynamicVars["HatFormPower"].BaseValue, this.Owner.Creature, (CardModel) this);
}
    

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);
}