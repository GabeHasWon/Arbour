using Arbour.Content.Tiles;
using Arbour.Content.Tiles.Blocks;
using Arbour.Content.Tiles.Custom;
using Arbour.Content.Tiles.Multitiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Arbour.Common.WorldGeneration;

internal class ArborGeneration
{
    private static UnifiedRandom Random => Main.rand;

    public static void BuildSingleIsland(int x, int y, bool genericUnderside = false)
    {
        int width = Random.Next(120, 140);

        int left = x - (width / 2);
        int right = x + (width / 2) + 1;
        int maxDepth = 0;

        List<Point16> topGrass = new();

        for (int i = left; i < right; ++i)
        {
            int distFromCenter = Math.Abs(i - x); //X distance from center
            float pow = MathF.Pow(distFromCenter + Random.Next(-1, 2), 2f); //Power function for curve
            float yOff = pow * 0.5f / width; //Adjustment for top surface curve

            float height = (width / 4) - (yOff * 2) + Random.Next(2); //Depth
            float adjHeight = 1f / ((distFromCenter == 0 ? 1 : distFromCenter) * 0.0008f) * 0.008f; //Extra depth for spice
            float adjHeightSine = MathF.Sin(distFromCenter * 0.6f) * 2; //Sine wave for EXTRA spice
            float realHeight = Math.Max(height + adjHeight + adjHeightSine, 4);

            int startY = y + (int)yOff; //Self explanatory
            int endY = startY + (int)realHeight;

            if (maxDepth < endY)
                maxDepth = endY;

            for (int j = startY; j < endY; ++j)
            {
                bool external = j == startY || i == left || i == right - 1;
                WorldGen.PlaceTile(i, j, external ? ModContent.TileType<ArborGrass>() : TileID.Dirt);

                if (j == startY)
                    topGrass.Add(new(i, j));
            }
        }

        if (genericUnderside)
            GenericCloudUnderside(x, width, left, right, maxDepth);
        else
        {
            for (int i = 0; i < 27; ++i) //Spam circles for underside
            {
                int Offset() => Random.Next(-width / 12, width / 12);

                int placeX = x;
                for (int j = 0; j < 8; ++j)
                    placeX += Offset();

                int placeY = maxDepth;

                if (!TileHelper.FindAbove(placeX, ref placeY))
                    continue;

                OutlineCircle(placeX, placeY - 3, TileID.Cloud, (int)(Random.Next(4, 10) * (width / 110f)), true, WallID.Cloud);
            }
        }

        PlaceUndergrowth(left, right, maxDepth);
        GrowStuffOnGrass(topGrass);
    }

    private static void GrowStuffOnGrass(List<Point16> topGrass)
    {
        foreach (var item in topGrass)
        {
            if (Main.rand.NextBool(10) && TileHelper.TryPlaceProperly(item.X, item.Y - 1, ModContent.TileType<FloorFoliage2x2>(), forceIfPossible: false))
                continue;

            if (Main.rand.NextBool(10) && TileHelper.TryPlaceProperly(item.X, item.Y - 1, ModContent.TileType<BigPinecone2x2>(), forceIfPossible: false))
                continue;

            if (Main.rand.NextBool(8) && TileHelper.TryPlaceProperly(item.X, item.Y - 1, ModContent.TileType<Hay1x3>(), forceIfPossible: false))
                continue;

            if (Main.rand.NextBool(7) && TileHelper.TryPlaceProperly(item.X, item.Y - 1, ModContent.TileType<FloorFoliage2x1>(), forceIfPossible: false))
                continue;

            if (Main.rand.NextBool(3) && TileHelper.TryPlaceProperly(item.X, item.Y - 1, ModContent.TileType<Hay1x2>(), forceIfPossible: false))
                continue;

            if (Main.rand.NextBool(3) && TileHelper.TryPlaceProperly(item.X, item.Y - 1, ModContent.TileType<PineconeSmall>(), forceIfPossible: false))
                continue;

            if (TileHelper.TryPlaceProperly(item.X, item.Y - 1, ModContent.TileType<Hay1x1>(), forceIfPossible: false))
                continue;
        }
    }

    private static void PlaceUndergrowth(int left, int right, int maxDepth)
    {
        for (int i = left; i < right; ++i)
        {
            int placeY = maxDepth;

            if (!TileHelper.FindTypesAbove(i, ref placeY, TileID.Dirt))
                continue;

            if (Main.tile[i, placeY].TileType == TileID.Dirt && !Main.tile[i, placeY + 1].HasTile)
            {
                Tile dirt = Main.tile[i, placeY];
                dirt.TileType = (ushort)ModContent.TileType<ArborGrass>();

                WorldGen.TileFrame(i, placeY);
                UndergrowthPlants(i, placeY);
            }
        }
    }

    private static void UndergrowthPlants(int i, int placeY)
    {
        if (Main.rand.NextBool(17))
            Microbirch.SpawnAt(i, placeY + 1);
        else
        {
            int len = Random.Next(5, 20);
            for (int j = 0; j < len; ++j)
            {
                if (Main.tile[i, placeY + j + 1].HasTile)
                    break;
                WorldGen.PlaceTile(i, placeY + j + 1, ModContent.TileType<ArborVines>());
            }
        }
    }

    private static void GenericCloudUnderside(int x, int width, int left, int right, int maxDepth)
    {
        for (int i = left; i < right; ++i)
        {
            int placeY = maxDepth;
            int distFromCenter = Math.Abs(i - x); //X distance from center
            float pow = MathF.Pow(distFromCenter + Random.Next(-1, 2), 2f); //Power function for curve
            float yOff = pow * 0.5f / width; //Adjustment for top surface curve
            float adjHeightSine = -MathF.Pow(MathF.Sin(distFromCenter * 0.1f - MathHelper.PiOver2), 2) * 10; //Sine wave for EXTRA spice
            float height = (width / 4) - (yOff * 1.2f) + Random.Next(2) - adjHeightSine; //Depth

            TileHelper.FindAbove(i, ref placeY);

            for (int j = placeY; j < placeY - 30 + height; ++j)
                WorldGen.PlaceTile(i, j, TileID.Cloud);
        }
    }

    public static void OutlineCircle(int x, int y, int type, int radius, bool fill = true, int wallType = WallID.None)
    {
        for (int i = x - radius; i < x + radius + 1; i++)
        {
            for (int j = y - radius; j < y + radius + 1; j++)
            {
                float dist = (int)Vector2.DistanceSquared(new Vector2(x, y), new Vector2(i, j));

                if (!fill)
                {
                    if (dist >= Math.Pow(radius - 2, 2) && dist < Math.Pow(radius, 2))
                        WorldGen.PlaceTile(i, j, type, true);
                }
                else
                {
                    if (dist < Math.Pow(radius, 2))
                        WorldGen.PlaceTile(i, j, type, true);
                    if (dist < Math.Pow(radius - 1, 2))
                        WorldGen.PlaceWall(i, j, wallType, true);
                }
            }
        }
    }

    public static int RecursiveFill(int x, int y, int type, int wallType, int maxReps, bool forceUntilType = true)
    {
        int reps = maxReps;

        if (reps > 1800)
            return 1801;

        WorldGen.PlaceTile(x, y, type, true);

        if (wallType != WallID.None)
            WorldGen.PlaceWall(x, y, wallType, true);

        bool ValidRecursion(int x, int y) => forceUntilType ? (!Main.tile[x, y - 1].HasTile && Main.tile[x, y - 1].TileType != type) : !Main.tile[x, y - 1].HasTile;

        if (ValidRecursion(x, y - 1))
        {
            RecursiveFill(x, y - 1, type, wallType, maxReps);
            reps++;
        }

        if (ValidRecursion(x, y + 1))
        {
            RecursiveFill(x, y + 1, type, wallType, maxReps);
            reps++;
        }

        if (ValidRecursion(x - 1, y))
        {
            RecursiveFill(x - 1, y, type, wallType, maxReps);
            reps++;
        }

        if (ValidRecursion(x + 1, y))
        {
            RecursiveFill(x + 1, y, type, wallType, maxReps);
            reps++;
        }

        return reps;
    }

    public static void PebblePond(int x, int y)
    {
        const int MaxDepth = 4;

        int waterDepth = 0;
        int width = 20;

        for (int i = x - width; i < x + width; ++i)
        {
            for (int j = y; j < y + MaxDepth; ++j)
            {
                Tile tile = Main.tile[i, j];

                if (j - y < waterDepth)
                {
                    tile.LiquidType = LiquidID.Water;
                    tile.LiquidAmount = 255;
                }
                else
                    WorldGen.PlaceTile(i, j, TileID.Stone);
            }

            waterDepth = (int)MathHelper.Clamp(waterDepth + Random.Next(-1, 2), 0, 3);

            if (i > x + width - 3 && waterDepth > 0)
                waterDepth = 0;
        }
    }
}
