using Arbour.Content.Tiles.Multitiles.Furniture;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable.BirchFurniture;

public class BirchWoodLampBlock : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<BirchLamp>());
        Item.ResearchUnlockCount = 100;
    }

    public override void AddRecipes()
    {
        CreateRecipe().
            AddIngredient<BirchWoodBlock>(6).
            AddIngredient(ItemID.Torch).
            AddTile(TileID.WorkBenches).
            Register();
    }
}