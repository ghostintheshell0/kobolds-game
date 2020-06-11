using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game/Map Objects")]
public class MapObjects : ScriptableObject
{
	[SerializeField] private MapObject[] objects;

	public IReadOnlyList<MapObject> List => objects;
}
