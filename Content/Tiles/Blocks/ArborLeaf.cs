using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Tiles.Blocks;

internal class ArborLeaf : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileSolid[Type] = true;
		Main.tileMerge[Type][Type] = true;
		Main.tileBlockLight[Type] = true;

		TileHelper.MergeWith(Type, TileID.Dirt, ModContent.TileType<ArborGrass>(), ModContent.TileType<BirchWoodTile>());
		AddMapEntry(new Color(252, 150, 55));

		DustType = ModContent.DustType<Dusts.LeafDust>();
	}
}