using Arbour.Content.Tiles.Blocks;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Capture;
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
        if (ModContent.GetInstance<ArborConfig>().CustomMusic)
            return MusicLoader.GetMusicSlot(Mod, "Assets/Music/Day");
        return -1;
    }

    public override string BestiaryIcon => $"Arbour/Assets/Misc/{nameof(ArborBiome)}_Icon";
    public override string BackgroundPath => MapBackground;
    public override Color? BackgroundColor => Color.Orange;
    public override string MapBackground => $"Arbour/Assets/Misc/{nameof(ArborBiome)}_Map";
    public override bool IsBiomeActive(Player player) => ArborTileCount.ArborCount >= 40 && (player.ZoneSkyHeight || player.ZoneOverworldHeight);
}

public class ArborTileCount : ModSystem
{
    public static int ArborCount => ModContent.GetInstance<ArborTileCount>().arborCount;

    public int arborCount;

    public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts) => arborCount = tileCounts[ModContent.TileType<ArborGrass>()];
}