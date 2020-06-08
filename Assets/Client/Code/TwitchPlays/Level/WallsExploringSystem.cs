using Leopotam.Ecs;
using UnityEngine;

public class WallsExploringSystem : IEcsRunSystem
{
	private readonly EcsFilter<WallDestroyingComponent> filter = default;
	private readonly EcsFilter<MapComponent> maps = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var dest = ref filter.Get1(i);

			foreach(var m in maps)
			{
				ref var map = ref maps.Get1(i);
				ShowWalls(ref map, dest.Position);
			}
		}
	}

	private void ShowWalls(ref MapComponent map, Vector2Int pos)
	{
		var left = new Vector2Int(pos.x - 1, pos.y);
		var right = new Vector2Int(pos.x + 1, pos.y);
		var up = new Vector2Int(pos.x, pos.y + 1);
		var down = new Vector2Int(pos.x, pos.y - 1);

		Enable(ref map, left, pos);
		Enable(ref map, right, pos);
		Enable(ref map, up, pos);
		Enable(ref map, down, pos);

	}

	private void Enable(ref MapComponent map, Vector2Int wallPos, Vector2Int holePos)
	{
		if (map.IsOut(wallPos)) return;
		
		var wallEnt = map.Walls[wallPos.x, wallPos.y];
		if(wallEnt.IsAlive())
		{
			ref var wallComp = ref wallEnt.Set<WallComponent>();
			wallComp.EnablePlane(holePos - wallPos);
		}
	}
}
