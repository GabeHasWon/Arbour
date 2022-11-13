using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Arbour.Content.Tiles.Multitiles;

class Hay1x1 : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = false;
        Main.tileLavaDeath[Type] = false;
        Main.tileFrameImportant[Type] = true;

        TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Plant"]);
        TileID.Sets.SwaysInWindBasic[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.newTile.RandomStyleRange = 5;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.AnchorValidTiles = TileSets.ArborPlantAnchors;
        TileObjectData.addTile(Type);
    }
}