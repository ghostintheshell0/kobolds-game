using Leopotam.Ecs;
using TwitchLib.Unity;
using TwitchLib.Client.Models;
using TwitchLib.Client.Events;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;

public class TwitchConnectSystem : IEcsRunSystem
{

	private readonly EcsFilter<TwitchSecretComponent> filter = default;
	private readonly GameData gameData = default;
	private readonly EcsWorld world = default;

	private Client client;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var secret = ref filter.Get1(i);

			var cred = new ConnectionCredentials(twitchUsername: secret.UserName, twitchOAuth: secret.Oauth);
			if (client == null)
			{
				client = new Client();
				client.Initialize(cred);

				client.OnConnected += OnConnected;
				client.OnMessageReceived += OnMessageReceived;
				client.OnJoinedChannel += OnJoinedChannel;
				client.Connect();
			}

			var joinChannelEnt = world.NewEntity();
			ref var joinChannel = ref joinChannelEnt.Set<TwitchChannelJoinComponent>();
			joinChannel.ChannelName = secret.Channel;
			SaveSecret(secret);
			filter.GetEntity(i).Unset<TwitchSecretComponent>();
		}
		
	}

	private void OnJoinedChannel(object sender, OnJoinedChannelArgs e)
	{
		var ent = world.NewEntity();
		ref var hideUI = ref ent.Set<ChangeTwitchConnectionUIComponent>();
		hideUI.Visible = false;
	}

	private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
	{
		var ent = world.NewEntity();

		ref var mess = ref ent.Set<TwitchMessageComponent>();
		mess.Sender = e.ChatMessage.DisplayName;
		mess.Text = e.ChatMessage.Message;

		
	}

	private void OnConnected(object sender, OnConnectedArgs args)
	{
		var connectionEnt = world.NewEntity();
		ref var connection = ref connectionEnt.Set<TwitchConnectionComponent>();
		connection.Client = client;

		var startEnt = world.NewEntity();
		ref var start = ref startEnt.Set<StartGameComponent>();


	}

	private void SaveSecret(TwitchSecretComponent secret)
	{
		var dataFolderPath = Path.Combine(Application.dataPath, gameData.LocalDataPath);
		var saveFilePath = Path.Combine(dataFolderPath, gameData.SecretFileName);

		if(!Directory.Exists(dataFolderPath))
		{
			Directory.CreateDirectory(dataFolderPath);
		}

		using (var fs = File.CreateText(saveFilePath))
		{
			fs.WriteLineAsync(secret.Oauth);
			fs.WriteLineAsync(secret.UserName);
			fs.WriteLineAsync(secret.Channel);
		}

	}
}
