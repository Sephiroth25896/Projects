using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class MenuGeneration : Menu
{
	public MenuSelectMaps MenuSelectMaps = null;

	public void Start()
	{
		StartCoroutine(coroutine_DelayLevelLoaded());
	}

	public void OnDestroy()
	{
	}

	private IEnumerator coroutine_DelayLevelLoaded()
	{
		yield return new WaitForSeconds(0.5f);
		
		OnLevelLoaded();
	}

	public void DelayLoadSelectedLevel()
	{
		StartCoroutine(coroutine_DelayLoadSelectedLevel());
	}

	private IEnumerator coroutine_DelayLoadSelectedLevel()
	{
		yield return new WaitForSeconds(1.0f);

		Application.LoadLevel(MenuSelectMaps.SelectedMapName);
	}

	public UnityEvent LevelLoadedEvent;
	public void OnLevelLoaded()
	{
		if (LevelLoadedEvent != null) LevelLoadedEvent.Invoke();
	}
}
