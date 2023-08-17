using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Arbour.Content.Tiles.Banners;

public class BaseBannerTile : ModTile
{
    public readonly int NPCType;
    public readonly string InternalName;

    public override string Name => InternalName;

    public BaseBannerTile()
    {
        NPCType = -1;
        InternalName = "";
    }

    public BaseBannerTile(int npcType, string internalName)
    {
        NPCType = npcType;
        InternalName = internalName;
    }

    public override bool IsLoadingEnabled(Mod mod) => NPCType != -1;

    public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileLavaDeath[Type] = true;

		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
		TileObjectData.addTile(Type);

		TileID.Sets.DisableSmartCursor[Type] = true;

		AddMapEntry(new Color(13, 88, 130));
	
		DustType = -1;
    }

    public override void NearbyEffects(int i, int j, bool closer)
	{
		Main.SceneMetrics.NPCBannerBuff[NPCType] = true;
		Main.SceneMetrics.hasBanner = true;
	}

	public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
	{
		if (i % 2 == 1)
			spriteEffects = SpriteEffects.FlipHorizontally;
	}
}

public class BaseBannerItem : ModItem
{
    readonly string InternalName;
    readonly int PlaceID;
    readonly int NPCType;

    protected override bool CloneNewInstances => true;
    public override string Name => InternalName;
    public override LocalizedText Tooltip => Language.GetText("CommonItemTooltip.BannerBonus");

    public BaseBannerItem()
    {
        InternalName = "";
        PlaceID = TileID.Dirt;
        NPCType = NPCID.None;
    }

    public BaseBannerItem(string internalName, int placeID, int npcType)
    {
        InternalName = internalName;
        PlaceID = placeID;
        NPCType = npcType;
    }


    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var tooltip = tooltips.First(x => x.Name == "Tooltip0");
        tooltip.Text += Lang.GetNPCName(NPCType);
    }

    public override bool IsLoadingEnabled(Mod mod) => PlaceID != TileID.Dirt;

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(PlaceID);
        Item.Size = new Vector2(12, 30);
        Item.rare = ItemRarityID.Blue;
    }
}

public class BannerLoader : ModSystem
{
    static Hook setDefaultsDetour;

    public override void Load()
    {
        var types = Mod.GetType().Assembly.GetTypes().Where(x => !x.IsAbstract && typeof(ModNPC).IsAssignableFrom(x) && Attribute.IsDefined(x, typeof(AutoloadBannerAttribute)));

        foreach (var type in types)
        {
            var banner = type.GetCustomAttribute(typeof(AutoloadBannerAttribute));
            var npc = Mod.Find<ModNPC>(type.Name);
            
            Mod.AddContent(new BaseBannerTile(npc.Type, npc.Name + "Banner"));
            Mod.AddContent(new BaseBannerItem(npc.Name + "BannerItem", Mod.Find<ModTile>(npc.Name + "Banner").Type, npc.Type));
        }

        setDefaultsDetour = new Hook(typeof(NPCLoader).GetMethod("SetDefaults", BindingFlags.Static | BindingFlags.NonPublic), SetBannerIfAutoloaded);
        setDefaultsDetour.Apply();
    }

    public override void Unload()
    {
        setDefaultsDetour.Undo();
        setDefaultsDetour.Dispose();
    }

    private void SetBannerIfAutoloaded(Action<NPC, bool> orig, NPC self, bool createModNPC)
    {
        orig(self, createModNPC);

        if (self.ModNPC is not null && Attribute.IsDefined(self.ModNPC.GetType(), typeof(AutoloadBannerAttribute)))
        {
            self.ModNPC.Banner = self.type;
            self.ModNPC.BannerItem = self.ModNPC.Mod.Find<ModItem>(self.ModNPC.Name + "BannerItem").Type;
        }
    }
}