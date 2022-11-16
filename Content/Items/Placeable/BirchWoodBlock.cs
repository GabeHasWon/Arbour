using Arbour.Content.Tiles.Blocks;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable
{
    public class BirchWoodBlock : ModItem
	{
		public override void SetDefaults() => Item.DefaultToPlaceableTile(ModContent.TileType<BirchWoodTile>());
    }
}