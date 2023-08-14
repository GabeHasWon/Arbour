using Arbour.Content.Tiles.Blocks;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

namespace Arbour;

internal class ArborBiome : ModBiome
{
    // public override void SetStaticDefaults() => DisplayName.SetDefault("Arbour");
    public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("Arbour/ArborWaterStyle");
    public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Normal;
    public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

    public override int Music => GetMusic();

    private int GetMusic() => MusicLoader.GetMusicSlot(Mod, "Assets/Music/Day");

    public override string BestiaryIcon => $"Arbour/Assets/Misc/{nameof(ArborBiome)}_Icon";
    public override string BackgroundPath => MapBackground;
    public override Color? BackgroundColor => Color.Orange;
    public override string MapBackground => $"Arbour/Assets/Misc/{nameof(ArborBiome)}_Map";
    public override bool IsBiomeActive(Player player) => ModContent.GetInstance<ArborTileCount>().arborCount >= 40 && (player.ZoneSkyHeight || player.ZoneOverworldHeight);
}

public class ArborTileCount : ModSystem
{
    public int arborCount;

    public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts) => arborCount = tileCounts[ModContent.TileType<ArborGrass>()];
}