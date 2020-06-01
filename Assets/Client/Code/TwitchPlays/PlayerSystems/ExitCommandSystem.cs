using Leopotam.Ecs;
using System;
using System.Collections.Generic;

//public class MessageReceivingSystem : IEcsRunSystem, IEcsInitSystem
//{

//	private readonly EcsFilter<MessageComponent> filter = default;
//	private readonly EcsWorld word = default;
//	private readonly TwitchCommands commandNames = default;
//	private readonly GameData gameData = default;
////	private readonly GameData data = default;
//	private readonly RuntimeData runtimeData = default;

//	private Dictionary<string, Action<string, string>> commands = new Dictionary<string, Action<string, string>>();

//	public void Init()
//	{
////		commands.Add(commandNames.Enter.ToUpper(), Enter);
//		commands.Add(commandNames.Left.ToUpper(), Left);
//		commands.Add(commandNames.Right.ToUpper(), Right);
//		commands.Add(commandNames.Up.ToUpper(), Up);
//		commands.Add(commandNames.Down.ToUpper(), Down);
////		commands.Add(commandNames.HeadSize.ToUpper(), HeadSize);
////		commands.Add(commandNames.Color.ToUpper(), SetColor);
////		commands.Add(commandNames.Exit.ToUpper(), Exit);
////		commands.Add(commandNames.Help.ToUpper(), Help);
////		commands.Add(commandNames.Fin.ToUpper(), Fin);
//	}

//	public void Run()
//	{
//		foreach(var i in filter)
//		{
//	/*		var mess = filter.Get1(i);

//			var words = mess.Text.Split(SplitChars, 2);
//			var cmdWord = words[0].ToUpper();
//			var args = words.Length == 1 ? string.Empty : words[1];

//			foreach (var cmd in commands)
//			{
//				if (cmd.Key.Equals(cmdWord))
//				{
//					commands[cmdWord](mess.Sender, args);
//				}
//			}*/
//		}
//	}
//	/*
//	private void Enter(string playerName, string args)
//	{
//		if (!runtimeData.IsStarted) return;
//		var e = word.NewEntity();
//		ref var spawn = ref e.Set<PlayerSpawnComponent>();
//		spawn.Name = playerName;
//		spawn.Level = gameData.PlayersData.StartLevel;
//		spawn.Ore = gameData.PlayersData.StartOre;
//		spawn.HeadSize = gameData.VisualData.DefaultHeadSize;
//		spawn.SkinColor = gameData.VisualData.DefaultCharacterColor;
//		Debug.Log("Name: " + spawn.Name);
//	}*/
//	/*
//	private void HeadSize(string playerName, string argsString)
//	{
//		if (!float.TryParse(argsString, out var size))
//		{
//			SendError($"@{playerName} syntax error. Example: {commandNames.HeadSize} 1,5");
//			return;
//		}

//		var playerEntity = runtimeData.GetPlayer(playerName);
//		if (playerEntity.IsNull())
//		{
//			SendNotSpawnedMessage(playerName);
//			return;
//		}

//		ref var headSize = ref playerEntity.Set<HeadSizeChangerComponent>();
//		headSize.Size = size;

//	}*/
//	/*
//	private void SetColor(string playerName, string argsString)
//	{
//		var firstWord = argsString.Split(SplitChars)[0];
//		if (string.IsNullOrEmpty(firstWord) || !ColorUtility.TryParseHtmlString(firstWord, out var color))
//		{
//			SendError($"@{playerName} syntax error. Example: {commandNames.Color} #FF0000");
//			return;
//		}

//		var playerEntity = runtimeData.GetPlayer(playerName);
//		if (playerEntity.IsNull())
//		{
//			SendNotSpawnedMessage(playerName);
//			return;
//		}

//		ref var skinColor = ref playerEntity.Set<SkinColorChangerComponent>();
//		skinColor.Color = color;

//	}
//	*/
//	private void Left(string playerName, string argsString)
//	{
//		CreateMovingCommand(playerName, Vector2Int.left);
//	}

//	private void Right(string playerName, string argsString)
//	{
//		CreateMovingCommand(playerName, Vector2Int.right);
//	}

//	private void Up(string playerName, string argsString)
//	{
//		CreateMovingCommand(playerName, Vector2Int.up);
//	}

//	private void Down(string playerName, string argsString)
//	{
//		CreateMovingCommand(playerName, Vector2Int.down);
//	}
//	/*
//	private void Exit(string playerName, string argsString)
//	{
//		var playerEntity = runtimeData.GetPlayer(playerName);
//		if (playerEntity.IsNull())
//		{
//			SendNotSpawnedMessage(playerName);
//			return;
//		}
//		ref var cmd = ref playerEntity.Set<PlayerExitCommandComponent>();
//	}
//	*/
//	private void CreateMovingCommand(string playerName, Vector2Int direction)
//	{
//		var playerEntity = runtimeData.GetPlayer(playerName);
//		if (playerEntity.IsNull())
//		{
//			SendNotSpawnedMessage(playerName);
//			return;
//		}

//		ref var player = ref playerEntity.Set<PlayerComponent>();
//		var targetPos = player.Position + direction;
//		if (runtimeData.IsOutOfMap(targetPos)) return;

//		var targetCell = runtimeData.Map.Walls[targetPos.x, targetPos.y];

//		if(targetCell.IsAlive())
//		{
//			ref var miningCmd = ref playerEntity.Set<MiningCommandComponent>();
//			miningCmd.Target = targetCell;
//			//mining
//			return;
//		}

//		//moving


//		ref var movingCmd = ref playerEntity.Set<MovingCommandComponent>();
//		movingCmd.TargetPosition = targetPos;
//	}

//	private void SendNotSpawnedMessage(string playerName)
//	{
//		SendError($"@{playerName} , your character not spawned. Type {commandNames.Enter}");
//	}

//	private void SendError(string message)
//	{
//		var ent = word.NewEntity();
//		ref var err = ref ent.Set<ErrorComponent>();
//		err.Message = message;
//	}
//	/*
//	private void Help(string playerName, string argsString)
//	{
//		var ent = word.NewEntity();
//		ref var err = ref ent.Set<ErrorComponent>();
//		err.Message = $"@{playerName} type !e for spawn. Type !u, !r, !d or !l for moving or mining. " +
//			$"When any player found exit come to ladder and type !exit for leave from the map. When time left all players will die. " +
//   $" Type !color and !size for customize your kobold.";
//	}
//	*/
//	/*private void Fin(string playerName, string argsString)
//	{
//		if(runtimeData.ContainsPlayer(playerName))
//		{
//			var playerEnt = runtimeData.GetPlayer(playerName);
//			ref var player = ref playerEnt.Set<PlayerComponent>();
//			var ent = word.NewEntity();
//			ref var err = ref ent.Set<ErrorComponent>();
//			err.Message = $"@{playerName} has {player.Ore} ore.";
//			return;
//		}

//		SendNotSpawnedMessage(playerName);
//	}*/
//}

public class ExitCommandSystem : IEcsRunSystem
{
	private readonly EcsFilter<MessageComponent> filter = default;
	private readonly RuntimeData runtimeData = default;
	private readonly TwitchCommands commands = default;
	private readonly EcsWorld world = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var mess = ref filter.Get1(i);

			if (commands.IsCommand(mess.FirstWord, commands.Exit) == false) continue;

			if (runtimeData.ContainsPlayer(mess.Sender))
			{
				var playerEnt = runtimeData.GetPlayer(mess.Sender);
				playerEnt.Set<PlayerExitCommandComponent>();
			}

			if (runtimeData.IsSavedPlayer(mess.Sender))
			{
				var e = world.NewEntity();
				ref var err = ref e.Set<ErrorComponent>();
				err.Message = $"@{mess.Sender} already leave this level";
			}

			filter.GetEntity(i).Destroy();
		}
	}
}
