using UnityEngine;
using System.Collections;

public class DamageDealer : MonoBehaviour {
	
	public	int	damage;
	public	bool	hero;
	public	bool	other;
	public  float force = 5f;
	private Player _player;
	private IA _monster;

	// Use this for initialization
	void Start () {
		if (hero)
			_player = transform.parent.GetComponent<Player> ();
		else {
			_monster = transform.GetComponent<IA> ();
			if (_monster == null)
				_monster = transform.parent.GetComponent<IA>();
		}
	}


	void OnTriggerEnter2D(Collider2D coll) {
		if (hero && coll.gameObject.GetComponent<IA>())
		{
			coll.gameObject.SendMessage("OnDamaged", damage);
			if (coll.gameObject.GetComponent<Rigidbody2D>() != null)
			{
				if (_player.transform.position.x < coll.transform.position.x)
					coll.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(force, force);
				else
					coll.gameObject.GetComponent<Rigidbody2D>().velocity =  new Vector2(-force, force);
			}
		}
		else if (other && coll.gameObject.GetComponent<Player>())
		{
			if (!coll.gameObject.GetComponent<Player>().invincible)
			{
				coll.gameObject.SendMessage("OnDamaged", damage);
				if (coll.gameObject.GetComponent<Rigidbody2D>() != null)
				{
					if (_monster.transform.position.x < coll.transform.position.x)
						coll.gameObject.GetComponent<Rigidbody2D>().velocity =  new Vector2(force, force / 2);
					else
						coll.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-force, force / 2);
				}
			}
		}
	}
}
