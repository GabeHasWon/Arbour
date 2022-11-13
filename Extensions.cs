using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Utilities;

namespace Arbour
{
    internal static class Extensions
    {
        public static int NextSign(this UnifiedRandom rand) => rand.NextBool(2) ? -1 : 1;

        public static int StyleRange(this ModTile tile)
        {
            var data = TileObjectData.GetTileData(tile.Type, 0, 0);

            if (data is null)
                return 0;
            return data.RandomStyleRange;
        }

        public static int RandomStyleRange(this ModTile tile) => Main.rand.Next(tile.StyleRange());
    }
}