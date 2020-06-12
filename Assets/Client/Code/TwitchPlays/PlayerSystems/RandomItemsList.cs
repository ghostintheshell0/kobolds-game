using UnityEngine;

public class RandomItemsList<T> : ScriptableObject where T : IRandomItem
{
	[SerializeField] protected T[] items = default;

	public T GetRandomItem()
	{
		var maxChance = GetMaxChance();
		var r = Random.Range(1, maxChance + 1);
		var c = 0;
		for (int i = 0; i < items.Length; ++i)
		{
			c += items[i].Chance;
			if (c >= r) return items[i];
		}

		throw new System.ApplicationException("items is empty");

	}

	private int GetMaxChance()
	{
		var chance = 0;
		for (int i = 0; i < items.Length; ++i)
		{
			chance += items[i].Chance;
		}

		return chance;
	}
}

public interface IRandomItem
{
	int Chance { get; }
}

public class RandomItem : ScriptableObject, IRandomItem
{
	[SerializeField] private int chance = default;

	public int Chance => chance;
}