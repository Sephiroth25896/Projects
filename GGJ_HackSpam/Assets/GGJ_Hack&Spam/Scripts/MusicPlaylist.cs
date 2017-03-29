using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicPlaylist : MonoBehaviour
{

	public List<AudioClip> AudioClips = new List<AudioClip>();
	public bool LoopOnLast = false;

	private AudioSource m_AudioSource = null;

	public void Start()
	{
		m_AudioSource = GetComponent<AudioSource>();
		StartCoroutine(coroutine_ExecutePlaylist());
		Object.DontDestroyOnLoad(gameObject);
	}

	private IEnumerator coroutine_ExecutePlaylist()
	{
		while (AudioClips.Count > 1)
		{
			m_AudioSource.clip = AudioClips[0];
			AudioClips.RemoveAt(0);
			m_AudioSource.Play();
			yield return new WaitForSeconds(m_AudioSource.clip.length - 0.016f);
		}
		
		if (LoopOnLast)
		{
			while (isActiveAndEnabled)
			{
				m_AudioSource.clip = AudioClips[0];
				m_AudioSource.Play();
				yield return new WaitForSeconds(m_AudioSource.clip.length - 0.016f);
			}
		}
	}
}
