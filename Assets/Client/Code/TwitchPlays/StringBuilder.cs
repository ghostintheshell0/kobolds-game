namespace TwitchPlays.Utils
{
	public static class StringBuilder
	{
		private static System.Text.StringBuilder Instance = new System.Text.StringBuilder();

		public static System.Text.StringBuilder Get()
		{
			Instance.Clear();
			return Instance;
		}
	}
}