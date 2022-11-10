using Arbour.Content.Projectiles.Info;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Projectiles.Environmental
{
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

            var npcs = Main.npc.Take(Main.maxNPCs).Where(x => x.CanBeChasedBy());
            var plrs = Main.player.Take(Main.maxPlayers).Where(x => x.active && !x.dead);

            for (int i = 0; i < States.Count; ++i)
            {
                if (UpdateSingleState(i, plrs, npcs))
                    i--;
            }

            if (States.Count == 0)
                Projectile.Kill();
        }

        private bool UpdateSingleState(int index, IEnumerable<Player> plrs, IEnumerable<NPC> npcs)
        {
            TileDrawState tileDrawState = States[index];

            Vector2 center = Projectile.position + Vector2.UnitY * (Projectile.height / 2) - new Vector2(0, 6); //Center of the projectile
            Vector2 centeredOffset = tileDrawState.offsetFromOrigin.ToWorldCoordinates() - Vector2.UnitY * (Projectile.height / 2); //Offset from that center
            Vector2 realOffset = centeredOffset.RotatedBy(Projectile.rotation); //Rotate the offset to fit the visual
            Vector2 realPos = center + realOffset + Projectile.velocity; //Get the real position by combining all of the above

            void KillMe()
            {
                States.RemoveAt(index);

                for (int i = 0; i < 7; ++i)
                    Dust.NewDust(realPos, tileDrawState.frame.X, tileDrawState.frame.Y, DustID.Pumpkin);

                if (tileDrawState.overrideTex is not null)
                    for (int i = 0; i < 4; ++i)
                        Gore.NewGore(Terraria.Entity.InheritSource(Projectile), realPos, Vector2.Zero, GoreID.TreeLeaf_VanityTreeYellowWillow);

                Item.NewItem(Terraria.Entity.InheritSource(Projectile), realPos, tileDrawState.frame.X, tileDrawState.frame.Y, ItemID.Wood);
            }

            if (Collision.SolidCollision(realPos, tileDrawState.frame.X, tileDrawState.frame.Y))
            {
                KillMe();
                return true;
            }

            var hitbox = new Rectangle((int)realPos.X, (int)realPos.Y, tileDrawState.frame.X, tileDrawState.frame.Y);

            foreach (var item in plrs) //Player collision
            {
                if (item.Hitbox.Intersects(hitbox))
                {
                    string[] deathTexts = new string[] { $"{item.name} cut a tree above their head.", $"A tree landed on {item.name}'s head.", $"{item.name} discovered gravity...again." };
                    item.Hurt(PlayerDeathReason.ByCustomReason(Main.rand.Next(deathTexts)), Main.DamageVar(12), 0, false, false, false);

                    KillMe();
                    return true;
                }
            }

            foreach (var npc in npcs) //NPC collision
            {
                if (npc.Hitbox.Intersects(hitbox))
                {
                    npc.StrikeNPCNoInteraction(Main.DamageVar(12), 1f, 0, false, false, false);

                    KillMe();
                    return true;
                }
            }

            if (Main.rand.NextBool(22) && tileDrawState.overrideTex is not null)
                Gore.NewGore(Terraria.Entity.InheritSource(Projectile), realPos, Vector2.Zero, GoreID.TreeLeaf_VanityTreeYellowWillow);
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
}
