using Terraria.ModLoader;

namespace Arbour;

public class Arbour : Mod
{
    public override void Load()
    {
        NPCUtils.NPCUtils.AutoloadModBannersAndCritters(this);
        NPCUtils.NPCUtils.TryLoadBestiaryHelper();
    }

    public override void Unload()
    {
        NPCUtils.NPCUtils.UnloadMod(this);
        NPCUtils.NPCUtils.UnloadBestiaryHelper();
    }
}