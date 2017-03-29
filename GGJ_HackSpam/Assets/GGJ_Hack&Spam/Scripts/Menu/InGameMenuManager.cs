using UnityEngine;
using System.Collections;

public class InGameMenuManager : MenuManager
{
	private GameObject m_MusicPlaylistPrefab;
	private MusicPlaylist m_MusicPlaylist;
	
	protected new void Start()
	{
		base.Start();
		
		m_MusicPlaylistPrefab = Resources.Load("Prefabs/InGameMusicPlayer", typeof(GameObject)) as GameObject;
		m_MusicPlaylist = FindObjectOfType<MusicPlaylist>();
		if (m_MusicPlaylist == null)
		{
			GameObject obj = GameObject.Instantiate(m_MusicPlaylistPrefab);
			m_MusicPlaylist = obj.GetComponent<MusicPlaylist>();
		}
	}
	
	protected void OnDestroy()
	{
		if (m_MusicPlaylist != null)
		{
			DestroyImmediate(m_MusicPlaylist.gameObject);
		}
	}
}
