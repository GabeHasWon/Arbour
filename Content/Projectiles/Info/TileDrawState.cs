using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arbour.Content.Projectiles.Info
{
    internal class TileDrawState
    {
        public Rectangle Source => new Rectangle(frame.X, frame.Y, overrideSize.HasValue ? overrideSize.Value.X : 16, overrideSize.HasValue ? overrideSize.Value.Y : 16);

        public Point frame = Point.Zero;
        public Point offsetFromOrigin = Point.Zero;
        public ushort tileID = TileID.Dirt;
        public string overrideTex = null;
        public Point? overrideSize = null;

        private Asset<Texture2D> asset = null;

        public TileDrawState(Point offsetFromOrigin, Point frame, ushort tileID)
        {
            this.offsetFromOrigin = offsetFromOrigin;
            this.frame = frame;
            this.tileID = tileID;
        }

        /// <summary>Automatically loads and returns the desired asset.</summary>
        /// <returns>The tile's asset or custom asset if using <see cref="overrideTex"/>.</returns>
        public Texture2D GetTexture()
        {
            if (asset is null)
            {
                if (overrideTex is null)
                    asset = TextureAssets.Tile[tileID];
                else
                    asset = ModContent.Request<Texture2D>(overrideTex);
            }

            return asset.Value;
        }
    }
}