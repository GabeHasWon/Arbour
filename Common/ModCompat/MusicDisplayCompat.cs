using Terraria.Localization;
using Terraria.ModLoader;

namespace Arbour.Common.ModCompat;

internal class MusicDisplayCompat : ModSystem
{
    public override void PostSetupContent()
    {
        if (!ModLoader.TryGetMod("MusicDisplay", out Mod display))
            return;

        LocalizedText modName = Language.GetText("Mods.Arbour.MusicDisplay.ModName");

        void AddMusic(string path, string name)
        {
            LocalizedText author = Language.GetText("Mods.Arbour.MusicDisplay." + name + ".Author");
            LocalizedText displayName = Language.GetText("Mods.Arbour.MusicDisplay." + name + ".DisplayName");
            display.Call("AddMusic", (short)MusicLoader.GetMusicSlot(Mod, path), displayName, author, modName);
        }

        AddMusic("Assets/Music/Day", "Day");
    }
}
