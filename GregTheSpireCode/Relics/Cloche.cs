using GregTheSpire.GregTheSpireCode.Keywords;
using GregTheSpire.GregTheSpireCode.Relics;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Relics;

public class Cloche() : GregTheSpireRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Rare;

    public override int ModifyCardPlayCount(CardModel card, Creature? target, int playCount)
    {
        return card.Owner != Owner || CombatManager.Instance.History.CardPlaysStarted.Count<CardPlayStartedEntry>((Func<CardPlayStartedEntry, bool>) (e => e.Actor == Owner.Creature && e.CardPlay.IsFirstInSeries && e.HappenedThisTurn(Owner.Creature.CombatState) && e.CardPlay.Card.Keywords.Contains(GregTheSpireKeywords.Snack))) >= 1 ? playCount : playCount + 1;
    }

    public override Task AfterModifyingCardPlayCount(CardModel card)
    {
        Flash();
        return Task.CompletedTask;
    }
}