using Arbour.Content.Items.Placeable;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Arbour.Content.Tiles.Multitiles;

class PineconeSmall : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = false;
        Main.tileLavaDeath[Type] = false;
        Main.tileFrameImportant[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.AnchorValidTiles = TileSets.ArborPlantAnchors;
        TileObjectData.addTile(Type);

        AddMapEntry(new Microsoft.Xna.Framework.Color(94, 42, 24));
    }
}

class RubblePineconeSmall : PineconeSmall
{
    public override string Texture => base.Texture.Replace("Rubble", "");

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        FlexibleTileWand.RubblePlacementSmall.AddVariations(ModContent.ItemType<BirchWoodBlock>(), Type, 0, 1, 2);
        RegisterItemDrop(ModContent.ItemType<BirchWoodBlock>());
    }
}