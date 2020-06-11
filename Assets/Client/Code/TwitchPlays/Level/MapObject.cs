using UnityEngine;

[System.Serializable]
public struct MapObject
{
	public Transform Template;
	[Range(0, 1f)] public float Chance;
	public int BordersOffset;
	public int Count;
	public bool Visible;
	public bool CanBeUnderWall;
	public MapObjectType MapObjectType;
}
