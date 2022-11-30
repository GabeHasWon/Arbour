using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Arbour.Content.Tiles.Multitiles;

class Hay1x2 : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = false;
        Main.tileLavaDeath[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileCut[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
        TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
        TileObjectData.newTile.RandomStyleRange = 2;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.AnchorValidTiles = TileSets.ArborPlantAnchors;
        TileObjectData.addTile(Type);

        AddMapEntry(new Microsoft.Xna.Framework.Color(181, 152, 90));
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY) => HayCommon.TryDropSeeds(i, j, 12, 32);
}