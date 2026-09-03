using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Cards.Colorless;
using GregTheSpire.GregTheSpireCode.Keywords;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class CamembertWheel() : GregTheSpireCard(1,
    CardType.Attack, CardRarity.Rare,
    TargetType.AllEnemies)
{
    private int _currentDamage = 2;
    private int _increasedDamage;
    private bool hasPlayedCheeseThisTurn = false;
    
    [SavedProperty]
    public int CurrentDamage
    {
        get => this._currentDamage;
        set
        {
            this.AssertMutable();
            this._currentDamage = value;
            this.DynamicVars.Damage.BaseValue = (Decimal) this._currentDamage;
        }
    }

    [SavedProperty]
    public int IncreasedDamage
    {
        get => this._increasedDamage;
        set
        {
            this.AssertMutable();
            this._increasedDamage = value;
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(2, ValueProp.Move),
        new IntVar("Increase", 1)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        GregTheSpireKeywords.Snack
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<Cheese>()
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        Decimal damage = Hook.ModifyDamage(this.RunState, this.CombatState, cardPlay.Target, this.Owner.Creature, DynamicVars.Damage.BaseValue, DynamicVars.Damage.Props, (CardModel) this, cardPlay, ModifyDamageHookType.All, CardPreviewMode.MultiCreatureTargeting, out IEnumerable<AbstractModel> _);
        await PowerCmd.Apply<CamembertWheelPower>(choiceContext, Owner.Creature, damage, Owner.Creature, this);
    }

    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card is Cheese && !hasPlayedCheeseThisTurn) {
            hasPlayedCheeseThisTurn = true;
            int intValue = DynamicVars["Increase"].IntValue;
            this.BuffFromPlay(intValue);
            if (this.DeckVersion is CamembertWheel deckVersion)
            {
                deckVersion.BuffFromPlay(intValue);
            }
        }
        return Task.CompletedTask;
    }

    public override Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side == CombatSide.Player)
        {
            hasPlayedCheeseThisTurn = false;
        }
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars["Increase"].UpgradeValueBy(1);
        this.UpdateDamage();
    }

    protected override void AfterDowngraded() => this.UpdateDamage();

    public void BuffFromPlay(int extraDamage)
    {
        this.IncreasedDamage += extraDamage;
        this.UpdateDamage();
    }

    private void UpdateDamage() => this.CurrentDamage = (int) DynamicVars.Damage.BaseValue + this.IncreasedDamage;
}