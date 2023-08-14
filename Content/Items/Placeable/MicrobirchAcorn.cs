using Arbour.Content.Tiles.Multitiles;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable;

public class MicrobirchAcorn : ModItem
{
	public override void SetStaticDefaults() 
    {
        // Tooltip.SetDefault("Can only be planted on ceilings");
        Item.ResearchUnlockCount = 50;
    }

	public override void SetDefaults() => Item.DefaultToPlaceableTile(ModContent.TileType<MicrobirchSapling>());
}