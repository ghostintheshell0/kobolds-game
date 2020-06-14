using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeData : IEcsSystem
{
	private List<PlayerStats> savedPlayersData = new List<PlayerStats>();
	private List<EcsEntity> livePlayers = new List<EcsEntity>();
	private List<PlayerStats> playersInLastGame = new List<PlayerStats>();
	private List<PlayerStats> escapedPlayers = new List<PlayerStats>();
	private readonly EcsFilter<MapComponent> maps = default;

	public bool IsDoom = false;

	public void AddPlayer(EcsEntity playerEntity)
	{
		var stats = playerEntity.Set<PlayerComponent>().Stats;
		livePlayers.Add(playerEntity);
		playersInLastGame.Add(stats);
	}

	public void ClearPlayersInLastGame()
	{
		playersInLastGame.Clear();
	}

	public void ClearLivePlayers()
	{
		livePlayers.Clear();
	}

	public EcsEntity GetPlayer(string playerName)
	{
		for (int i = 0; i < livePlayers.Count; ++i)
		{
			if (!livePlayers[i].Has<PlayerComponent>()) continue;
			ref var player = ref livePlayers[i].Set<PlayerComponent>();
			if (player.Stats.Name == playerName) return livePlayers[i];
		}

		return default;
	}

	public PlayerStats GetSavedPlayer(string playerName)
	{
		for (int i = 0; i < savedPlayersData.Count; ++i)
		{
			var player = savedPlayersData[i];
			if (player.Name == playerName) return savedPlayersData[i];
		}

		return default;
	}

	public bool ContainsPlayer(string playerName)
	{
		for(int i = 0; i < livePlayers.Count; ++i)
		{
			if (!livePlayers[i].Has<PlayerComponent>()) continue;
			ref var player = ref livePlayers[i].Set<PlayerComponent>();
			if (player.Stats.Name == playerName) return true;
		}

		return false;
	}

	public void SavePlayer(PlayerStats data)
	{
		savedPlayersData.Add(data);
	}

	public bool IsSavedPlayer(string playerName)
	{
		for (int i = 0; i < savedPlayersData.Count; ++i)
		{
			if (savedPlayersData[i].Name == playerName) return true;
		}

		return false;
	}

	public void RemovePlayer(string playerName)
	{
		for (int i = 0; i < livePlayers.Count; ++i)
		{
			if (!livePlayers[i].Has<PlayerComponent>()) continue;
			ref var player = ref livePlayers[i].Set<PlayerComponent>();
			if (player.Stats.Name == playerName)
			{
				livePlayers.RemoveAt(i);
				return;
			}
		}
	}

	public bool IsEscapedPlayer(string playerName)
	{
		for (int i = 0; i < escapedPlayers.Count; i++)
		{
			if (escapedPlayers[i].Name == playerName) return true;
		}
		return false;
	}

	public void ClearEscapedPlayers()
	{
		escapedPlayers.Clear();
	}

	public void EscapePlayer(PlayerStats data)
	{
		escapedPlayers.Add(data);
	}

	public ref MapComponent GetMap(int index)
	{
		return ref maps.Get1(index);
	}

	public int PlayersCount => livePlayers.Count;
	public IReadOnlyList<PlayerStats> EscapedPlayers => escapedPlayers;
	public IReadOnlyList<PlayerStats> PlayersInLastGame => playersInLastGame;
	public List<PlayerStats> SavedPlayers => savedPlayersData;
}