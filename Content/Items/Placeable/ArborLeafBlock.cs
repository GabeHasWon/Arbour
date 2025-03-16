using Arbour.Content.Tiles.Blocks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable;

public class ArborLeafBlock : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 100;
    public override void SetDefaults() => Item.DefaultToPlaceableTile(ModContent.TileType<ArborLeaf>());

    public override void AddRecipes() => CreateRecipe(5).
            AddIngredient<BirchWoodBlock>(2).
            AddTile(TileID.WorkBenches).
            Register();
}