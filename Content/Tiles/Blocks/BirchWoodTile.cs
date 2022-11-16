﻿using Arbour.Content.Items.Placeable;
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
		AddMapEntry(new Color(198, 98, 43));

		ItemDrop = ModContent.ItemType<BirchWoodBlock>();
	}
}