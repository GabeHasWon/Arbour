using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Tiles.Blocks;

internal class Pleinglass : ModTile
{
    public override void Load() => Terraria.On_Player.Update += Player_Update;
    public override bool IsLoadingEnabled(Mod mod) => false;

    private void Player_Update(Terraria.On_Player.orig_Update orig, Player self, int i)
    {
        Main.tileSolid[Type] = false;
        orig(self, i);
        Main.tileSolid[Type] = true;
    }

    public override void SetStaticDefaults()
    {
        DustType = DustID.Glass;
        HitSound = SoundID.Shatter;

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
