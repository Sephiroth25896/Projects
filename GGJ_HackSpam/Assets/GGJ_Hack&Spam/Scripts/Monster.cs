using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	public	int hitpoint;

	public	void	OnDamaged(int damage){
		hitpoint -= damage;
		if (hitpoint <= 0)
			Destroy (this.gameObject);
	}
}
