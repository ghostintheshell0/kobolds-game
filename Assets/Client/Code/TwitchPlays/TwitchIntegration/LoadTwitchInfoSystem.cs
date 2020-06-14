using Leopotam.Ecs;
using System.IO;
using UnityEngine;

public class LoadTwitchInfoSystem : IEcsInitSystem
{
	private readonly GameData gameData = default;
	private readonly EcsWorld world = default;

	public void Init()
	{
		var path = Path.Combine(Application.dataPath, gameData.LocalDataPath, gameData.SecretFileName);
		if(!File.Exists(path)) return;

		var secrets = File.ReadAllLines(path);
		if(secrets.Length < 3) return;

		var secretEnt = world.NewEntity();
		ref var secret = ref secretEnt.Set<TwitchSecretComponent>();
		secret.Oauth = secrets[0];
		secret.UserName = secrets[1];
 		secret.Channel = secrets[2];

		var hideConnectHudEnt = world.NewEntity();
		ref var hideConnectHud = ref hideConnectHudEnt.Set<ChangeTwitchConnectionUIComponent>();
	}
}