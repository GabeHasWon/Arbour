using Arbour.Common.WorldGeneration;
using Arbour.Content.Walls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Items
{
	public class Debug : ModItem
	{
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
			var m = Main.MouseWorld.ToTileCoordinates();
			RemixedGeneration.SpawnPocket(m.X, m.Y);
            return true;
        }
    }
}