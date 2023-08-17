using Terraria;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Arbour.Content.NPCs.Global;

internal class NPCHappinessGlobal : GlobalNPC
{
    public override bool AppliesToEntity(NPC npc, bool lateInstantiation) => npc.type == NPCID.Mechanic || npc.type == NPCID.Cyborg || npc.type == NPCID.Painter
        || npc.type == NPCID.Clothier;

    public override void SetStaticDefaults()
    {
        NPCHappiness.Get(NPCID.Mechanic)
            .SetBiomeAffection<ArborBiome>(AffectionLevel.Like);

        NPCHappiness.Get(NPCID.Cyborg)
            .SetBiomeAffection<ArborBiome>(AffectionLevel.Like);

        NPCHappiness.Get(NPCID.Painter)
            .SetBiomeAffection<ArborBiome>(AffectionLevel.Dislike);

        NPCHappiness.Get(NPCID.Clothier)
            .SetBiomeAffection<ArborBiome>(AffectionLevel.Dislike);
    }

    public override void GetChat(NPC npc, ref string chat)
    {
        if (Main.rand.NextBool(6))
        {
            if (npc.type == NPCID.Mechanic)
                chat = Language.GetTextValue("Mods.Arbour.Dialogue.Mechanic");
            else if (npc.type == NPCID.Cyborg)
                chat = Language.GetTextValue("Mods.Arbour.Dialogue.Cyborg");
            else if (npc.type == NPCID.Painter)
                chat = Language.GetTextValue("Mods.Arbour.Dialogue.Painter");
            else if (npc.type == NPCID.Clothier)
                chat = Language.GetTextValue("Mods.Arbour.Dialogue.Clothier");
        }
    }
}
