using UnityEngine;
using System.Collections;

public class LevelExit : MonoBehaviour
{
	private const int m_FramesToSkip = 10;
	private int m_CurrentFrame;
	private bool m_Done;

	protected void Start()
	{
		m_CurrentFrame = 0;
		m_Done = false;
	}

	protected void OnTriggerStay2D(Collider2D col)
	{
		if (!m_Done)
		{
			if (m_CurrentFrame == 0)
			{
				IA[] ais = FindObjectsOfType<IA>();
				foreach(IA ai in ais){
					if (ai.gameObject.name.Contains("Spikes") && ai.gameObject.name.Contains("FireBall"))
					{
						return;
					}
				}
				MenuInGame inGameMenu = FindObjectOfType<MenuInGame>();
				if (inGameMenu != null)
				{
					inGameMenu.OnVictory();
					m_Done = true;
					Player.MapsDone++;
				}
			}
			else
			{
				m_CurrentFrame = (m_CurrentFrame + 1) % m_FramesToSkip;
			}
		}
	}
}
