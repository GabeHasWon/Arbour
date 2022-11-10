using Arbour.Content.Tiles;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Arbour.Common.Globals.Tiles
{
    internal class AnchorDrawTile : GlobalTile
    {
        private List<int> anchorTiles = new List<int>();

        public override void Load()
        {
            var attributes = ModContent.GetInstance<TileTagLoader>().AttributesByID.Where(x => x.Value.Tags.Contains(TileTags.NeedsTopAnchor));

            foreach (var pair in attributes)
                anchorTiles.Add(pair.Key);
        }

        public override bool PreDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            int below = Main.tile[i, j + 1].TileType;
            if (anchorTiles.Contains(below) && below != type)
            {

            }
            return true;
        }
    }
}
