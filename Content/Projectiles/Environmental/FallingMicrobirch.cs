using Arbour.Content.Projectiles.Info;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
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

            for (int i = 0; i < States.Count; ++i)
            {
                if (UpdateSingleState(i))
                    i--;
            }

            if (States.Count == 0)
                Projectile.Kill();
        }

        private bool UpdateSingleState(int index)
        {
            TileDrawState tileDrawState = States[index];

            Vector2 center = Projectile.position + Vector2.UnitY * (Projectile.height / 2) - new Vector2(0, 6);
            Vector2 baseOffset = tileDrawState.offsetFromOrigin.ToWorldCoordinates();
            Vector2 centeredOffset = baseOffset - Vector2.UnitY * (Projectile.height / 2);
            Vector2 realOffset = centeredOffset.RotatedBy(Projectile.rotation);
            Vector2 realPos = center + realOffset + Projectile.velocity;

            if (Collision.SolidCollision(realPos, tileDrawState.frame.X, tileDrawState.frame.Y))
            {
                States.RemoveAt(index);

                for (int i = 0; i < 7; ++i)
                    Dust.NewDust(realPos, tileDrawState.frame.X, tileDrawState.frame.Y, DustID.Pumpkin);

                Item.NewItem(Terraria.Entity.InheritSource(Projectile), realPos, tileDrawState.frame.X, tileDrawState.frame.Y, ItemID.Wood);
                return true;
            }
            return false;
        }

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
