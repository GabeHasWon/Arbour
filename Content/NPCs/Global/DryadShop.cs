using Arbour.Content.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.NPCs.Global;

internal class DryadShop : GlobalNPC
{
    public override bool AppliesToEntity(NPC entity, bool lateInstantiation) => entity.type == NPCID.Dryad;

    public override void ModifyShop(NPCShop shop)
    {
        shop.Add(new NPCShop.Entry(ModContent.ItemType<ArborGrassSeeds>()));
    }
}
