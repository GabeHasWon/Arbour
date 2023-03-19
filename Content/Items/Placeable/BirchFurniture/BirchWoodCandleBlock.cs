using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable.BirchFurniture;

public class BirchWoodCandleBlock : ModItem
{
	public override void SetDefaults() 
    { 
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Multitiles.Furniture.BirchCandle>());
        SacrificeTotal = 100;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1).
            AddIngredient<BirchWoodBlock>(4).
            AddIngredient(ItemID.Torch).
            AddTile(TileID.WorkBenches).
            Register();
    }
}