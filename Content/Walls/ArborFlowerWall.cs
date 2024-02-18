using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Walls;

public class ArborFlowerWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        WallID.Sets.AllowsPlantsToGrow[Type] = true;

        DustType = DustID.Pumpkin;
        AddMapEntry(new Color(51, 12, 20));
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
}

public class ArborFlowerWall_Unsafe : ModWall
{
    public override string Texture => base.Texture.Replace("_Unsafe", "");

    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = false;
        DustType = DustID.Pumpkin;
        AddMapEntry(new Color(56, 42, 27));
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
}