using UnityEngine;

public struct MapObjectComponent
{
	public Vector2Int Position;
	public Transform View;
	public bool Visible;
	public bool CanBeUnderWall;
	public MapObjectType MapObjectType;
}
