using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Chat.Commands;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace Arbour.Common;

internal class ArborBackground : CustomSky
{
    private readonly struct ArborIsland
    {
        public readonly Vector2 Position;
        public readonly int Type;
        public readonly float Depth;

        public ArborIsland(Vector2 position)
        {
            Position = position;
            Type = Main.rand.Next(4);
            Depth = Main.rand.NextFloat(1.2f, 2.5f);
        }

        public override string ToString() => $"Pos: {Position}, Type: {Type}, Depth: {Depth}";
    }

    private bool _isActive;

    Asset<Texture2D>[] _backgrounds;
    List<ArborIsland> _islands = null;

    public override void OnLoad()
    {
        _backgrounds = new Asset<Texture2D>[4];

        for (int i = 0; i < 4; ++i)
            _backgrounds[i] = ModContent.Request<Texture2D>("Arbour/Assets/Misc/ArborIsland" + i);
    }

    public override void Update(GameTime gameTime)
    {
        if (_isActive)
            Opacity = MathHelper.Lerp(Opacity, 1f, 0.05f);
        else
            Opacity = MathHelper.Lerp(Opacity, 0f, 0.08f);

        if (Opacity < 0.01f)
            Opacity = 0f;
    }

    public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
        if (maxDepth >= 0f && minDepth < 0f)
        {
            if (Opacity <= 0)
                return;

            if (_islands is null)
                InitializePositions();

            Rectangle view = new((int)Main.Camera.UnscaledPosition.X, (int)Main.Camera.UnscaledPosition.Y, Main.screenWidth, Main.screenHeight);
            view.Inflate(Main.screenWidth, Main.screenHeight);
            var screenPositions = _islands.Where(x => view.Contains(GetDrawPositionByDepth(x.Position, x.Depth).ToPoint()));

            if (!screenPositions.Any())
                return;

            foreach (var island in screenPositions)
            {
                Vector2 pos = GetDrawPositionByDepth(island.Position, island.Depth);
                Color color = GetColor(Main.ColorOfTheSkies, island.Depth);
                spriteBatch.Draw(_backgrounds[island.Type].Value, pos - Main.Camera.UnscaledPosition, color);
            }
        }
    }

    private void InitializePositions()
    {
        int repeats = Main.maxTilesX / 4200 * 160;

        _islands = new(repeats);

        for (int i = 0; i < repeats; i++)
        {
            float frac = (float)i / repeats;
            float x = Main.maxTilesX * frac;
            var pos = (new Vector2(x, Main.rand.Next((int)(Main.worldSurface * 0.14f), (int)(Main.worldSurface * 0.43f))) * 16).Floor();

            _islands.Add(new ArborIsland(pos));
        }

        _islands = _islands.OrderByDescending(x => x.Depth).ToList();
    }

    internal Color GetColor(Color backgroundColor, float depth)
    {
        float modAtmo = 1f;

        if (Main.atmo < 0.125f)
            modAtmo = Main.atmo * 8;

        Color c = Color.Lerp(backgroundColor, Color.White, MathHelper.Clamp(2 - depth, 0, 1)) * modAtmo * Opacity;
        return c;
    }

    internal static Vector2 GetDrawPositionByDepth(Vector2 p, float d) => (p - Main.Camera.Center) * new Vector2(1f / d, 0.9f / d) + Main.Camera.Center;
    public override void Activate(Vector2 position, params object[] args) => _isActive = true;
    public override void Deactivate(params object[] args) => _isActive = false;
    public override float GetCloudAlpha() => 1f;
    public override void Reset() => _isActive = false;
    public override bool IsActive() => _isActive || Opacity > 0;
}
