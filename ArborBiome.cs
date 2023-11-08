using Arbour.Common;
using Arbour.Content.Tiles.Blocks;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Arbour;

internal class ArborBiome : ModBiome
{
    public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("Arbour/ArborWaterStyle");
    public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Normal;
    public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

    public override int Music => GetMusic();

    private int GetMusic()
    {
        if (ModContent.GetInstance<ArborConfig>().NewMusic)
            return MusicLoader.GetMusicSlot(Mod, "Assets/Music/Day");
        return MusicLoader.GetMusicSlot(Mod, "Assets/Music/DayOld");
    }

    public override void Load()
    {
        if (!Main.dedServ)
        {
            SkyManager.Instance["Arbor:ArborBackground"] = new ArborBackground();
            Filters.Scene["Arbor:ArborBackground"] = new Filter(new PuritySpiritScreenShaderData("FilterMiniTower").UseColor(0.4f, 0.9f, 0.4f).UseOpacity(0f), EffectPriority.VeryHigh);
        }
    }

    public override string BestiaryIcon => $"Arbour/Assets/Misc/{nameof(ArborBiome)}_Icon";
    public override string BackgroundPath => MapBackground;
    public override Color? BackgroundColor => Color.Orange;
    public override string MapBackground => $"Arbour/Assets/Misc/{nameof(ArborBiome)}_Map";

    public override bool IsBiomeActive(Player player) => ArborTileCount.ArborCount >= 40 && (player.ZoneSkyHeight || player.ZoneOverworldHeight);

    public override void SpecialVisuals(Player player, bool isActive)
    {
        player.ManageSpecialBiomeVisuals("Arbor:ArborBackground", isActive);
    }
}

public class ArborTileCount : ModSystem
{
    public static int ArborCount => ModContent.GetInstance<ArborTileCount>().arborCount;

    public int arborCount;

    public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts) => arborCount = tileCounts[ModContent.TileType<ArborGrass>()];
}

public class PuritySpiritScreenShaderData : ScreenShaderData
{
    public PuritySpiritScreenShaderData(string passName)
        : base(passName)
    {
    }

    public override void Apply()
    {
        base.Apply();
    }
}