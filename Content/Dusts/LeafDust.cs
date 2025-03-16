using Terraria;
using Terraria.ModLoader;

namespace Arbour.Content.Dusts;

public class LeafDust : ModDust
{
    public override void SetStaticDefaults() => UpdateType = 33;

    public override void OnSpawn(Dust dust)
    {
        dust.velocity *= 0.5f;
        dust.velocity.Y += 1f;

        if (Collision.WetCollision(dust.position, 6, 6))
        {
            dust.velocity.Y = 0;
        }
    }
}