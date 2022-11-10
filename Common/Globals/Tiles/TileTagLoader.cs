using Arbour.Content.Tiles;
using System.Linq;
using System;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Arbour.Common.Globals.Tiles
{
    internal class TileTagLoader : ILoadable
    {
        public Dictionary<int, TileTagAttribute> AttributesByID = new();

        public void Load(Mod mod)
        {
            var types = GetType().Assembly.GetTypes().Where(x => !x.IsAbstract && Attribute.IsDefined(x, typeof(TileTagAttribute)) && typeof(ModTile).IsAssignableFrom(x));

            foreach (var item in types)
            {
                var attr = Attribute.GetCustomAttribute(item, typeof(TileTagAttribute)) as TileTagAttribute;
                int tileType = ModLoader.GetMod("Arbour").Find<ModTile>(item.Name).Type;

                AttributesByID.Add(tileType, attr);
            }
        }

        public void Unload()
        {
            AttributesByID.Clear();
        }
    }
}
