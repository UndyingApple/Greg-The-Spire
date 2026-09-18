using Godot;
using MegaCrit.Sts2.addons.mega_text;
using MegaCrit.Sts2.Core.Assets;



namespace GregTheSpire.GregTheSpireCode.ui;

[GlobalClass]
public partial class GregTheSpireMegaLabel : MegaLabel
{
    public override void _Ready()
    {
        AddThemeFontOverride(ThemeConstants.Label.Font,
            PreloadManager.Cache.GetAsset<Font>("res://themes/kreon_bold_glyph_space_one.tres"));
        base._Ready();
    }
}
