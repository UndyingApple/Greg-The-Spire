using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class RatForm() : GregTheSpireCard(3,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "PowerUp", this.Owner.Character.PowerUpAnimDelay);
        StoragePower storagePower = await PowerCmd.Apply<StoragePower>(choiceContext, this.Owner.Creature, 3, this.Owner.Creature, (CardModel) this);
        RatFormPower ratFormPower = await PowerCmd.Apply<RatFormPower>(choiceContext, this.Owner.Creature, 1, this.Owner.Creature, (CardModel) this);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}