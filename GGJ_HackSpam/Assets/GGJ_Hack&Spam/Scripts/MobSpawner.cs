using UnityEngine;
using System.Collections;

public class MobSpawner : MonoBehaviour {

	public	AudioClip	onCreate;
	private	AudioSource	_audio;

	void Start(){
		_audio = GetComponent<AudioSource> ();
	}

	void InstantiateIA(MasterSpawner.IAPacket toSpawn) {
		_audio.PlayOneShot (onCreate);
		GameObject obj = Instantiate (toSpawn.IA, this.transform.position, this.transform.rotation) as GameObject;
		//obj.GetComponent<IA>().SetName(toSpawn.Name);
	}
}
