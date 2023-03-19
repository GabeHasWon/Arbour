using Arbour.Content.Tiles.Multitiles.Furniture;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable.BirchFurniture;

public class BirchWorkbenchBlock : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<BirchWorkbench>());
        SacrificeTotal = 100;
    }

    public override void AddRecipes()
    {
        CreateRecipe().
            AddIngredient<BirchWoodBlock>(10).
            Register();
    }
}