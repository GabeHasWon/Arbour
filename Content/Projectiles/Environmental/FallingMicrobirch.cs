using Arbour.Content.Items.Placeable;
using Arbour.Content.Projectiles.Info;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Arbour.Content.Projectiles.Environmental;

internal class FallingMicrobirch : ModProjectile
{
    internal List<TileDrawState> States = new();

    public override void SetDefaults()
    {
        Projectile.damage = 20;
        Projectile.friendly = true;
        Projectile.hostile = true;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Generic;
        Projectile.aiStyle = 0;
        Projectile.timeLeft = 6000;
        Projectile.width = 16;
        Projectile.tileCollide = false;
    }

    public override void AI()
    {
        Projectile.velocity.Y += 0.2f;
        Projectile.rotation += Projectile.ai[0];

        Span<bool> collidingNPCs = stackalloc bool[Main.maxNPCs];
        foreach (var npc in ActiveEntities.NPCs)
        {
            if (npc.CanBeChasedBy() && EntityInHitbox(npc))
                collidingNPCs[npc.whoAmI] = true;
        }

        Span<bool> collidingPlayers = stackalloc bool[Main.maxPlayers];
        foreach (var plr in ActiveEntities.Players)
        {
            if (!plr.dead && EntityInHitbox(plr))
                collidingPlayers[plr.whoAmI] = true;
        }

        for (int i = 0; i < States.Count; ++i)
        {
            if (UpdateSingleState(i, collidingPlayers, collidingNPCs))
                i--;
        }

        if (States.Count == 0)
            Projectile.Kill();
    }

    private bool EntityInHitbox(Entity entity)
    {
        Vector2 top = Projectile.Center - (Vector2.UnitY * Projectile.height / 2).RotatedBy(Projectile.rotation);
        Vector2 bottom = Projectile.Center + (Vector2.UnitY * Projectile.height / 2).RotatedBy(Projectile.rotation);

        float discard = 0;
        return Collision.CheckAABBvLineCollision(entity.position, entity.Size, top, bottom, 6, ref discard);
    }

    /// <summary>Updates a given TileDrawState. Players and npcs have already been confirmed to be active, not dead and touching the projectile</summary>
    private bool UpdateSingleState(int index, Span<bool> players, Span<bool> npcs)
    {
        TileDrawState tileDrawState = States[index];

        Vector2 center = Projectile.position + Vector2.UnitY * (Projectile.height / 2) - new Vector2(0, 6); //Center of the projectile
        Vector2 centeredOffset = tileDrawState.offsetFromOrigin.ToWorldCoordinates() - Vector2.UnitY * (Projectile.height / 2); //Offset from that center
        Vector2 realOffset = centeredOffset.RotatedBy(Projectile.rotation); //Rotate the offset to fit the visual
        Vector2 realPos = center + realOffset + Projectile.velocity; //Get the real position by combining all of the above

        //Local method for killing the current TileDrawState, with gores and all
        void KillMe()
        {
            States.RemoveAt(index);

            for (int i = 0; i < 7; ++i)
                Dust.NewDust(realPos, tileDrawState.frame.X, tileDrawState.frame.Y, DustID.Pumpkin);

            if (tileDrawState.overrideTex is not null) //If this is a leafy top
            {
                for (int i = 0; i < 4; ++i)
                    Gore.NewGore(Terraria.Entity.InheritSource(Projectile), realPos, Vector2.Zero, Mod.Find<ModGore>("OrangeLeaf").Type);

                int item = Item.NewItem(Terraria.Entity.InheritSource(Projectile), realPos, tileDrawState.frame.X, tileDrawState.frame.Y, ModContent.ItemType<MicrobirchAcorn>(), noBroadcast: true);
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item);
                SoundEngine.PlaySound(SoundID.Grass, realPos);
            }
            else
                SoundEngine.PlaySound(SoundID.Dig, realPos);

            if (!Main.rand.NextBool(5))
            {
                int item = Item.NewItem(Terraria.Entity.InheritSource(Projectile), realPos, tileDrawState.frame.X, tileDrawState.frame.Y, ModContent.ItemType<BirchWoodBlock>(), noBroadcast: true);
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item);
            }
        }

        if (Collision.SolidCollision(realPos, tileDrawState.frame.X, tileDrawState.frame.Y)) //Tile collision check
        {
            KillMe();
            return true;
        }

        var hitbox = new Rectangle((int)realPos.X, (int)realPos.Y, tileDrawState.frame.X, tileDrawState.frame.Y);

        for (int i = 0; i < players.Length; ++i) //Player collision
        {
            if (players[i] && Main.player[i].Hitbox.Intersects(hitbox))
            {
                string text = Language.GetTextValue("Mods.Arbour.MicrobirchKill." + Main.rand.Next(3), Main.player[i].name);
                Main.player[i].Hurt(PlayerDeathReason.ByCustomReason(text), Main.DamageVar(12, Main.player[i].luck), 0);

                KillMe();
                return true;
            }
        }

        for (int i = 0; i < npcs.Length; ++i) //NPC collision
        {
            if (npcs[i] && Main.npc[i].Hitbox.Intersects(hitbox))
            {
                Main.npc[i].StrikeNPC(Main.npc[i].CalculateHitInfo(20, 1, true, 6, DamageClass.Default, false));

                KillMe();
                return true;
            }
        }

        if (Main.rand.NextBool(12) && tileDrawState.overrideTex is not null) //If I'm a leafy part falling, spawn leaves
            Gore.NewGore(Terraria.Entity.InheritSource(Projectile), realPos, Vector2.Zero, Mod.Find<ModGore>("OrangeLeaf").Type);

        return false;
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) => false;

    public override bool PreDraw(ref Color lightColor)
    {
        foreach (var item in States)
        {
            Vector2 origin = (new Vector2(item.Source.Width, Projectile.height) / 2f) - item.offsetFromOrigin.ToWorldCoordinates();
            Vector2 drawPos = Projectile.position - Main.screenPosition + Vector2.UnitY * (Projectile.height / 2) - new Vector2(0, 6);
            Main.spriteBatch.Draw(item.GetTexture(), drawPos, item.Source, lightColor, Projectile.rotation, origin, 1f, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
        }

        return false;
    }
}
