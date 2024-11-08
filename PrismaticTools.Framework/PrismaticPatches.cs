using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StardewValley;

namespace PrismaticTools.Framework;

internal static class PrismaticPatches
{
    public static void After_Object_GetBaseRadiusForSprinkler(ref Object __instance, ref int __result)
    {
        if (__instance.QualifiedItemId == "(O)PrismaticSprinkler")
        {
            __result = ModEntry.Config.SprinklerRange;
        }
    }

    public static bool Object_UpdatingWhenCurrentLocation(ref Object __instance, GameTime time)
    {
        //IL_0020: Unknown result type (might be due to invalid IL or missing references)
        Object obj = __instance;
        if (obj.QualifiedItemId == "(O)PrismaticSprinkler")
        {
            TryEnablePrismaticSprinkler(__instance.Location, obj.TileLocation, obj);
        }
        return true;
    }

    public static bool Object_OnPlacing(ref Object __instance, GameLocation location, int x, int y)
    {
        //IL_001d: Unknown result type (might be due to invalid IL or missing references)
        Object obj = __instance;
        if (obj.QualifiedItemId == "(O)PrismaticSprinkler")
        {
            TryEnablePrismaticSprinkler(location, new Vector2((float)x, (float)y), obj);
        }
        return true;
    }

    public static void After_ShopBuilder_GetToolUpgradeConventionalPrice(ref int __result, ref int level)
    {
        if (level == 24)
        {
            __result = ModEntry.Config.PrismaticToolCost;
        }
    }

    public static void After_ShopBuilder_GetToolUpgradeConventionalTradeItem(ref string __result, ref int level)
    {
        if (level == 24)
        {
            __result = "PrismaticBar";
        }
    }

    public static void Tool_TilesAffected_Postfix(ref List<Vector2> __result, Vector2 tileLocation, int power, Farmer who)
    {
        //IL_00e7: Unknown result type (might be due to invalid IL or missing references)
        //IL_00ec: Unknown result type (might be due to invalid IL or missing references)
        //IL_00ed: Unknown result type (might be due to invalid IL or missing references)
        //IL_00f2: Unknown result type (might be due to invalid IL or missing references)
        //IL_0100: Unknown result type (might be due to invalid IL or missing references)
        //IL_0104: Unknown result type (might be due to invalid IL or missing references)
        //IL_0109: Unknown result type (might be due to invalid IL or missing references)
        //IL_010a: Unknown result type (might be due to invalid IL or missing references)
        //IL_011d: Unknown result type (might be due to invalid IL or missing references)
        //IL_0121: Unknown result type (might be due to invalid IL or missing references)
        //IL_0126: Unknown result type (might be due to invalid IL or missing references)
        //IL_012a: Unknown result type (might be due to invalid IL or missing references)
        //IL_012f: Unknown result type (might be due to invalid IL or missing references)
        //IL_0134: Unknown result type (might be due to invalid IL or missing references)
        //IL_0135: Unknown result type (might be due to invalid IL or missing references)
        //IL_0142: Unknown result type (might be due to invalid IL or missing references)
        //IL_0146: Unknown result type (might be due to invalid IL or missing references)
        //IL_014b: Unknown result type (might be due to invalid IL or missing references)
        //IL_0150: Unknown result type (might be due to invalid IL or missing references)
        //IL_0155: Unknown result type (might be due to invalid IL or missing references)
        //IL_015a: Unknown result type (might be due to invalid IL or missing references)
        //IL_015b: Unknown result type (might be due to invalid IL or missing references)
        if (power < 6)
        {
            return;
        }
        __result.Clear();
        int radius = ModEntry.Config.PrismaticToolWidth;
        int length = ModEntry.Config.PrismaticToolLength;
        Vector2 direction = default(Vector2);
        Vector2 orth = default(Vector2);
        switch (who.FacingDirection)
        {
            case 0:
                direction = new Vector2(0f, -1f);
                orth = new Vector2(1f, 0f);
                break;
            case 1:
                direction = new Vector2(1f, 0f);
                orth = new Vector2(0f, 1f);
                break;
            case 2:
                direction = new Vector2(0f, 1f);
                orth = new Vector2(-1f, 0f);
                break;
            case 3:
                direction = new Vector2(-1f, 0f);
                orth = new Vector2(0f, -1f);
                break;
            default:
                direction = Vector2.Zero;
                orth = Vector2.Zero;
                break;
        }
        for (int i = 0; i < length; i++)
        {
            __result.Add(direction * (float)i + tileLocation);
            for (int j = 1; j <= radius; j++)
            {
                __result.Add(direction * (float)i + orth * (float)j + tileLocation);
                __result.Add(direction * (float)i + orth * (float)(-j) + tileLocation);
            }
        }
    }

    private static void TryEnablePrismaticSprinkler(GameLocation location, Vector2 tile, Object obj)
    {
        //IL_0026: Unknown result type (might be due to invalid IL or missing references)
        //IL_0033: Unknown result type (might be due to invalid IL or missing references)
        //IL_0052: Unknown result type (might be due to invalid IL or missing references)
        //IL_0058: Unknown result type (might be due to invalid IL or missing references)
        //IL_0062: Unknown result type (might be due to invalid IL or missing references)
        if (!(obj.QualifiedItemId != "(O)PrismaticSprinkler") && ModEntry.Config.UseSprinklersAsLamps)
        {
            int id = (int)tile.X * 4000 + (int)tile.Y;
            if (!location.sharedLights.ContainsKey(id.ToString()))
            {
                obj.lightSource = new LightSource(id.ToString(), 4, tile * 64f, 2f, Color.Black);
                location.sharedLights.Add(id.ToString(), obj.lightSource);
            }
        }
    }
}
