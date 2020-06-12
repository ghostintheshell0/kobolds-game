using Leopotam.Ecs;
using UnityEngine;

public static class MapComponentExtensions
{ 
	public static bool IsOut(this ref MapComponent map, Vector2Int pos)
	{
		return pos.x < 0 || pos.x >= map.Size.x || pos.y < 0 || pos.y >= map.Size.y;
	}


	public static Vector3 MapToWorld(this ref MapComponent map, Vector2Int mapPos)
	{
		var x = mapPos.x * map.CellSize.x;
		var y = 0;
		var z = mapPos.y * map.CellSize.z;
		var pos = new Vector3(x, y, z) + map.Position;
		return pos;
	}

	public static EcsEntity GetObjectInPosition(this ref MapComponent map, Vector2 pos)
	{
		for (var i = 0; i < map.Objects.Count; ++i)
		{
			ref var mapObjComp = ref map.Objects[i].Set<MapObjectComponent>();
			if (mapObjComp.Position == pos) return map.Objects[i];
		}

		return EcsEntity.Null;
	}
	
}
