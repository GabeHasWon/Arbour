using Arbour.Content.Tiles.Custom;
using Arbour.Content.Tiles.Multitiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

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

    public override void RandomUpdate(int i, int j)
    {
		Tile tile = Main.tile[i, j + 1];
		if (!tile.HasTile)
		{
			if (Main.rand.NextBool(220))
				Microbirch.SpawnAt(i, j + 1);
			else if (Main.rand.NextBool(10))
				WorldGen.PlaceTile(i, j + 1, ModContent.TileType<ArborVines>(), true);
		}

		if (Main.rand.NextBool(14) && TileHelper.TryPlaceProperly(i, j, ModContent.TileType<Hay1x3>(), forceIfPossible: false))
			return;

		if (Main.rand.NextBool(10) && TileHelper.TryPlaceProperly(i, j, ModContent.TileType<Hay1x2>(), forceIfPossible: false))
			return;

		if (Main.rand.NextBool(7) && TileHelper.TryPlaceProperly(i, j, ModContent.TileType<Hay1x1>(), forceIfPossible: false))
			return;
	}
}
