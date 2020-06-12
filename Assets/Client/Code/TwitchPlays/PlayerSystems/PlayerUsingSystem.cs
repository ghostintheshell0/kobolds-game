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
			ref var objComp = ref objEnt.Set<MapObjectComponent>();
			if (objEnt.IsAlive())
			{
				if(objEnt.Has<ChestComponent>())
				{
					var hat = gameData.Hats.GetRandomItem();
					if(!ContainsHat(ref player, hat))
					{
						player.Stats.Hats.Add(hat.Index);
					}

					ObjectPool.Recycle(objComp.View);
					map.Objects.Remove(objEnt);
					objEnt.Destroy();
				}
			}

			playerEnt.Unset<UseCommand>();
		}
	}

	private bool ContainsHat(ref PlayerComponent player, HatItem hat)
	{
		for(int i = 0; i < player.Stats.Hats.Count; ++i)
		{
			if (player.Stats.Hats[i] == hat.Index) return true;
		}
		return false;
	}
}
