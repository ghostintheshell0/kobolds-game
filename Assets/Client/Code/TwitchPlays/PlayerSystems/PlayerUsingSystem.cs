using Leopotam.Ecs;
using UnityEngine;

public class PlayerUsingSystem : IEcsRunSystem
{
	private readonly EcsFilter<PlayerComponent, UseCommand> filter = default;
	private readonly GameData gameData = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var player = ref filter.Get1(i);
			ref var playerEnt = ref filter.GetEntity(i);

			ref var map = ref player.MapEntity.Set<MapComponent>();
			var objEnt = map.GetObjectInPosition(player.Position);

			if (objEnt.IsAlive())
			{
				if(objEnt.Has<ChestComponent>())
				{
					var hatId = gameData.Hats.GetRandomItem();

					Debug.Log($"Your hat ({hatId}) in another castle");
					map.Objects.Remove(objEnt);
					objEnt.Destroy();
				}
			}

			playerEnt.Unset<UseCommand>();
		}
	}
}