using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game/Random wall list")]
public class RandomWallList : ScriptableObject
{
	[SerializeField] protected List<RandomWallItem> items = default;
	private int maxChance = 0;

	private void OnEnable()
	{
		CalculateChances();
	}

	public void CalculateChances()
	{
		maxChance = 0;
		for (int i = 0; i < items.Count; ++i)
		{
			maxChance += items[i].Chance;
		}
	}


	public RandomWallItem GetRandomItem()
	{
		var r = Random.Range(1, maxChance + 1);
		var c = 0;
		for (int i = 0; i < items.Count; ++i)
		{
			c += items[i].Chance;
			if (c >= r) return items[i];
		}

		throw new System.ApplicationException("Your algorythm is suck! (" + r + "/" + maxChance + ")");
	}

	public IReadOnlyList<RandomWallItem> Items => items;
}
