using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ObjectData;

namespace Arbour.Content.Tiles
{
    internal class TileHelper
    {
        public static Vector2 TileOffset => Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
        public static Vector2 TileCustomPosition(int i, int j, Vector2? off = null) => (new Vector2(i, j) * 16) - Main.screenPosition - (off ?? new Vector2(0)) + TileOffset;

        public static bool TryPlaceProperly(int i, int j, int type, bool quiet = false, bool mute = true, int style = -1, bool forceIfPossible = false)
        {
            TileObjectData data = TileObjectData.GetTileData(type, style == -1 ? 0 : style, 0);

            if (data is null)
            {
                bool canPlace = forceIfPossible ? !WorldGen.SolidTile(i, j) : !Main.tile[i, j].HasTile;

                if (canPlace)
                {
                    WorldGen.PlaceTile(i, j, type, mute);
                    return true;
                }
                return false;
            }
            else
            { 
                int width = data.Width;
                int height = data.Height;

                if (!AreaClear(i, j, width, height, forceIfPossible))
                    return false;

                int x = i + data.Origin.X - width;
                int y = j + data.Origin.Y - height;
                int useStyle = style == -1 ? Main.rand.Next(data.RandomStyleRange) : style;

                WorldGen.PlaceObject(x, y, type, mute, useStyle);

                if (!quiet)
                    NetMessage.SendTileSquare(-1, x, y, width, height, TileChangeType.None);
                return true;
            }
        }

        public static int TilesInRectangle(int i, int j, int width, int height, bool onlySolids = false)
        {
            int count = 0;

            for (int x = i; x < i + width; ++x)
                for (int y = j; y < j + height; ++y)
                    if ((onlySolids && !WorldGen.SolidTile(x, y)) || !Main.tile[x, y].HasTile)
                        count++;

            return count;
        }

        public static bool AreaClear(int i, int j, int width, int height, bool onlySolids = true) => TilesInRectangle(i, j, width, height, onlySolids) == 0;

        public static void FindAbove(int x, ref int y)
        {
            while (!Main.tile[x, y].HasTile)
                y--;
        }
    }
}
