using Leopotam.Ecs;
using System.IO;
using UnityEngine;

public class LoadTwitchInfoSystem : IEcsInitSystem
{
	private readonly GameData gameData = default;
	private readonly EcsWorld world = default;

	public void Init()
	{
		var path = Path.Combine(Application.dataPath, gameData.SecretFileName);
		if(!File.Exists(path))
		{
			return;
		}
		var secrets = File.ReadAllLines(path);

		var secretEnt = world.NewEntity();
		ref var secret = ref secretEnt.Set<TwitchSecretComponent>();
		secret.Oauth = secrets[0];
 		secret.Channel = secrets[1];

		var hideConnectHudEnt = world.NewEntity();
		ref var hideConnectHud = ref hideConnectHudEnt.Set<HideTwitchConnectionUIComponent>();
	}
}