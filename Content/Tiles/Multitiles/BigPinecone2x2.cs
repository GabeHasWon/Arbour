using Arbour.Content.Items.Placeable;
using Arbour.Content.Tiles.Blocks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Arbour.Content.Tiles.Multitiles;

class BigPinecone2x2 : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = false;
        Main.tileLavaDeath[Type] = false;
        Main.tileFrameImportant[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
        TileObjectData.newTile.AnchorValidTiles = TileSets.ArborPlantAnchors;
        TileObjectData.addTile(Type);

        AddMapEntry(new Microsoft.Xna.Framework.Color(94, 42, 24));
    }
}

class BigRubblePinecone2x2 : BigPinecone2x2
{
    public override string Texture => base.Texture.Replace("Rubble", "");

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<BirchWoodBlock>(), Type, 0, 1);
        RegisterItemDrop(ModContent.ItemType<BirchWoodBlock>());
    }
}