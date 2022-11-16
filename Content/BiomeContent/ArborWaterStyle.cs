using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Verdant.Waters
{
	public class ArborWaterStyle : ModWaterStyle
	{
		public override int ChooseWaterfallStyle() => ModContent.Find<ModWaterfallStyle>("Verdant/VerdantWaterfallStyle").Slot;
		public override int GetSplashDust() => Mod.Find<ModDust>("VerdantWaterSplash").Type;
		public override int GetDropletGore() => ModContent.GoreType<Gores.Verdant.ArborDroplet>();

		public override void LightColorMultiplier(ref float r, ref float g, ref float b)
        {
			r = 0.675f;
			g = 0.783f;
			b = 0.9f;
		}

		public override Color BiomeHairColor() => new(183, 68, 42);
	}
}