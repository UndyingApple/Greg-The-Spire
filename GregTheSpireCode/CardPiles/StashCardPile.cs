using BaseLib.Abstracts;
using BaseLib.Patches.Content;
using Godot;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.CardPiles;

public class StashCardPile() : CustomPile(StashPileType) {
    [CustomEnum] public static PileType StashPileType;

    public override bool CardShouldBeVisible(CardModel card) => true;

    public override Vector2 GetTargetPosition(CardModel model, Vector2 size) {
        return new Vector2(75, 765); // Stash pile position
    }
}

public struct StashSelectorPrefs {
    public static LocString ToStashSelectionPrompt => new LocString("card_selection", "GREGTHESPIRE-TO_STASH");
    public static LocString FromStashSelectionPrompt => new LocString("card_selection", "GREGTHESPIRE-FROM_STASH");
}