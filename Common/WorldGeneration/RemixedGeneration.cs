using Arbour.Content.Tiles.Blocks;
using Arbour.Content.Tiles.Custom;
using Arbour.Content.Tiles.Multitiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Arbour.Common.WorldGeneration;

internal class RemixedGeneration
{
    private static float WorldSize => Main.maxTilesX / 4200f;

    public static WorldGen.GrowTreeSettings TreeSettings;

    public static bool ArborTreeGroundTest(int tileType) => tileType == ModContent.TileType<ArborGrass>();

    internal static void SpawnArborPockets(GenerationProgress progress, GameConfiguration configuration)
    {
        int repeats = (int)(4 * WorldSize);

        for (int i = 0; i < repeats; ++i)
        {
            bool success = SpawnPocket(WorldGen.genRand.Next(Main.maxTilesX / 4, (int)(Main.maxTilesX / 1.25f)), 
                WorldGen.genRand.Next(Main.maxTilesY / 2, Main.maxTilesY - 300));

            if (!success)
                i--;
        }
    }

    internal static bool SpawnPocket(int x, int y)
    {
        int width = (int)(WorldGen.genRand.Next(40, 60) * WorldSize);
        int height = (int)(WorldGen.genRand.Next(70, 90) * WorldSize);
        float ratio = width / (float)height;
        float maxDist = width * width * ratio * ratio;

        HashSet<Point> points = [];

        for (int i = x - (width / 1); i < x + (width / 1); ++i)
        {
            for (int j = y - (height / 1); j < y + (height / 1); ++j)
            {
                float dist = (MathF.Pow(j - y, 2) * ratio) + MathF.Pow(i - x, 2);

                if (dist < maxDist)
                    CheckTile(i, j, dist, maxDist, points);
            }
        }

        if (points.Count < 40)
            return false;

        foreach (var item in points)
        {
            WorldGen.KillTile(item.X, item.Y, false, false, true);
            WorldGen.PlaceTile(item.X, item.Y, (ushort)ModContent.TileType<ArborGrass>(), true);

            if (Main.tile[item.X, item.Y - 1].HasTile)
                Main.tile[item].TileType = (ushort)ModContent.TileType<ArborGrass>();

            WorldGen.TileFrame(item.X, item.Y, true, true);
        }

        foreach (var item in points)
        {
            if (!WorldGen.SolidTile(item.X, item.Y + 1))
            {
                if (WorldGen.genRand.NextBool(20))
                    Microbirch.SpawnAt(item.X, item.Y + 1);
                else if (!WorldGen.genRand.NextBool(5))
                    ArborGeneration.PlaceVines(item.X, item.Y);
            }
            else if (WorldGen.SolidTile(item.X, item.Y - 1))
            {
                ArborGeneration.TryGrowTreeAndWalls(new(item.X, item.Y));
                ArborGeneration.PlaceDecor(new(item.X, item.Y));
            }
        }

        for (int i = 0; i < 8; ++i)
        {
            Vector2 item = points.ElementAt(WorldGen.genRand.Next(points.Count)).ToWorldCoordinates();

            if (!Collision.SolidCollision(item - new Vector2(16, -16), 48, 32))
                WormWalls(item.ToTileCoordinates(), 1);
            else if (!Collision.SolidCollision(item - new Vector2(16, 56), 48, 32))
                WormWalls(item.ToTileCoordinates(), -1);
            else
                i--;
        }

        return true;
    }

    private static void WormWalls(Point point, int direction)
    {
        Vector2 position = point.ToVector2();
        Vector2 velocity = new(0, direction);

        while (true)
        {
            position += velocity;
            velocity = velocity.RotatedBy(WorldGen.genRand.NextFloat(MathHelper.PiOver4 * 0.2f) * (WorldGen.genRand.NextBool() ? -1 : 1));

            ArborGeneration.SpamWalls(position.ToPoint16(), 5, 5);

            if (Collision.SolidCollision(position.ToWorldCoordinates() - new Vector2(16), 32, 32) || !WorldGen.InWorld((int)position.X, (int)position.Y))
                break;
        }
    }

    private static void CheckTile(int i, int j, float dist, float maxDist, HashSet<Point> points)
    {
        if (dist > maxDist * 0.55f)
        {
            float distance = MathF.Sqrt(maxDist) - MathF.Sqrt(dist);

            if (distance <= 1 || WorldGen.genRand.NextBool((int)distance))
                return;
        }

        if (!Main.tile[i, j].HasTile || Main.tile[i, j].TileType != TileID.Grass)
            return;

        points.Add(new(i, j));
    }
}
