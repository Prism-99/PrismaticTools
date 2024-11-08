using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PrismaticTools.Framework;

public class PrismaticAPI
{
	public int SprinklerRange { get; } = ModEntry.Config.SprinklerRange;


	public string SprinklerQualifiedItemID { get; } = "(O)PrismaticSprinkler";


	public string BarQualifiedItemID { get; } = "(O)PrismaticBar";


	public bool ArePrismaticSprinklersScarecrows { get; } = ModEntry.Config.UseSprinklersAsScarecrows;


	public IEnumerable<Vector2> GetSprinklerCoverage(Vector2 origin)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		for (int x = -SprinklerRange; x <= SprinklerRange; x++)
		{
			for (int y = -SprinklerRange; y <= SprinklerRange; y++)
			{
				yield return new Vector2((float)x, (float)y) + origin;
			}
		}
	}
}
