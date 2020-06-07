using UnityEngine;
using System.Collections.Generic;

public class DebugOutput : MonoBehaviour
{
	[SerializeField] private Transform messagesContainer = default;
	[SerializeField] private MessageView template = default;
	[SerializeField] private int size = default;
	private List<MessageView> messages;

	private void Start()
	{
		messages = new List<MessageView>(size);
	}

	public void Log(string text)
	{
		if(messages.Count >= size)
		{
			RemoveMessageView(messages[0]);
		}

		var view = GetMessageView();
		view.Text = text;
		view.transform.SetParent(messagesContainer);
		messages.Add(view);
	}

	private MessageView GetMessageView()
	{
		return ObjectPool.Spawn(template);
	}

	private void RemoveMessageView(MessageView view)
	{
		if (messages.Remove(view))
		{
			ObjectPool.Recycle(view);
		}
	}
}
