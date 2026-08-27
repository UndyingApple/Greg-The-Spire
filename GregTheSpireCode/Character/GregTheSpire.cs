using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using GregTheSpire.GregTheSpireCode.Extensions;
using Godot;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Relics;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;

namespace GregTheSpire.GregTheSpireCode.Character;

  
 
public class GregTheSpire : PlaceholderCharacterModel
{
    public const string CharacterId = "GregTheSpire";

    public static readonly Color Color = new("00a86b");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 68;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<StrikeGreg>(),
        ModelDb.Card<StrikeGreg>(),
        ModelDb.Card<StrikeGreg>(),
        ModelDb.Card<StrikeGreg>(),
        ModelDb.Card<StrikeGreg>(),
        ModelDb.Card<DefendGreg>(),
        ModelDb.Card<DefendGreg>(),
        ModelDb.Card<DefendGreg>(),
        ModelDb.Card<DefendGreg>(),
        ModelDb.Card<BagOfTricks>(),
        ModelDb.Card<Thieve>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<TrustyBackpack>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<GregTheSpireCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<GregTheSpireRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<GregTheSpirePotionPool>();

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }

    public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();
}