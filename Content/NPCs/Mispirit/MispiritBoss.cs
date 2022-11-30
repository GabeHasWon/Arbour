using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;

namespace Arbour.Content.NPCs.Mispirit;

public enum MispiritAIState : int
{
    Spawn,
    TauntFollow
}

public class MispiritBoss : ModNPC
{
    public override bool IsLoadingEnabled(Mod mod) => false;

    private MispiritAIState AIState
    {
        get => (MispiritAIState)NPC.ai[0];
        set => NPC.ai[0] = (float)value;
    }

    private ref float Timer => ref NPC.ai[1];

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Mischevous Spirit");
        Main.npcFrameCount[NPC.type] = 1;
    }

    public override void SetDefaults()
    {
        NPC.width = 24;
        NPC.height = 22;
        NPC.damage = 5;
        NPC.defense = 6;
        NPC.lifeMax = 600;
        NPC.value = Item.buyPrice(0, 5, 0, 0);
        NPC.knockBackResist = 0f;
        NPC.aiStyle = -1;
        NPC.noGravity = true;

        SpawnModBiomes = new int[1] { ModContent.GetInstance<ArborBiome>().Type };
    }

    public override void AI()
    {
        Timer++;

        if (AIState == MispiritAIState.Spawn)
            SpawnBehaviour();
        else
        {
            if (Main.mouseLeft)
                NPC.velocity = NPC.DirectionTo(Main.MouseWorld) * NPC.Distance(Main.MouseWorld) * 0.02f;

            for (int i = 0; i < Main.maxItems; ++i)
            {
                Item item = Main.item[i];

                if (item.active && item.DistanceSQ(NPC.Center) < 100 * 100)
                {
                    item.velocity += item.DirectionTo(NPC.Center) * 0.4f;
                    item.position += item.velocity;

                    if (item.velocity.LengthSquared() > 12 * 12)
                        item.velocity = Vector2.Normalize(item.velocity) * 12;
                }
            }
        }
    }

    private void SpawnBehaviour()
    {
        if (Timer % 1 == 0)
        {
            int proj = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MispiritLeaves>(), 0, 0, Main.myPlayer);
            Main.projectile[proj].ai[0] = NPC.whoAmI;
        }

        if (Timer > 70)
            AIState = MispiritAIState.TauntFollow;
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
            new FlavorTextBestiaryInfoElement("A legendary spirit, known to cause trouble, steal items, and sometimes even eats coins. Why? To cause chaos."),
        });
    }

    //public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.SpawnTileType == ModContent.TileType<ArborGrass>() ? 1f : 0f;
}