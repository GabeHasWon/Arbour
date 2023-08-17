using Arbour.Content.Gores;
using Arbour.Content.Tiles.Banners;
using Arbour.Content.Tiles.Blocks;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.NPCs;

[AutoloadBanner]
public class BrushTooth : ModNPC
{
    ref float Timer => ref NPC.ai[0];

    Player Target => Main.player[NPC.target];

    public override void SetStaticDefaults() => Main.npcFrameCount[NPC.type] = 2;

    public override void SetDefaults()
    {
        NPC.width = 34;
        NPC.height = 36;
        NPC.damage = 30;
        NPC.defense = 0;
        NPC.lifeMax = 50;
        NPC.value = 100;
        NPC.knockBackResist = 0.8f;
        NPC.aiStyle = -1;
        NPC.HitSound = SoundID.Grass;
        NPC.DeathSound = SoundID.DD2_GoblinHurt;
        NPC.noGravity = true;

        SpawnModBiomes = new int[1] { ModContent.GetInstance<ArborBiome>().Type };
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    { 
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
            new FlavorTextBestiaryInfoElement("Mods.Arbour.NPCs.BrushTooth.Bestiary"),
        });
    }

    public override void AI()
    {
        float maxSpeed = Main.expertMode ? Main.masterMode ? 7 : 5 : 4;

        if (Main.netMode != NetmodeID.Server && Main.rand.NextBool(250))
            Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.GoreType<OrangeLeaf>());

        Timer++;

        NPC.TargetClosest();
        NPC.rotation = NPC.velocity.ToRotation();

        if (NPC.collideX)
            NPC.velocity.X *= -1;

        if (NPC.collideY)
            NPC.velocity.Y *= -1;

        if (!Target.active || Target.dead || !Collision.CanHit(NPC, Target))
        {
            if (NPC.velocity.LengthSquared() < maxSpeed * maxSpeed)
                NPC.velocity *= 1.03f;

            NPC.velocity = NPC.velocity.RotatedBy(MathF.Sin(Timer * 0.02f) * 0.02f);
        }
        else
        {
            NPC.velocity += NPC.DirectionTo(Target.Center) * 0.133f;

            if (NPC.velocity.LengthSquared() > maxSpeed * maxSpeed)
                NPC.velocity = Vector2.Normalize(NPC.velocity) * maxSpeed;
        }
    }

    public override void FindFrame(int frameHeight) => NPC.frame.Y = frameHeight * (NPC.frameCounter++ % 8 <= 3).ToInt();

    public override void HitEffect(NPC.HitInfo hit)
    {
        for (int i = 0; i < 16; ++i)
            Dust.NewDust(NPC.position, NPC.width, NPC.height / 2, DustID.Pumpkin, NPC.velocity.X, NPC.velocity.Y, 0, Color.Orange, Main.rand.NextFloat(1f, 1.5f));

        if (NPC.life <= 0)
        {
            for (int i = 0; i < 16; ++i)
                Dust.NewDust(NPC.position, NPC.width, NPC.height / 2, DustID.Pumpkin, Main.rand.NextFloat(-2f, 2), 
                    Main.rand.NextFloat(-2, -0.5f), 0, Color.Orange, Main.rand.NextFloat(1f, 1.5f));
        }
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.SpawnTileType == ModContent.TileType<ArborGrass>() && !Main.dayTime ? 0.3f : 0f;
}