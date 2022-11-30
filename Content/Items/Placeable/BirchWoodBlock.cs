using Arbour.Content.Tiles.Blocks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable
{
    public class BirchWoodBlock : ModItem
	{
		public override void SetDefaults() => Item.DefaultToPlaceableTile(ModContent.TileType<BirchWoodTile>());

        public override void AddRecipes()
        {
            CreateRecipe(1).
                AddIngredient<BirchPlatformBlock>(2).
                Register();

            CreateRecipe(1).
                AddIngredient<BirchWoodWallBlock>(4).
                AddTile(TileID.WorkBenches).
                Register();

            CreateRecipe(1).
                AddIngredient<ArborLeafWallBlock>(4).
                AddTile(TileID.LivingLoom).
                Register();
        }
    }
}