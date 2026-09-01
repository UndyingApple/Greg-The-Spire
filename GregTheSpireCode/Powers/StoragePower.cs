using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Enchantments;
using GregTheSpire.GregTheSpireCode.Keywords;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Powers;

 

public class StoragePower() : GregTheSpirePower 
{
    public static event Action<PlayerCombatState, int>? StorageChanged;
    
    public override PowerType Type =>
        PowerType.Buff;

    protected override object InitInternalData() => new Data();

    private class Data
    {
        public int storage;
    }
    
    public override PowerStackType StackType =>
        PowerStackType.Counter;
    
    public override bool AllowNegative => false;

    public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        if (power is StoragePower)
        {
            StorageChanged?.Invoke(this.Owner.Player.PlayerCombatState, GetInternalData<Data>().storage + (int) amount);
            GetInternalData<Data>().storage += (int) amount;
        } else if (power is BiteSizedPower)
        {
            StorageChanged?.Invoke(this.Owner.Player.PlayerCombatState, GetInternalData<Data>().storage + StashCardPile.StashPileType.GetPile(this.Owner.Player).Cards.Where<CardModel>((Func<CardModel, bool>)(c =>
                c.Keywords.Contains(GregTheSpireKeywords.Snack) || (c.Enchantment is Stowaway) )).Count());
        }

        return Task.CompletedTask;
    }

    public async override Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel? clonedBy)
    {
        if (card.Owner == this.Owner.Player && (card.Keywords.Contains(GregTheSpireKeywords.Snack) || card.Enchantment is Stowaway)&& this.Owner.HasPower<BiteSizedPower>() && (card.Pile.Type == StashCardPile.StashPileType || oldPileType == StashCardPile.StashPileType))
        {
            StorageChanged?.Invoke(this.Owner.Player.PlayerCombatState, GetInternalData<Data>().storage + StashCardPile.StashPileType.GetPile(card.Owner).Cards.Where<CardModel>((Func<CardModel, bool>)(c =>
                c.Keywords.Contains(GregTheSpireKeywords.Snack) || (c.Enchantment is Stowaway))).Count());
        }
    }
}