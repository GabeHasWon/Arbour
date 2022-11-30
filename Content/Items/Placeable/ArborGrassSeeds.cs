using Arbour.Content.Tiles.Blocks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable;

public class ArborGrassSeeds : ModItem
{
	public override void SetStaticDefaults() => Tooltip.SetDefault("Can be placed");

	public override void SetDefaults()
	{
		Item.autoReuse = true;
		Item.useTurn = true;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useAnimation = 15;
		Item.rare = ItemRarityID.White;
		Item.useTime = 15;
		Item.maxStack = 999;
		Item.width = 16;
		Item.height = 18;
		Item.consumable = true;
	}

	public override bool? UseItem(Player player)
	{
		if (Main.netMode == NetmodeID.Server)
			return false;

		Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);

		if (tile.HasTile && tile.TileType == TileID.Dirt && player.InInteractionRange(Player.tileTargetX, Player.tileTargetY)) 
		{
			tile.TileType = (ushort)ModContent.TileType<ArborGrass>();
			WorldGen.TileFrame(Player.tileTargetX, Player.tileTargetY);
		}
		return true;
	}
}
