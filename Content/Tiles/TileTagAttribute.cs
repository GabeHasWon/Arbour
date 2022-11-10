using System;

namespace Arbour.Content.Tiles
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class TileTagAttribute : Attribute
    {
        public TileTags[] Tags { get; set; }

        public TileTagAttribute(params TileTags[] tags)
        {
            Tags = tags;
        }
    }

    public enum TileTags
    {
        NeedsTopAnchor
    }
}
