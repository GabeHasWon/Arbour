using Arbour.Content.Walls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable.BirchFurniture;

public class BirchWoodWallBlock : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 400;
    public override void SetDefaults() => Item.DefaultToPlaceableWall((ushort)ModContent.WallType<BirchWoodWall>());

    public override void AddRecipes()
    {
        CreateRecipe(4).
            AddIngredient<BirchWoodBlock>(1).
            AddTile(TileID.WorkBenches).
            Register();

        Recipe.Create(ModContent.ItemType<BirchWoodBlock>(), 1).
            AddIngredient<BirchWoodWallBlock>(4).
            AddTile(TileID.WorkBenches).
            Register();
    }
}