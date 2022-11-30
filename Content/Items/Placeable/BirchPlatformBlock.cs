using Arbour.Content.Tiles.Blocks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable;

public class BirchPlatformBlock : ModItem
{
	public override void SetDefaults() => Item.DefaultToPlaceableTile(ModContent.TileType<BirchPlatformTile>());

    public override void AddRecipes()
    {
        CreateRecipe(2).
            AddIngredient<BirchWoodBlock>(1).
            Register();
    }
}