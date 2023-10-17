using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Walls;

public class BirchFenceWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        WallID.Sets.AllowsPlantsToGrow[Type] = true;

        DustType = DustID.Pumpkin;
        AddMapEntry(new Color(230, 230, 235) * 0.4f);
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
}