using Arbour.Content.Items.Placeable;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Tiles.Multitiles;

internal class HayCommon
{
    internal static void TryDropSeeds(int i, int j, int chance, int height)
    {
        if (Main.rand.NextBool(chance))
        {
            int item = Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, (int)(height * Main.rand.NextFloat()), ModContent.ItemType<ArborGrassSeeds>());

            if (Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item);
        }
    }
}
