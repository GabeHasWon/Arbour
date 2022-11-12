using Arbour.Content.Tiles;
using System.Linq;
using System;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Arbour.Common.Globals.Tiles
{
    internal class TileTagLoader : ModSystem
    {
        public Dictionary<int, TileTagAttribute> AttributesByID = new();

        public static Action<TileTagLoader> OnLoad = null;

        public override void PostSetupContent()
        {
            var types = GetType().Assembly.GetTypes().Where(x => !x.IsAbstract && Attribute.IsDefined(x, typeof(TileTagAttribute)) && typeof(ModTile).IsAssignableFrom(x));

            foreach (var item in types)
            {
                var attr = Attribute.GetCustomAttribute(item, typeof(TileTagAttribute)) as TileTagAttribute;
                int tileType = Mod.Find<ModTile>(item.Name).Type;

                AttributesByID.Add(tileType, attr);
            }

            OnLoad?.Invoke(this);
        }
    }
}
