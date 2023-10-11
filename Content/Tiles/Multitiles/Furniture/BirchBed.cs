using Arbour.Content.Dusts;
using Arbour.Content.Items.Placeable.BirchFurniture;
using Arbour.Content.Tiles.Multitiles.FurnitureHelpers;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Arbour.Content.Tiles.Multitiles.Furniture;

public class BirchBed : BedTile
{
    protected override SpecificTileInfo SpecificInfo => new(ModContent.ItemType<BirchBedBlock>(), ModContent.DustType<BirchDust>(), new Color(124, 93, 68));
}
