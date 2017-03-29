using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class MenuInGame : Menu
{
	public Player Player = null;
	public Text TextHP = null;
	public Text TextMaps = null;
	public Text TextScore = null;
	
	private string m_HPFormat;
	private string m_MapsFormat;
	private string m_ScoreFormat;

	public bool IsVictorious { get; private set; }
	public UnityEvent VictoryEvent;
	internal void OnVictory()
	{
		if (VictoryEvent != null) VictoryEvent.Invoke();
		IsVictorious = true;
	}
	
	public UnityEvent GameFinishedEvent;
	internal void OnGameFinished()
	{
		if (GameFinishedEvent != null) GameFinishedEvent.Invoke();
	}

	protected void Start()
	{
		if (Player == null)
		{
			Player = FindObjectOfType<Player>();
		}
		if (TextHP != null)
		{
			m_HPFormat = TextHP.text;
		}
		if (TextMaps != null)
		{
			m_MapsFormat = TextMaps.text;
		}
		if (TextScore != null)
		{
			m_ScoreFormat = TextScore.text;
		}
	}

	protected void Update()
	{
		if (TextHP != null)
		{
			TextHP.text = string.Format(m_HPFormat, Player.life);
		}
		if (TextMaps != null)
		{
			TextMaps.text = string.Format(m_MapsFormat, Player.MapsDone);
		}
		if (TextScore != null)
		{
			TextScore.text = string.Format(m_ScoreFormat, Player.MobKilled);
		}
	}
}
