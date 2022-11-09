using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Tiles.Blocks;

internal class Pleinglass : ModTile
{
    public override void Load()
    {
        On.Terraria.Player.Update += Player_Update;
    }

    private void Player_Update(On.Terraria.Player.orig_Update orig, Player self, int i)
    {
        Main.tileSolid[Type] = false;
        orig(self, i);
        Main.tileSolid[Type] = true;
    }

    public override void SetStaticDefaults()
    {
        DustType = DustID.Glass;
        HitSound = SoundID.Shatter;
        //ItemDrop = drop;

        Main.tileSolid[Type] = true;
        TileID.Sets.BlocksWaterDrawingBehindSelf[Type] = false;

        AddMapEntry(Color.AliceBlue);
    }

    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        Tile tile = Main.tile[i, j];
        tile.TileFrameX = (short)(Main.rand.Next(4) * 18);
        return false;
    }
}
