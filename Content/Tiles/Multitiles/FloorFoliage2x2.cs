using Arbour.Content.Items.Placeable;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Arbour.Content.Tiles.Multitiles;

class FloorFoliage2x2 : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = false;
        Main.tileLavaDeath[Type] = true;
        Main.tileFrameImportant[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
        TileObjectData.newTile.AnchorValidTiles = TileSets.ArborPlantAnchors;
        TileObjectData.addTile(Type);

        AddMapEntry(new Microsoft.Xna.Framework.Color(243, 133, 54));

        HitSound = Terraria.ID.SoundID.Grass;
    }
}

class RubbleFloorFoliage2x2 : FloorFoliage2x2
{
    public override string Texture => base.Texture.Replace("Rubble", "");

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<ArborGrassSeeds>(), Type, 0, 1, 2);
        RegisterItemDrop(ModContent.ItemType<ArborGrassSeeds>());
    }
}