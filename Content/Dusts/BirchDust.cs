using Terraria;
using Terraria.ModLoader;

namespace Arbour.Content.Dusts;

public class BirchDust : ModDust
{
    public override void SetStaticDefaults() => UpdateType = 33;

    public override void OnSpawn(Dust dust)
    {
        dust.velocity *= 0.5f;
        dust.velocity.Y += 1f;
    }
}