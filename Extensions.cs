using Terraria;
using Terraria.Utilities;

namespace Arbour
{
    internal static class Extensions
    {
        public static int NextSign(this UnifiedRandom rand) => rand.NextBool(2) ? -1 : 1;
    }
}
