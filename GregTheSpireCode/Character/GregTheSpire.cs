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

    public override Color NameColor => new("00a86b");
    public override CharacterGender Gender => CharacterGender.Masculine;
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

    public override string CustomIconTexturePath => "character_icon_greg.png".GregPath();
    public override string CustomIconOutlineTexturePath => "character_icon_greg_outline.png".GregPath();
    public override string CustomCharacterSelectIconPath => "char_select_greg.png".CharacterUiPath();
    //public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_greg.png".GregPath();
    public override string CustomArmPointingTexturePath => "multiplayer_hand_greg_point.png".GregPath();
    public override string CustomArmRockTexturePath => "multiplayer_hand_greg_rock.png".GregPath();
    public override string CustomArmPaperTexturePath => "multiplayer_hand_greg_paper.png".GregPath();
    public override string CustomArmScissorsTexturePath => "multiplayer_hand_greg_scissors.png".GregPath();
    public override string CustomEnergyCounterPath => "res://GregTheSpire/scenes/energy_counter.tscn";

    //public override string CustomIconPath => "res://GregTheSpire/scenes/greg_icon.tscn"; needs to be made
    //public override string CustomCharacterSelectIconPath => "char_select_regent.png".GregPath();
}
