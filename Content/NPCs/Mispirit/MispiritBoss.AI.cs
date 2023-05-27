using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Arbour.Content.NPCs.Mispirit;

public partial class MispiritBoss : ModNPC
{
    private void ChangeState(MispiritAIState state)
    {
        AIState = state;
        Timer = 0;
    }

    private void SpawnBehaviour()
    {
        if (Timer % 1 == 0)
        {
            int proj = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MispiritLeaves>(), 0, 0, Main.myPlayer);
            Main.projectile[proj].ai[0] = NPC.whoAmI;
        }

        if (Timer > 70)
            ChangeState(MispiritAIState.SeekPlayer);
    }

    private void SeekBehaviour()
    {
        NPC.velocity = (Target.Center - NPC.Center) * 0.02f;
        SuctionItems();

        if (Timer > 120)
            ChangeState(MispiritAIState.SeekPlayer);
    }
}
