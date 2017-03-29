using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public abstract class MenuSelect : Menu
{
	public int SelectionTimer = 6000;
	public Text TimerText;
	
	protected int m_CurrentSelectionTime = 0;
	protected string m_TimerTextFormat;
	protected bool m_TimerTriggered;
	
	protected void Start()
	{
		m_TimerTextFormat = TimerText.text;
		m_TimerTriggered = true;
		
		MenuOpenedEvent.AddListener(StartTimer);
		SelectionTimerOverEvent.AddListener(HandleSelectionTimerOver);
	}
	
	protected void OnDestroy()
	{
		MenuOpenedEvent.RemoveListener(StartTimer);
		SelectionTimerOverEvent.RemoveListener(HandleSelectionTimerOver);
	}
	
	protected virtual void Update()
	{
		if (m_CurrentSelectionTime > 0)
		{
			m_CurrentSelectionTime -= (int)(Time.deltaTime * 1000.0f);
			int seconds = Mathf.Max(m_CurrentSelectionTime / 1000, 0);
			int decimals = Mathf.Max(m_CurrentSelectionTime - (seconds * 1000), 0);
			TimerText.text = string.Format(m_TimerTextFormat, seconds, decimals);
		}
		else if (!m_TimerTriggered && m_CurrentSelectionTime <= 0)
		{
			m_TimerTriggered = true;
			TwitchIRC twitchIrc = FindObjectOfType<TwitchIRC>();
			if (twitchIrc != null)
			{
				twitchIrc.MessageRecievedEvent -= HandleMessageRecievedEvent;
			}
			OnSelectionTimerOver();
		}
	}
	
	public virtual void StartTimer()
	{
		m_CurrentSelectionTime = SelectionTimer;
		m_TimerTriggered = false;
		TwitchIRC twitchIrc = FindObjectOfType<TwitchIRC>();
		if (twitchIrc != null)
		{
			twitchIrc.MessageRecievedEvent += HandleMessageRecievedEvent;
		}
	}
	
	protected virtual void HandleMessageRecievedEvent(MessageIRC message)
	{
	}

	protected virtual void HandleSelectionTimerOver()
	{
	}
	
	public UnityEvent SelectionTimerOverEvent;
	public void OnSelectionTimerOver()
	{
		if (SelectionTimerOverEvent != null) SelectionTimerOverEvent.Invoke();
	}
}
