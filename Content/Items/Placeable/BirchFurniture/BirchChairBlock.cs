using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable.BirchFurniture;

public class BirchChairBlock : ModItem
{
    public override void SetDefaults() 
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Multitiles.Furniture.BirchChair>());
        Item.ResearchUnlockCount = 100;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1).
            AddIngredient<BirchWoodBlock>(4).
            AddTile(TileID.WorkBenches).
            Register();
    }
}