using Terraria.Localization;
using Terraria.ModLoader;

namespace Arbour.Content.Tiles.Multitiles.FurnitureHelpers;

public abstract class TileBlueprint : ModTile
{
    protected abstract StaticTileInfo StaticInfo { get; }
    protected abstract SpecificTileInfo SpecificInfo { get; }

    public sealed override void SetStaticDefaults()
    {
        Defaults();
        AddMapEntry(SpecificInfo.MapColor, Language.GetText(StaticInfo.MapKeyName));
        RegisterItemDrop(SpecificInfo.Drop);

        AdjTiles = StaticInfo.AdjTypes;
        DustType = SpecificInfo.DustType;
    }

    public abstract void Defaults();
}
