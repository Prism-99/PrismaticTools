using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PrismaticTools.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Internal;
using StardewValley.Tools;

namespace PrismaticTools;

public class ModEntry : Mod
{
	public static IModHelper ModHelper;

	public static ModConfig Config;

	public static Texture2D ToolsTexture;

	private int colorCycleIndex;

	private readonly List<Color> colors = new List<Color>();

	private AssetEditor AssetEditor;

	public override void Entry(IModHelper helper)
	{
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Expected O, but got Unknown
		ModHelper = helper;
		AssetEditor = new AssetEditor();
		Config = Helper.ReadConfig<ModConfig>();
		//ModHelper.Translation.();
		ToolsTexture = ModHelper.ModContent.Load<Texture2D>("assets/tools.png");
		IRawTextureData barTexture = ModHelper.ModContent.Load<IRawTextureData>("assets/prismaticBar.png");
		IRawTextureData sprinklerTexture = ModHelper.ModContent.Load<IRawTextureData>("assets/prismaticSprinkler.png");
		helper.ConsoleCommands.Add("ptools", "Upgrade all tools to prismatic", UpgradeTools);
		helper.Events.GameLoop.SaveLoaded += OnSaveLoaded;
		helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
		helper.Events.Player.InventoryChanged += OnInventoryChanged;
		helper.Events.Content.AssetRequested += OnAssetRequested;
		helper.Events.GameLoop.GameLaunched += OnGameLauched;
		helper.Events.Content.LocaleChanged += OnLocaleChanged;
		InitColors();
        Harmony harmony = new Harmony("iargue.PrismaticTools");
		ApplyPatches(harmony);
	}

	private void ApplyPatches(Harmony harmony)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected O, but got Unknown
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		harmony.Patch((MethodBase)AccessTools.Method(typeof(StardewValley.Object), "GetBaseRadiusForSprinkler", (Type[])null, (Type[])null), (HarmonyMethod)null, new HarmonyMethod(typeof(PrismaticPatches), "After_Object_GetBaseRadiusForSprinkler", (Type[])null), (HarmonyMethod)null, (HarmonyMethod)null);
		harmony.Patch((MethodBase)AccessTools.Method(typeof(StardewValley.Object), "updateWhenCurrentLocation", (Type[])null, (Type[])null), new HarmonyMethod(typeof(PrismaticPatches), "Object_UpdatingWhenCurrentLocation", (Type[])null), (HarmonyMethod)null, (HarmonyMethod)null, (HarmonyMethod)null);
		harmony.Patch((MethodBase)AccessTools.Method(typeof(StardewValley.Object), "placementAction", (Type[])null, (Type[])null), new HarmonyMethod(typeof(PrismaticPatches), "Object_OnPlacing", (Type[])null), (HarmonyMethod)null, (HarmonyMethod)null, (HarmonyMethod)null);
		harmony.Patch((MethodBase)AccessTools.Method(typeof(ShopBuilder), "GetToolUpgradeConventionalPrice", (Type[])null, (Type[])null), (HarmonyMethod)null, new HarmonyMethod(typeof(PrismaticPatches), "After_ShopBuilder_GetToolUpgradeConventionalPrice", (Type[])null), (HarmonyMethod)null, (HarmonyMethod)null);
		harmony.Patch((MethodBase)AccessTools.Method(typeof(ShopBuilder), "GetToolUpgradeConventionalTradeItem", (Type[])null, (Type[])null), (HarmonyMethod)null, new HarmonyMethod(typeof(PrismaticPatches), "After_ShopBuilder_GetToolUpgradeConventionalTradeItem", (Type[])null), (HarmonyMethod)null, (HarmonyMethod)null);
		harmony.Patch((MethodBase)AccessTools.Method(typeof(Tool), "tilesAffected", (Type[])null, (Type[])null), (HarmonyMethod)null, new HarmonyMethod(typeof(PrismaticPatches), "Tool_TilesAffected_Postfix", (Type[])null), (HarmonyMethod)null, (HarmonyMethod)null);
	}

	private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		if (!e.IsMultipleOf(8u))
		{
			return;
		}
		Farmer farmer = Game1.player;
		Item item;
		try
		{
			item = farmer.Items[farmer.CurrentToolIndex];
		}
		catch (ArgumentOutOfRangeException)
		{
			return;
		}
		if (!(item is StardewValley.Object obj) || obj.QualifiedItemId != "(O)PrismaticBar")
		{
			return;
		}
		foreach (LightSource light in farmer.currentLocation.sharedLights.Values)
		{
			if (light.Id == farmer.UniqueMultiplayerID.ToString())
			{
				light.color.Value = colors[colorCycleIndex];
			}
		}
		colorCycleIndex = (colorCycleIndex + 1) % colors.Count;
	}

	public override object GetApi()
	{
		return new PrismaticAPI();
	}

	private void UpgradeTools(string command, string[] args)
	{
		foreach (Item item in Game1.player.Items)
		{
			if ((item is Axe || item is WateringCan || item is Pickaxe || item is Hoe) && (item as Tool).UpgradeLevel != 5)
			{
				Tool t = item as Tool;
				int upgrades = 5 - t.UpgradeLevel;
				t.InitialParentTileIndex += ((upgrades >= 3) ? (7 * upgrades + 21) : (7 * upgrades));
				t.UpgradeLevel = 24;
				if (item is Axe)
				{
					t.ItemId = "PrismaticAxe";
				}
				else if (item is WateringCan)
				{
					t.ItemId = "PrismaticWateringCan";
				}
				else if (item is Pickaxe)
				{
					t.ItemId = "PrismaticPickaxe";
				}
				else if (item is Hoe)
				{
					t.ItemId = "PrismaticHoe";
				}
			}
		}
	}

	private void AddLightsToInventoryItems()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		if (!Config.UseSprinklersAsLamps)
		{
			return;
		}
		foreach (Item item in Game1.player.Items)
		{
			if (item is StardewValley.Object obj)
			{
				if (obj.QualifiedItemId == "(O)PrismaticSprinkler")
				{
					obj.lightSource = new LightSource("PrismaticSprinkler"+Game1.timeOfDay.ToString(), 5, Vector2.Zero, 2f, new Color(0f, 0f, 0f), LightSource.LightContext.None, 0L);
				}
				else if (obj.QualifiedItemId == "(O)PrismaticBar")
				{
					obj.lightSource = new LightSource("PrismaticBar" + Game1.timeOfDay.ToString(), 5, Vector2.Zero, 1f, colors[colorCycleIndex], LightSource.LightContext.None, 0L);
				}
			}
		}
	}

	private void OnInventoryChanged(object sender, InventoryChangedEventArgs e)
	{
		if (e.IsLocalPlayer)
		{
			AddLightsToInventoryItems();
		}
	}

	private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
	{
		if (Game1.player.FarmingLevel >= 9)
		{
			try
			{
				Game1.player.craftingRecipes.Add("Prismatic Sprinkler", 0);
			}
			catch
			{
			}
		}
		Utility.ForEachItemContext(delegate( in ForEachItemContext context)
		{
			if (context.Item.QualifiedItemId == "(O)1112")
			{
				Item obj3 = ItemRegistry.Create("(O)PrismaticBar", context.Item.Stack, context.Item.Quality);
				context.ReplaceItemWith(obj3);
			}
			return true;
		});
		Utility.ForEachItemContext(delegate(in ForEachItemContext context)
		{
			if (context.Item.QualifiedItemId == "(O)1113")
			{
				Item obj2 = ItemRegistry.Create("(O)PrismaticSprinkler", context.Item.Stack, context.Item.Quality);
                context.ReplaceItemWith(obj2);
			}
			return true;
		});
		Utility.ForEachItem(delegate(Item item)
		{
			if (item.QualifiedItemId == "(O)OldId")
			{
			}
			return true;
		});
	}

	private void OnAssetRequested(object sender, AssetRequestedEventArgs e)
	{
		AssetEditor.OnAssetRequested(e);
	}

	private void OnGameLauched(object sender, GameLaunchedEventArgs e)
	{
		ModHelper.GameContent.InvalidateCache("Data/Objects");
	}

	private void OnLocaleChanged(object? sender, LocaleChangedEventArgs e)
	{
		Helper.GameContent.InvalidateCache("Data/Objects");
		Helper.GameContent.InvalidateCache("Data/Tools");
	}

	private void InitColors()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		int j = 24;
		for (int i = 0; i < j; i++)
		{
			colors.Add(ColorFromHSV(360.0 * (double)i / (double)j, 1.0, 1.0));
		}
	}

	private Color ColorFromHSV(double hue, double saturation, double value)
	{
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		int hi = Convert.ToInt32(Math.Floor(hue / 60.0)) % 6;
		double f = hue / 60.0 - Math.Floor(hue / 60.0);
		value *= 255.0;
		int v = Convert.ToInt32(value);
		int p = Convert.ToInt32(value * (1.0 - saturation));
		int q = Convert.ToInt32(value * (1.0 - f * saturation));
		int t = Convert.ToInt32(value * (1.0 - (1.0 - f) * saturation));
		v = 255 - v;
		p = 255 - v;
		q = 255 - q;
		t = 255 - t;
		return (Color)(hi switch
		{
			0 => new Color(v, t, p), 
			1 => new Color(q, v, p), 
			2 => new Color(p, v, t), 
			3 => new Color(p, q, v), 
			4 => new Color(t, p, v), 
			_ => new Color(v, p, q), 
		});
	}
}
