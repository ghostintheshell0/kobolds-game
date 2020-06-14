using Leopotam.Ecs;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class SavePlayersSystem : IEcsRunSystem
{
	private readonly RuntimeData runtimeData = default;
	private readonly LevelData levelData = default;
	private readonly GameData gameData = default;

	public void Run()
	{
		if (Input.GetKeyDown(levelData.SaveGameButton))
		{
			Save();
		}
	}

	private void Save()
	{
		var directoryPath = Path.Combine(Application.dataPath, gameData.LocalDataPath);
		if (!Directory.Exists(directoryPath))
		{
			Directory.CreateDirectory(directoryPath);
		}
		var path = Path.Combine(directoryPath, gameData.PlayersSaveFile);
		var data = runtimeData.SavedPlayers;
		var json = JsonConvert.SerializeObject(data);
		File.WriteAllText(path, json);
	}
}

public class LoadPlayersSystem : IEcsInitSystem
{
	private readonly RuntimeData runtimeData = default;
	private readonly GameData gameData = default;

	public void Init()
	{
		var path = Path.Combine(Application.dataPath, gameData.LocalDataPath, gameData.PlayersSaveFile);
		if(!File.Exists(path))
		{
			Debug.Log($"File {path} not exist");
			return;
		}

		var json = File.ReadAllText(path);


		var players = JsonConvert.DeserializeObject<List<PlayerStats>>(json);

		for(var i = 0; i < players.Count; ++i)
		{
			runtimeData.SavePlayer(players[i]);
		}
	}
}