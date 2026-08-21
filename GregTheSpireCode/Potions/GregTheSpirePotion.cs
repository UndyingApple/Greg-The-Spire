using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using GregTheSpire.GregTheSpireCode.Character;
using GregTheSpire.GregTheSpireCode.Extensions;

namespace GregTheSpire.GregTheSpireCode.Potions;

[Pool(typeof(GregTheSpirePotionPool))]
public abstract class GregTheSpirePotion : CustomPotionModel
{
    public override string? CustomPackedImagePath =>
        $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionImagePath();

    public override string? CustomPackedOutlinePath =>
        $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionOutlineImagePath();
}