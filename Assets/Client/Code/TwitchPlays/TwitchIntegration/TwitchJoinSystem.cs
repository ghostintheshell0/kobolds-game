using Leopotam.Ecs;

public class TwitchJoinSystem : IEcsRunSystem
{
	private readonly EcsFilter<TwitchChannelJoinComponent> channels = default;
	private readonly EcsFilter<TwitchConnectionComponent> connections = default;


	public void Run()
	{
		if (connections.IsEmpty()) return;

		foreach (var c in channels)
		{
			ref var cChannel = ref channels.Get1(c);

			foreach (var e in connections)
			{

				ref var connection = ref connections.Get1(e);
				connection.Client.JoinChannel(cChannel.ChannelName);
				connection.Channel = cChannel.ChannelName;
				
			}

			channels.GetEntity(c).Unset<TwitchChannelJoinComponent>();
		}

	}
}