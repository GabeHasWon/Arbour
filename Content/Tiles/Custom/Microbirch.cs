﻿using Arbour.Content.Projectiles.Environmental;
using Arbour.Content.Projectiles.Info;
using Arbour.Content.Tiles.Blocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Tiles.Custom;

[TileTag(TileTags.NeedsTopAnchor)]
internal class Microbirch : ModTile
{
    private static bool KillingMicrobirch = false;

    const int TreeBottomFrame = 3 * 18;

    public override void SetStaticDefaults()
    {
        DustType = DustID.Pumpkin;
        HitSound = SoundID.Dig;

        Main.tileFrameImportant[Type] = true;
        Main.tileSolid[Type] = false;
        Main.tileAxe[Type] = true;

        AddMapEntry(new Color(230, 230, 235), CreateMapEntryName());
    }

    public override void NearbyEffects(int i, int j, bool closer)
    {
        if (!WorldGen.SolidTile(i, j - 1) && Main.tile[i, j - 1].TileType != Type && Main.tile[i, j - 1].TileType != ModContent.TileType<ArborGrass>())
            WorldGen.KillTile(i, j);
    }

    public static void SpawnAt(int x, int y)
    {
        int length = Main.rand.Next(8, 22);
        bool lastWasBottom = false;

        for (int i = 0; i < length; ++i)
        {
            Tile t = Main.tile[x, y + i];

            if (t.HasTile && WorldGen.SolidTile(x, y + i))
                break;

            t.TileType = (ushort)ModContent.TileType<Microbirch>();
            t.TileFrameX = (short)(Main.rand.Next(3) * 18);
            t.TileFrameY = 0;
            t.HasTile = true;

            bool canBeBottom = i < length - 3 && i > 1 && Main.rand.NextBool(7) && !lastWasBottom;
            lastWasBottom = false;
            if (i == length - 1 || canBeBottom)
            {
                t.TileFrameX = TreeBottomFrame;
                lastWasBottom = true;

                for (int j = 0; j < 4; ++j)
                    Gore.NewGore(new EntitySource_TileUpdate(x, y + i), new Vector2(x, y + i) * 16, Vector2.Zero, ModContent.GetInstance<Arbour>().Find<ModGore>("OrangeLeaf").Type, 1f);
            }
        }
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (fail)
            return;

        Tile above = Main.tile[i, j - 1];
        Tile me = Main.tile[i, j];

        if (above.HasTile && above.TileType == Type)
            above.TileFrameY += 18;

        if (!KillingMicrobirch)
        {
            KillingMicrobirch = true;

            List<TileDrawState> states = new List<TileDrawState>()
            {
                new(new Point(0, 0), new Point(me.TileFrameX, me.TileFrameY), Type)
            };
            int offset = 1;

            while (true)
            {
                Tile next = Main.tile[i, j + offset];
                if (next.HasTile && next.TileType == Type)
                {
                    TileDrawState state = new(new Point(0, offset), new Point(next.TileFrameX, next.TileFrameY), Type);

                    if (next.TileFrameX == TreeBottomFrame)
                    {
                        state.overrideTex = "Arbour/Content/Tiles/Custom/MicrobirchBottom";
                        state.overrideSize = new Point(48, 42);
                        state.frame = new Point(states[^2].frame.X / 18 * 50, 0);
                    }

                    states.Add(state);
                    WorldGen.KillTile(i, j + offset);
                }
                else
                    break;

                offset++;
            }

            int proj = Projectile.NewProjectile(new EntitySource_TileBreak(i, j), Vector2.Zero, Vector2.Zero, ModContent.ProjectileType<FallingMicrobirch>(), 20, 1f, Main.myPlayer);
            Projectile birch = Main.projectile[proj];
            birch.height = ((states.Count - 1) * 16) + 42;
            birch.position = new Vector2(i, j) * 16;
            birch.ai[0] = Main.rand.NextFloat(0.002f, 0.01f) * Main.rand.NextSign();

            FallingMicrobirch microbirch = birch.ModProjectile as FallingMicrobirch;
            microbirch.States = states;

            KillingMicrobirch = false;
        }
    }

    public static void SpawnAt(Point p) => SpawnAt(p.X, p.Y);
    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak) => false;
    public override void NumDust(int i, int j, bool fail, ref int num) => num = 0;

    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
    {
        Tile tile = Main.tile[i, j];

        if (tile.TileFrameX == TreeBottomFrame)
        {
            Texture2D tex = Mod.Assets.Request<Texture2D>("Content/Tiles/Custom/MicrobirchBottom").Value;

            TileSwaySystem.DrawTreeSway(i, j, tex, new Rectangle(Main.tile[i, j - 1].TileFrameX / 18 * 50, 0, 48, 42), new Vector2(6, 0), new Vector2(24, 0), true);
            return false;
        }
        return true;
    }
}
