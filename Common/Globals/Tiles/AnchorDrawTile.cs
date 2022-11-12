using Arbour.Content.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Arbour.Common.Globals.Tiles
{
    internal class AnchorDrawTile : GlobalTile
    {
        private List<int> anchorTiles = new List<int>();

        public override void Load()
        {
            TileTagLoader.OnLoad += (loader) =>
            {
                var attributes = loader.AttributesByID.Where(x => x.Value.Tags.Contains(TileTags.NeedsTopAnchor));

                foreach (var pair in attributes)
                    anchorTiles.Add(pair.Key);
            };
        }

        public override bool PreDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            int below = Main.tile[i, j + 1].TileType;

            if (anchorTiles.Contains(below) && below != type)
            {
                Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
                spriteBatch.Draw(TextureAssets.Tile[below].Value, new Vector2(i, j) * 16 - Main.screenPosition + zero, new Rectangle(0, 0, 16, 16), Lighting.GetColor(i, j));
            }
            return true;
        }
    }
}
