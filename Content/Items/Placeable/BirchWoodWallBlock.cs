using Arbour.Content.Walls;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable;

public class BirchWoodWallBlock : ModItem
{
	public override void SetDefaults() => Item.DefaultToPlacableWall((ushort)ModContent.WallType<BirchWoodWall>());

    public override void AddRecipes()
    {
        CreateRecipe(4).
            AddIngredient<BirchWoodBlock>(1).
            AddTile(TileID.WorkBenches).
            Register();
    }
}