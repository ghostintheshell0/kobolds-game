using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Game/HatsList")]
public class HatsList : RandomItemsList<HatItem>
{
	public HatItem NoHat;
	public IReadOnlyList<HatItem> Hats => items;
}
