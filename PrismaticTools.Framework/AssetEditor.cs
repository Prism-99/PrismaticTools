using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley.GameData.Machines;
using StardewValley.GameData.Objects;
using StardewValley.GameData.Tools;

namespace PrismaticTools.Framework;

public class AssetEditor
{
	private string barName => ModEntry.ModHelper.Translation.Get("prismaticBar.name");

	private string barDesc => ModEntry.ModHelper.Translation.Get("prismaticBar.description");

	private string sprinklerName => ModEntry.ModHelper.Translation.Get("prismaticSprinkler.name");

	private string sprinklerDesc => ModEntry.ModHelper.Translation.Get("prismaticSprinkler.description");

	public void OnAssetRequested(AssetRequestedEventArgs e)
	{
		if (e.NameWithoutLocale.IsEquivalentTo("Data/Objects"))
		{
			e.Edit(delegate(IAssetData asset)
			{
				//IL_000d: Unknown result type (might be due to invalid IL or missing references)
				//IL_0012: Unknown result type (might be due to invalid IL or missing references)
				//IL_001e: Unknown result type (might be due to invalid IL or missing references)
				//IL_002a: Unknown result type (might be due to invalid IL or missing references)
				//IL_0036: Unknown result type (might be due to invalid IL or missing references)
				//IL_0041: Unknown result type (might be due to invalid IL or missing references)
				//IL_004c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0054: Unknown result type (might be due to invalid IL or missing references)
				//IL_005f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0066: Unknown result type (might be due to invalid IL or missing references)
				//IL_0086: Expected O, but got Unknown
				//IL_0272: Unknown result type (might be due to invalid IL or missing references)
				//IL_0277: Unknown result type (might be due to invalid IL or missing references)
				//IL_0283: Unknown result type (might be due to invalid IL or missing references)
				//IL_028f: Unknown result type (might be due to invalid IL or missing references)
				//IL_029b: Unknown result type (might be due to invalid IL or missing references)
				//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
				//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
				//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
				//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
				//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
				//IL_02ef: Expected O, but got Unknown
				IDictionary<string, ObjectData> data4 = asset.AsDictionary<string, ObjectData>().Data;
				dynamic val3 = (dynamic)new ObjectData
				{
					Name = sprinklerName,
					DisplayName = sprinklerName,
					Description = sprinklerDesc,
					Price = 2000,
					Type = "Crafting",
					Category = -8,
					Edibility = -300,
					SpriteIndex = 0,
					Texture = ModEntry.ModHelper.ModContent.GetInternalAssetName("assets/prismaticSprinkler.png").Name
				};
				val3.ContextTags = new List<string>();
				if (ModEntry.Config.UseSprinklersAsScarecrows)
				{
					val3.ContextTags.Add("crow_scare");
					val3.ContextTags.Add("crow_scare_radius_" + (ModEntry.Config.SprinklerRange + 2));
				}
				data4.Add("PrismaticBar", new ObjectData
				{
					Name = barName,
					DisplayName = barName,
					Description = barDesc,
					Price = 2500,
					Type = "Basic",
					Category = -15,
					Edibility = -300,
					SpriteIndex = 0,
					Texture = ModEntry.ModHelper.ModContent.GetInternalAssetName("assets/prismaticBar.png").Name
				});
				data4.Add("PrismaticSprinkler", val3);
			});
		}
		else if (e.NameWithoutLocale.IsEquivalentTo("Data/Machines"))
		{
			e.Edit(delegate(IAssetData asset)
			{
				//IL_000d: Unknown result type (might be due to invalid IL or missing references)
				//IL_0012: Unknown result type (might be due to invalid IL or missing references)
				//IL_001d: Unknown result type (might be due to invalid IL or missing references)
				//IL_0028: Unknown result type (might be due to invalid IL or missing references)
				//IL_0030: Expected O, but got Unknown
				//IL_0030: Unknown result type (might be due to invalid IL or missing references)
				//IL_0035: Unknown result type (might be due to invalid IL or missing references)
				//IL_0041: Unknown result type (might be due to invalid IL or missing references)
				//IL_004d: Unknown result type (might be due to invalid IL or missing references)
				//IL_005f: Expected O, but got Unknown
				//IL_005f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0064: Unknown result type (might be due to invalid IL or missing references)
				//IL_0070: Unknown result type (might be due to invalid IL or missing references)
				//IL_007c: Unknown result type (might be due to invalid IL or missing references)
				//IL_008d: Unknown result type (might be due to invalid IL or missing references)
				//IL_009f: Expected O, but got Unknown
				//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
				//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
				//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
				//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
				//IL_0100: Expected O, but got Unknown
				//IL_0100: Unknown result type (might be due to invalid IL or missing references)
				//IL_0105: Unknown result type (might be due to invalid IL or missing references)
				//IL_0111: Unknown result type (might be due to invalid IL or missing references)
				//IL_011d: Unknown result type (might be due to invalid IL or missing references)
				//IL_0132: Expected O, but got Unknown
				//IL_0132: Unknown result type (might be due to invalid IL or missing references)
				//IL_0137: Unknown result type (might be due to invalid IL or missing references)
				//IL_0143: Unknown result type (might be due to invalid IL or missing references)
				//IL_014f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0162: Unknown result type (might be due to invalid IL or missing references)
				//IL_0179: Expected O, but got Unknown
				IDictionary<string, MachineData> data3 = asset.AsDictionary<string, MachineData>().Data;
				MachineOutputRule val = new MachineOutputRule
				{
					Id = "Prismatic_Bar",
					MinutesUntilReady = 2400,
					DaysUntilReady = -1
				};
				MachineOutputTriggerRule item = new MachineOutputTriggerRule
				{
					Id = "ItemPlacedInMachine",
					RequiredItemId = "(O)74",
					RequiredCount = ModEntry.Config.PrismaticBarShardCost
				};
				MachineItemOutput item2 = new MachineItemOutput
				{
					Id = "PrismaticBar",
					ItemId = "PrismaticBar",
					MinStack = ModEntry.Config.PrismaticBarNumberProduced,
					MaxStack = ModEntry.Config.PrismaticBarNumberProduced
				};
				val.Triggers = new List<MachineOutputTriggerRule> { item };
				val.OutputItem = new List<MachineItemOutput> { item2 };
				data3["(BC)13"].OutputRules.Add(val);
				MachineOutputRule val2 = new MachineOutputRule
				{
					Id = "Prismatic_Bar",
					MinutesUntilReady = 2400,
					DaysUntilReady = -1
				};
				MachineOutputTriggerRule item3 = new MachineOutputTriggerRule
				{
					Id = "ItemPlacedInMachine",
					RequiredItemId = "(O)74",
					RequiredCount = ModEntry.Config.PrismaticBarShardCost * 5
				};
				MachineItemOutput item4 = new MachineItemOutput
				{
					Id = "PrismaticBar",
					ItemId = "PrismaticBar",
					MinStack = ModEntry.Config.PrismaticBarNumberProduced * 5,
					MaxStack = ModEntry.Config.PrismaticBarNumberProduced * 5 + 5
				};
				val2.Triggers = new List<MachineOutputTriggerRule> { item3 };
				val2.OutputItem = new List<MachineItemOutput> { item4 };
				data3["(BC)HeavyFurnace"].OutputRules.Add(val2);
			});
		}
		else if (e.NameWithoutLocale.IsEquivalentTo("Data/CraftingRecipes"))
		{
			e.Edit(delegate(IAssetData asset)
			{
				IDictionary<string, string> data2 = asset.AsDictionary<string, string>().Data;
				if (asset.Locale != "en")
				{
					data2.Add("Prismatic Sprinkler", $"{"PrismaticBar"} 2 787 2/Home/{"PrismaticSprinkler"}/false/Farming {9}/{sprinklerName}");
				}
				else
				{
					data2.Add("Prismatic Sprinkler", $"{"PrismaticBar"} 2 787 2/Home/{"PrismaticSprinkler"}/false/Farming {9}");
				}
			});
		}
		else if (e.NameWithoutLocale.IsEquivalentTo("Data/Tools"))
		{
			e.Edit(delegate(IAssetData asset)
			{
				IDictionary<string, ToolData> data = asset.AsDictionary<string, ToolData>().Data;
				data.Add("PrismaticPickaxe", createPickAxe());
				data.Add("PrismaticAxe", createAxe());
				data.Add("PrismaticWateringCan", createWateringCan());
				data.Add("PrismaticHoe", createHoe());
			});
		}
	}

	private static ToolData createPickAxe()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		ToolData pickAxe = new ToolData();
		pickAxe.ClassName = "Pickaxe";
		pickAxe.Name = "PrismaticPickaxe";
		pickAxe.AttachmentSlots = -1;
		pickAxe.DisplayName = ModEntry.ModHelper.Translation.Get("prismaticPickaxe");
		pickAxe.Description = "[LocalizedText Strings\\Tools:Pickaxe_Description]";
		pickAxe.Texture = ModEntry.ModHelper.ModContent.GetInternalAssetName("assets/tools.png").Name;
		pickAxe.SpriteIndex = 161;
		pickAxe.MenuSpriteIndex = 187;
		pickAxe.UpgradeLevel = 24;
		//pickAxe.ApplyUpgradeLevelToDisplayName = false;
		pickAxe.ConventionalUpgradeFrom = "(T)IridiumPickaxe";
		ToolUpgradeData pickAxeUpgradeData = new ToolUpgradeData();
		pickAxeUpgradeData.Price = ModEntry.Config.PrismaticToolCost;
		pickAxeUpgradeData.TradeItemId = "PrismaticBar";
		pickAxeUpgradeData.RequireToolId = "(T)IridiumPickaxe";
		pickAxeUpgradeData.TradeItemAmount = 5;
		pickAxe.UpgradeFrom = new List<ToolUpgradeData> { pickAxeUpgradeData };
		return pickAxe;
	}

	private static ToolData createAxe()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		ToolData axe = new ToolData();
		axe.ClassName = "Axe";
		axe.Name = "PrismaticAxe";
		axe.AttachmentSlots = -1;
		axe.DisplayName = ModEntry.ModHelper.Translation.Get("prismaticAxe");
		axe.Description = "[LocalizedText Strings\\Tools:Axe_Description]";
		axe.Texture = ModEntry.ModHelper.ModContent.GetInternalAssetName("assets/tools.png").Name;
		axe.SpriteIndex = 245;
		axe.MenuSpriteIndex = 271;
		axe.UpgradeLevel = 24;
		//axe.ApplyUpgradeLevelToDisplayName = false;
		axe.ConventionalUpgradeFrom = "(T)IridiumAxe";
		ToolUpgradeData axeUpgradeData = new ToolUpgradeData();
		axeUpgradeData.Price = ModEntry.Config.PrismaticToolCost;
		axeUpgradeData.TradeItemId = "PrismaticBar";
		axeUpgradeData.RequireToolId = "(T)IridiumAxe";
		axeUpgradeData.TradeItemAmount = 5;
		axe.UpgradeFrom = new List<ToolUpgradeData> { axeUpgradeData };
		return axe;
	}

	private static ToolData createWateringCan()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		ToolData wateringCan = new ToolData();
		wateringCan.ClassName = "WateringCan";
		wateringCan.Name = "PrismaticWateringCan";
		wateringCan.AttachmentSlots = -1;
		wateringCan.DisplayName = ModEntry.ModHelper.Translation.Get("prismaticWatercan");
		wateringCan.Description = "[LocalizedText Strings\\Tools:WateringCan_Description]";
		wateringCan.Texture = ModEntry.ModHelper.ModContent.GetInternalAssetName("assets/tools.png").Name;
		wateringCan.SpriteIndex = 329;
		wateringCan.MenuSpriteIndex = 352;
		wateringCan.UpgradeLevel = 24;
		//wateringCan.ApplyUpgradeLevelToDisplayName = false;
		wateringCan.ConventionalUpgradeFrom = "(T)IridiumWateringCan";
		ToolUpgradeData wateringCanUpgradeData = new ToolUpgradeData();
		wateringCanUpgradeData.Price = ModEntry.Config.PrismaticToolCost;
		wateringCanUpgradeData.TradeItemId = "PrismaticBar";
		wateringCanUpgradeData.RequireToolId = "(T)IridiumWateringCan";
		wateringCanUpgradeData.TradeItemAmount = 5;
		wateringCan.UpgradeFrom = new List<ToolUpgradeData> { wateringCanUpgradeData };
		return wateringCan;
	}

	private static ToolData createHoe()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		ToolData hoe = new ToolData();
		hoe.ClassName = "Hoe";
		hoe.Name = "PrismaticHoe";
		hoe.AttachmentSlots = -1;
		hoe.DisplayName = ModEntry.ModHelper.Translation.Get("prismaticHoe");
		hoe.Description = "[LocalizedText Strings\\Tools:Hoe_Description]";
		hoe.Texture = ModEntry.ModHelper.ModContent.GetInternalAssetName("assets/tools.png").Name;
		hoe.SpriteIndex = 77;
		hoe.MenuSpriteIndex = 103;
		hoe.UpgradeLevel = 24;
		//hoe.ApplyUpgradeLevelToDisplayName = false;
		hoe.ConventionalUpgradeFrom = "(T)IridiumHoe";
		ToolUpgradeData hoeUpgradeData = new ToolUpgradeData();
		hoeUpgradeData.Price = ModEntry.Config.PrismaticToolCost;
		hoeUpgradeData.TradeItemId = "PrismaticBar";
		hoeUpgradeData.RequireToolId = "(T)IridiumHoe";
		hoeUpgradeData.TradeItemAmount = 5;
		hoe.UpgradeFrom = new List<ToolUpgradeData> { hoeUpgradeData };
		return hoe;
	}

	public Rectangle GetRectangle(int id)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		int x = id % 24 * 16;
		int y = id / 24 * 16;
		return new Rectangle(x, y, 16, 16);
	}
}
