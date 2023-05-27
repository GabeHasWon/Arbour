using Arbour.Content.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.NPCs.Global;

internal class DryadShop : GlobalNPC
{
    public override void SetupShop(int type, Chest shop, ref int nextSlot)
    {
        if (type != NPCID.Dryad)
            return;

        shop.item[nextSlot++].SetDefaults(ModContent.ItemType<ArborGrassSeeds>());
    }
}
