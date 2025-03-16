using Arbour.Content.Tiles.Blocks;
using Arbour.Content.Tiles.Custom;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Arbour.Content.Tiles.Multitiles;

class MicrobirchSapling : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = false;
        Main.tileLavaDeath[Type] = false;
        Main.tileFrameImportant[Type] = true;

        TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Plant"]);
        TileID.Sets.SwaysInWindBasic[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.newTile.CoordinateHeights = [16];
        TileObjectData.newTile.RandomStyleRange = 3;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.AnchorValidTiles = [ModContent.TileType<ArborGrass>(), ModContent.TileType<ArborLeaf>()];
        TileObjectData.newTile.AnchorBottom = default;
        TileObjectData.newTile.AnchorTop = new Terraria.DataStructures.AnchorData(AnchorType.SolidBottom | AnchorType.SolidTile, 1, 0);
        TileObjectData.addTile(Type);

        LocalizedText translation = CreateMapEntryName();
        AddMapEntry(new Microsoft.Xna.Framework.Color(132, 139, 235), translation);
    }

    public override void RandomUpdate(int i, int j)
    {
        if (Main.rand.NextBool(3))
        {
            Tile tile = Main.tile[i, j];
            tile.HasTile = false;

            Microbirch.SpawnAt(i, j);
        }
    }
}