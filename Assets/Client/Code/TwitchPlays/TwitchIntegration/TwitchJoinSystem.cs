using Leopotam.Ecs;

public class TwitchJoinSystem : IEcsRunSystem
{
	private readonly EcsFilter<TwitchChannelJoinComponent> channels = default;
	private readonly EcsFilter<TwitchConnectionComponent> connections = default;


	public void Run()
	{
		foreach (var c in channels)
		{
			foreach (var e in connections)
			{

				ref var connection = ref connections.Get1(e);
				ref var cChannel = ref channels.Get1(c);
				connection.Client.JoinChannel(cChannel.ChannelName);
				connection.Channel = cChannel.ChannelName;
				
				channels.GetEntity(c).Unset<TwitchChannelJoinComponent>();
			}

		}

	}
}
