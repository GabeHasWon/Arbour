using Arbour.Content.Walls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable.BirchFurniture;

public class BirchFenceBlock : ModItem
{
	public override void SetDefaults()
    {
        Item.DefaultToPlacableWall((ushort)ModContent.WallType<BirchFenceWall>());
        SacrificeTotal = 400;
    }

    public override void AddRecipes()
    {
        CreateRecipe(4).
            AddIngredient<BirchWoodBlock>(1).
            AddTile(TileID.WorkBenches).
            Register();

        Recipe.Create(ModContent.ItemType<BirchWoodBlock>(), 1).
            AddIngredient<BirchFenceBlock>(4).
            AddTile(TileID.WorkBenches).
            Register();
    }
}