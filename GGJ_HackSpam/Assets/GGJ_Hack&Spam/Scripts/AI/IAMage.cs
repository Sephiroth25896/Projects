using UnityEngine;
using System.Collections;

public class IAMage : IAWalking {

	public	AudioClip onAttack;

	private Transform spawn;
	// Use this for initialization
	void Start () {
		base.Start();
		spawn = transform.Find ("Spawn");
		InvokeRepeating ("Attack", 2, 2);
	}
	
	void Attack()
	{
		_audio.PlayOneShot (onAttack);
		if (_player.transform.position.x < transform.position.x && !reversed) {
			Flip ();
		} else if (_player.transform.position.x > transform.position.x && reversed) {
			Flip ();
		}
		GameObject g = Resources.Load ("Prefabs/FireBall", typeof(GameObject)) as GameObject;
		GameObject s = Instantiate(g, spawn.position, spawn.rotation) as GameObject;
		s.GetComponent<IAFireBall>().Launch (reversed);
	}
}
