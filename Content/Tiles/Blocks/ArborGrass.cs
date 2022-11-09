using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Tiles.Blocks;

public class ArborGrass : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileSolid[Type] = true;
		Main.tileMerge[Type][Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileMerge[TileID.Dirt][Type] = true;

		TileID.Sets.Grass[Type] = true;
		TileID.Sets.Conversion.Grass[Type] = true;
		
		AddMapEntry(new Color(104, 156, 70));

		ItemDrop = ItemID.DirtBlock;
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		if (!fail) //Change self into dirt
		{
			fail = true;
			Framing.GetTileSafely(i, j).TileType = TileID.Dirt;
		}
	}
}
