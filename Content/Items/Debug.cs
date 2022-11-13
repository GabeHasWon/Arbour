using Arbour.Common.WorldGeneration;
using Arbour.Content.Tiles.Blocks;
using Arbour.Content.Tiles.Custom;
using Arbour.Content.Tiles.Multitiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Items
{
	public class Debug : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This is a basic modded sword.");
		}

		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.createTile = ModContent.TileType<ArborGrass>();
		}

        public override bool? UseItem(Player player)
        {
            //Item.placeStyle = Main.rand.Next(3);
            //Tile tile = Main.tile[Main.MouseWorld.ToTileCoordinates()];
            //Main.NewText(tile.TileFrameX + " " + tile.TileFrameY);
            //tile.LiquidAmount = 255;
            //tile.LiquidType = LiquidID.Water;

            //Microbirch.SpawnAt(Main.MouseWorld.ToTileCoordinates());

            //Point pos = Main.MouseWorld.ToTileCoordinates();
            //int len = Main.rand.Next(6, 18);
            //for (int i = 0; i < len; ++i)
            //    WorldGen.PlaceTile(pos.X, pos.Y + i, ModContent.TileType<ArborVines>());

            //ArborGeneration.PebblePond(pos.X, pos.Y);
            return true;
        }
    }
}