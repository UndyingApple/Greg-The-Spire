using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Keywords;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class LintBall() : GregTheSpireCard(100,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    private Decimal _extraDamageFromPlays;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(5, ValueProp.Move),
        new IntVar("Increase", 5)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromKeyword(GregTheSpireKeywords.Stash)
    ];
    
    private Decimal ExtraDamageFromPlays
    {
        get => this._extraDamageFromPlays;
        set
        {
            this.AssertMutable();
            this._extraDamageFromPlays = value;
        }
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target).Execute(choiceContext);
    }

    public override Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side == CombatSide.Enemy && Pile == StashCardPile.StashPileType.GetPile(Owner))
        {
            DamageVar damage = DynamicVars.Damage;
            damage.BaseValue += DynamicVars["Increase"].BaseValue;
            ExtraDamageFromPlays += DynamicVars["Increase"].BaseValue;
        }
        return Task.CompletedTask;
    }

    protected override void AfterDowngraded()
    {
        base.AfterDowngraded();
        DamageVar damage = DynamicVars.Damage;
        damage.BaseValue += ExtraDamageFromPlays;
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
        DynamicVars["Increase"].UpgradeValueBy(3);
    }
}