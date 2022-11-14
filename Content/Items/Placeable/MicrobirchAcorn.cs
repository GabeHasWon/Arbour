using Arbour.Content.Tiles.Multitiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Items
{
    public class MicrobirchAcorn : ModItem
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Can only be planted on ceilings");

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 20;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = 0;
			Item.rare = ItemRarityID.White;
			Item.autoReuse = true;
            Item.createTile = ModContent.TileType<MicrobirchSapling>();
        }
    }
}