using Arbour.Content.Tiles.Multitiles;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable
{
    public class BirchWoodBlock : ModItem
	{
		public override void SetDefaults() => Item.DefaultToPlaceableTile(ModContent.TileType<MicrobirchSapling>());
    }
}