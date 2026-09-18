using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Cards.Multiplayer;

public class Sharing() : GregTheSpireCard(1,
    CardType.Power, CardRarity.Uncommon,
    TargetType.AnyAlly)
{

    public override CardMultiplayerConstraint MultiplayerConstraint
    {
        get => CardMultiplayerConstraint.MultiplayerOnly;
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull((object) cardPlay.Target, "cardPlay.Target");
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "PowerUp", this.Owner.Character.PowerUpAnimDelay);
        await CreatureCmd.TriggerAnim(cardPlay.Target, "PowerUp", cardPlay.Target.Player.Character.PowerUpAnimDelay);
        await PowerCmd.Apply<SharingPower>(choiceContext, cardPlay.Target, 1M, this.Owner.Creature, (CardModel) this);
    }

    protected override void OnUpgrade()
    {
        this.AddKeyword(CardKeyword.Innate);
    }
}