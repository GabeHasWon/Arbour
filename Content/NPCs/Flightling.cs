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
public class Flightling : ModNPC
{
    private bool _leavesBroken = false;

    public override void SetStaticDefaults() => Main.npcFrameCount[NPC.type] = 2;

    public override void SetDefaults()
    {
        NPC.width = 24;
        NPC.height = 22;
        NPC.damage = 5;
        NPC.defense = 6;
        NPC.lifeMax = 30;
        NPC.value = 10;
        NPC.knockBackResist = 1.05f;
        NPC.aiStyle = NPCAIStyleID.Fighter;
        NPC.HitSound = SoundID.Grass;
        NPC.DeathSound = SoundID.DD2_GoblinHurt;

        AIType = NPCID.Crab;
        SpawnModBiomes = new int[1] { ModContent.GetInstance<ArborBiome>().Type };
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    { 
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
            new FlavorTextBestiaryInfoElement("Mods.Arbour.NPCs.Flightling.Bestiary"),
        });
    }

    public override bool PreAI()
    {
        NPC.velocity.X *= 0.95f;

        if (NPC.velocity.X != 0)
            NPC.spriteDirection = -Math.Sign(NPC.velocity.X);

        if (_leavesBroken)
        {
            ScaredyAI();
            return false;
        }
        return true;
    }

    private void ScaredyAI()
    {
        const float JumpSpeed = 4.6f;

        NPC.TargetClosest(false);
        Player target = Main.player[NPC.target];

        float dir = target.Center.X < NPC.Center.X ? 1 : -1;
        NPC.velocity.X += dir * 0.2f;
        NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -4, 4);

        bool grounded = Collision.SolidCollision(NPC.BottomLeft, NPC.width, 6) && NPC.velocity.Y >= 0; //Little jumps so it can skedaddle away
        if (Collision.SolidCollision(NPC.position + new Vector2(NPC.width, 0), 8, NPC.height - 2) && NPC.collideY && grounded)
            NPC.velocity.Y = -JumpSpeed;

        if (Collision.SolidCollision(NPC.position - new Vector2(8, 0), 8, NPC.height - 2) && NPC.collideY && grounded)
            NPC.velocity.Y = -JumpSpeed;
    }

    public override void FindFrame(int frameHeight) => NPC.frame.Y = frameHeight * _leavesBroken.ToInt();

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= NPC.lifeMax / 2 && !_leavesBroken)
        {
            _leavesBroken = true;

            for (int i = 0; i < 16; ++i)
                Dust.NewDust(NPC.position, NPC.width, NPC.height / 2, DustID.Pumpkin, Main.rand.NextFloat(-2f, 2), Main.rand.NextFloat(-2, -0.5f), 0, Color.Orange, Main.rand.NextFloat(1f, 1.5f));
        }

        if (NPC.life <= 0)
        {
            for (int i = 0; i < 16; ++i)
                Dust.NewDust(NPC.position, NPC.width, NPC.height / 2, DustID.Pumpkin, Main.rand.NextFloat(-2f, 2), Main.rand.NextFloat(-2, -0.5f), 0, Color.Orange, Main.rand.NextFloat(1f, 1.5f));
        }
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.SpawnTileType == ModContent.TileType<ArborGrass>() ? 0.5f : 0f;
}