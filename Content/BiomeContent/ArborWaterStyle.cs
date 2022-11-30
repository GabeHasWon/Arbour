using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Arbour.Content.BiomeContent;

public class ArborWaterStyle : ModWaterStyle
{
	public override int ChooseWaterfallStyle() => ModContent.Find<ModWaterfallStyle>("Arbour/ArborWaterfallStyle").Slot;
	public override int GetSplashDust() => Mod.Find<ModDust>("ArborWaterSplash").Type;
	public override int GetDropletGore() => ModContent.GoreType<Gores.ArborDroplet>();

	public override void LightColorMultiplier(ref float r, ref float g, ref float b)
    {
		r = 0.5f;
		g = 0.6f;
		b = 0.8f;
	}

	public override Color BiomeHairColor() => new(183, 68, 42);
}