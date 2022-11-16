using Arbour.Content.Tiles.Blocks;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable;

public class BirchPlatformBlock : ModItem
{
	public override void SetDefaults() => Item.DefaultToPlaceableTile(ModContent.TileType<BirchPlatformTile>());
}