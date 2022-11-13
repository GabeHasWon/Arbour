using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Arbour.Content.Tiles.Multitiles;

class Hay1x3 : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = false;
        Main.tileLavaDeath[Type] = false;
        Main.tileFrameImportant[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
        TileObjectData.newTile.Height = 3;
        TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
        TileObjectData.newTile.RandomStyleRange = 3;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.AnchorValidTiles = TileSets.ArborPlantAnchors;
        TileObjectData.addTile(Type);
    }
}