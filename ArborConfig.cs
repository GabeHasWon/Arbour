using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Arbour;

public class ArborConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    [DefaultValue(true)]
    public bool NewMusic { get; set; }

    [DefaultValue(false)]
    public bool NoMusic { get; set; }
}