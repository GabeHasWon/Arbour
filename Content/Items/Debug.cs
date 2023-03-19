using Arbour.Common.WorldGeneration;
using Arbour.Content.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Items
{
	public class Debug : ModItem
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("arbor test item");
        public override bool IsLoadingEnabled(Mod mod) => false;

        public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 2;
			Item.useAnimation = 2;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            //Item.createTile = ModContent.TileType<MicrobirchSapling>();
            Item.createWall = ModContent.WallType<ArborLeafWall>();
        }

        public override bool? UseItem(Player player)
        {
            //Microbirch.SpawnAt(Main.MouseWorld.ToTileCoordinates());

            Point pos = Main.MouseWorld.ToTileCoordinates();
            //int len = Main.rand.Next(6, 18);
            //for (int i = 0; i < len; ++i)
            //    WorldGen.PlaceTile(pos.X, pos.Y + i, ModContent.TileType<ArborVines>());
            //TileHelper.TryPlaceProperly(pos, ModContent.TileType<Hay1x3>(), forceIfPossible: true);
            ArborGeneration.BuildSingleIsland(pos.X, pos.Y);
            //WorldGen.PlaceObject(pos.X, pos.Y, ModContent.TileType<ArborSapling>());
            //Main.NewText(WorldGen.GrowTree(pos.X, pos.Y));
            return true;
        }
    }
}