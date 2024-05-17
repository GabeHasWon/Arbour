using Arbour.Content.Tiles.Multitiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Items.Placeable;

public class StarlitWindsBoxItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 1;
		MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Music/Day"), ModContent.ItemType<StarlitWindsBoxItem>(), ModContent.TileType<StarlitWindsBox>());
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<StarlitWindsBox>());
		Item.rare = ItemRarityID.LightRed;
	}
}