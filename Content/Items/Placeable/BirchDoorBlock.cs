using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable;

public class BirchDoorBlock : ModItem
{
	public override void SetDefaults() => Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Multitiles.Furniture.BirchDoorClosed>());

    public override void AddRecipes()
    {
        CreateRecipe(1).
            AddIngredient<BirchWoodBlock>(6).
            AddTile(TileID.WorkBenches).
            Register();
    }
}