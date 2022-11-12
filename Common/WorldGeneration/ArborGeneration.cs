using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace Arbour.Common.WorldGeneration
{
    internal class ArborGeneration
    {
        public static void BuildSingleIsland(int x, int y)
        {

        }

        public static void OutlineCircle(int x, int y, int type, int radius, bool fill = true, int wallType = WallID.None)
        {
            for (int i = x - radius; i < x + radius + 1; i++)
            {
                for (int j = y - radius; j < y + radius + 1; j++)
                {
                    float dist = (int)Vector2.DistanceSquared(new Vector2(x, y), new Vector2(i, j));

                    if (dist >= Math.Pow(radius - 2, 2) && dist < Math.Pow(radius, 2))
                        WorldGen.PlaceTile(i, j, type, true);
                }
            }

            if (fill)
                RecursiveFill(x, y, type, wallType, 0);
        }

        public static int RecursiveFill(int x, int y, int type, int wallType, int maxReps)
        {
            int reps = maxReps;

            if (reps > 1800)
                return 1801;

            WorldGen.PlaceTile(x, y, type, true);

            if (wallType != WallID.None)
                WorldGen.PlaceWall(x, y, wallType, true);

            if (!Main.tile[x, y - 1].HasTile)
            {
                RecursiveFill(x, y - 1, type, wallType, maxReps);
                reps++;
            }

            if (!Main.tile[x, y + 1].HasTile)
            {
                RecursiveFill(x, y + 1, type, wallType, maxReps);
                reps++;
            }

            if (!Main.tile[x - 1, y].HasTile)
            {
                RecursiveFill(x - 1, y, type, wallType, maxReps);
                reps++;
            }

            if (!Main.tile[x + 1, y].HasTile)
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

                waterDepth = (int)MathHelper.Clamp(waterDepth + Main.rand.Next(-1, 2), 0, 3);

                if (i > x + width - 3 && waterDepth > 0)
                    waterDepth = 0;
            }
        }
    }
}
