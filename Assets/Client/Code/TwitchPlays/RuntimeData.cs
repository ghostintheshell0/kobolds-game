using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeData
{
	private List<PlayerStats> savedPlayersData = new List<PlayerStats>();
	private List<EcsEntity> players = new List<EcsEntity>();

	public void AddPlayer(EcsEntity playerEntity)
	{
		players.Add(playerEntity);
	}

	public EcsEntity GetPlayer(string playerName)
	{
		for (int i = 0; i < players.Count; ++i)
		{
			if (!players[i].Has<PlayerComponent>()) continue;
			ref var player = ref players[i].Set<PlayerComponent>();
			if (player.Stats.Name == playerName) return players[i];
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

		for(int i = 0; i < players.Count; ++i)
		{
			if (!players[i].Has<PlayerComponent>()) continue;
			ref var player = ref players[i].Set<PlayerComponent>();
			if (player.Stats.Name == playerName) return true;
		}

		return false;
	}

	public void SavePlayer(EcsEntity playerEnt)
	{
		ref var playerComp = ref playerEnt.Set<PlayerComponent>();
		ref var skinComp = ref playerEnt.Set<SkinComponent>();

		savedPlayersData.Add(playerComp.Stats);
	}

	public void ClearSavedPlayers()
	{
		savedPlayersData.Clear();
	}

	public void ClearPlayers()
	{
		players.Clear();
	}

	public void RemovePlayer(string playerName)
	{
		for (int i = 0; i < players.Count; ++i)
		{
			if (!players[i].Has<PlayerComponent>()) continue;
			ref var player = ref players[i].Set<PlayerComponent>();
			if (player.Stats.Name == playerName)
			{
				players.RemoveAt(i);
				return;
			}
		}
	}

	public bool IsSavedPlayer(string playerName)
	{
		for (int i = 0; i < savedPlayersData.Count; i++)
		{
			if (savedPlayersData[i].Name == playerName) return true;
		}
		return false;
	}

	public int PlayersCount => players.Count;
	public IReadOnlyList<PlayerStats> SavedPlayers => savedPlayersData;
}
