using UnityEngine;

public static class MapComponentExtensions
{ 
	public static bool IsOut(this ref MapComponent map, Vector2Int pos)
	{
		return pos.x < 0 || pos.x >= map.MapSize.x || pos.y < 0 || pos.y >= map.MapSize.y;
	}


	public static Vector3 MapToWorld(this ref MapComponent map, Vector2Int mapPos)
	{
		var x = mapPos.x * map.CellSize.x;
		var y = 0;
		var z = mapPos.y * map.CellSize.z;
		var pos = new Vector3(x, y, z) + map.Position;
		return pos;
	}
}
