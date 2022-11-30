using Arbour.Content.Tiles.Multitiles.Furniture;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable;

public class BirchWorkbenchBlock : ModItem
{
	public override void SetDefaults() => Item.DefaultToPlaceableTile(ModContent.TileType<BirchWorkbench>());

    public override void AddRecipes()
    {
        CreateRecipe().
            AddIngredient<BirchWoodBlock>(10).
            Register();
    }
}