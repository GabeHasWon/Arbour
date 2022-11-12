using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Tiles.Blocks;

[TileTag(TileTags.NeedsTopAnchor)]
internal class ArborVines : ModTile
{
    public override void SetStaticDefaults()
    {
        DustType = DustID.Pumpkin;
        HitSound = SoundID.Grass;

        Main.tileSolid[Type] = false;
        Main.tileCut[Type] = true;
        Main.tileFrameImportant[Type] = true;

        AddMapEntry(Color.OrangeRed);
    }

    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        Tile tile = Main.tile[i, j];
        tile.TileFrameY = (short)(Main.rand.Next(3) * 18);

        if (!Main.tile[i, j + 1].HasTile)
            tile.TileFrameY = 3 * 18;

        return false;
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        fail = false;

        Tile below = Main.tile[i, j + 1];
        if (below.HasTile && below.TileType == Type)
            WorldGen.KillTile(i, j + 1);
    }
}
