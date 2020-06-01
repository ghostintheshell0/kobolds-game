using Leopotam.Ecs;

public class TwitchMessagesReceivingSystem : IEcsRunSystem
{
	private static char[] SplitChars = new char[] { ' ' };

	private readonly EcsFilter<TwitchMessageComponent> filter = default;
	private readonly EcsWorld world = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var twitchMsg = ref filter.Get1(i);
			var ent = world.NewEntity();
			ref var mess = ref ent.Set<MessageComponent>();
			mess.Sender = twitchMsg.Sender;
			mess.Text = twitchMsg.Text;
			var words = mess.Text.Split(SplitChars, 2);
			var cmdWord = words[0];
			var args = words.Length == 1 ? string.Empty : words[1];
			mess.FirstWord = cmdWord;
			mess.Args = args;
			filter.GetEntity(i).Destroy();
		}
	}


}
