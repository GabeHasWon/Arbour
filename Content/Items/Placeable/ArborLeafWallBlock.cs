using Arbour.Content.Walls;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable;

public class ArborLeafWallBlock : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 400;
    public override void SetDefaults() => Item.DefaultToPlaceableWall((ushort)ModContent.WallType<ArborLeafWall>());

    public override void AddRecipes()
    {
        CreateRecipe(4).
            AddIngredient<BirchWoodBlock>(1).
            AddTile(TileID.LivingLoom).
            Register();
    }
}