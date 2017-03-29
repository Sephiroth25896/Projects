using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class MenuHackIt : Menu
{
	private CanvasGroup m_CanvasGroup;

	protected void Start()
	{
		m_CanvasGroup = GetComponent<CanvasGroup>();
	}

	protected void Update()
	{
		if (m_CanvasGroup.blocksRaycasts)
		{
			if (Input.anyKeyDown)
			{
				OnAnyKeyDown();
			}
		}
	}

	public UnityEvent AnyKeyDownEvent;
	internal void OnAnyKeyDown()
	{
		if (AnyKeyDownEvent != null) AnyKeyDownEvent.Invoke();
	}
}
