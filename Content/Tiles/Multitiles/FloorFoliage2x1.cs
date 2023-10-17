using Arbour.Content.Items.Placeable;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Arbour.Content.Tiles.Multitiles;

class FloorFoliage2x1 : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = false;
        Main.tileLavaDeath[Type] = true;
        Main.tileFrameImportant[Type] = true;

        TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Plant"]);
        TileID.Sets.SwaysInWindBasic[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
        TileObjectData.newTile.CoordinateHeights = new int[1] { 18 };
        TileObjectData.newTile.StyleHorizontal = false;
        TileObjectData.newTile.AnchorValidTiles = TileSets.ArborPlantAnchors;
        TileObjectData.addTile(Type);

        AddMapEntry(new Microsoft.Xna.Framework.Color(243, 133, 54));

        HitSound = SoundID.Grass;
    }
}

class RubbleFloorFoliage2x1 : FloorFoliage2x1
{
    public override string Texture => base.Texture.Replace("Rubble", "");

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        FlexibleTileWand.RubblePlacementMedium.AddVariations(ModContent.ItemType<ArborGrassSeeds>(), Type, 0, 1, 2);
        RegisterItemDrop(ModContent.ItemType<ArborGrassSeeds>());
    }
}