using GregTheSpire.GregTheSpireCode.Keywords;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Powers;

public class SharingPower() : GregTheSpirePower
{
    private bool _isAddingSnack;

    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    private bool IsAddingSnack
    {
        get => this._isAddingSnack;
        set
        {
            this.AssertMutable();
            this._isAddingSnack = value;
        }
    }

    public override async Task AfterCardGeneratedForCombat(CardModel card, Player? creator)
    {
        if (creator == null || creator.Creature != this.Applier || !card.Keywords.Contains(GregTheSpireKeywords.Snack) || this.IsAddingSnack)
            return;
        this.IsAddingSnack = true;
        this.Flash();
        CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(card.CreateCloneForPlayer(this.Owner.Player), PileType.Draw, this.Owner.Player, CardPilePosition.Random));
        this.IsAddingSnack = false;
    }
}
