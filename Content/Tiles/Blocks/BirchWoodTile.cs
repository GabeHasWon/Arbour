using Arbour.Content.Items.Placeable;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Tiles.Blocks;

internal class BirchWoodTile : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileSolid[Type] = true;
		Main.tileMerge[Type][Type] = true;
		Main.tileBlockLight[Type] = true;

		TileHelper.MergeWith(Type, TileID.Dirt, ModContent.TileType<ArborGrass>());
		AddMapEntry(new Color(230, 230, 235));

		DustType = ModContent.DustType<Dusts.BirchDust>();
	}
}