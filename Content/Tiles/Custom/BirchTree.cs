using Arbour.Content.Gores;
using Arbour.Content.Items.Placeable;
using Arbour.Content.Tiles.Blocks;
using Arbour.Content.Tiles.Multitiles;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Tiles.Custom
{
	public class BirchTree : ModTree
	{
		const string Path = "Arbour/Content/Tiles/Custom/BirchTree";

		public override TreePaintingSettings TreeShaderSettings => new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 11f / 72f,
			SpecialGroupMaximumHueValue = 0.25f,
			SpecialGroupMinimumSaturationValue = 0.88f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight) { }
        public override void SetStaticDefaults() => GrowsOnTileId = new int[1] { ModContent.TileType<ArborGrass>() };
		public override Asset<Texture2D> GetTexture() => ModContent.Request<Texture2D>(Path);
		public override Asset<Texture2D> GetBranchTextures() => ModContent.Request<Texture2D>(Path + "_Branches");
		public override Asset<Texture2D> GetTopTextures() => ModContent.Request<Texture2D>(Path + "_Tops");
		public override int CreateDust() => ModContent.DustType<Dusts.BirchDust>();

        public override int SaplingGrowthType(ref int style)
		{
			style = 0;
			return ModContent.TileType<ArborSapling>();
		}

		public override int DropWood() => ModContent.ItemType<BirchWoodBlock>();
		public override int TreeLeaf() => ModContent.GoreType<OrangeLeaf>();

		//public override bool Shake(int x, int y, ref bool createLeaves)
		//{
		//	Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, ModContent.ItemType<Items.Placeable.ExampleBlock>());
		//	return false;
		//}
	}
}