using Leopotam.Ecs;

public class ChangeHatSystem : IEcsRunSystem
{
	private readonly EcsFilter<PlayerComponent, ChangeHatComponent> filter = default;
	private readonly GameData gameData = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var player = ref filter.Get1(i);
			ref var playerEnt = ref filter.GetEntity(i);
			ref var changeHat = ref filter.Get2(i);

			if (changeHat.HatIndex >= 0 && changeHat.HatIndex < player.Stats.Hats.Count)
			{
				var hatItem = GetHatItem(ref player, gameData.Hats);
				var hat = ObjectPool.Spawn(hatItem.View);
				hat.transform.SetParent(player.View.Skin.HeadTop);
				hat.transform.localPosition = hatItem.Offset;
				if (player.View.Skin.Hat != null)
				{
					ObjectPool.Recycle(player.View.Skin.Hat);
				}

				player.View.Skin.Hat = hat;
				player.Stats.CurrentHatIndex = changeHat.HatIndex;
			}

			playerEnt.Unset<ChangeHatComponent>();
		}
	}

	private HatItem GetHatItem(ref PlayerComponent player, HatsList hats)
	{
		if (player.Stats.CurrentHatIndex >= player.Stats.Hats.Count)
		{
			return hats.NoHat;
		}
		var item = hats.Hats[player.Stats.Hats[player.Stats.CurrentHatIndex]];
		return item;
	}
}
