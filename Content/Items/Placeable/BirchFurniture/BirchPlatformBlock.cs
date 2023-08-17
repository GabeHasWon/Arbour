using Arbour.Content.Tiles.Blocks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable.BirchFurniture;

public class BirchPlatformBlock : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 100;
    public override void SetDefaults() => Item.DefaultToPlaceableTile(ModContent.TileType<BirchPlatformTile>());

    public override void AddRecipes()
    {
        CreateRecipe(2).
            AddIngredient<BirchWoodBlock>(1).
            Register();
    }
}