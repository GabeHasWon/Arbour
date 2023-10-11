using Microsoft.Xna.Framework;

namespace Arbour.Content.Tiles.Multitiles.FurnitureHelpers;

public readonly struct StaticTileInfo
{
    public readonly string MapKeyName;
    public readonly int[] AdjTypes;

    public StaticTileInfo(string mapKeyName, params int[] adjTypes)
    {
        MapKeyName = mapKeyName;
        AdjTypes = adjTypes;
    }
}

public readonly struct SpecificTileInfo
{
    public readonly int Drop;
    public readonly int DustType;
    public readonly Color MapColor;

    public SpecificTileInfo(int drop, int dustType, Color mapColor)
    {
        Drop = drop;
        DustType = dustType;
        MapColor = mapColor;
    }
}