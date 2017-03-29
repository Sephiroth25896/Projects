using UnityEngine;
using System.Collections;

public class IABat : IAFlying {

	// Use this for initialization
	void Start () {
		base.Start();
		InvokeRepeating ("Move", 0, 0.5f);
	}
}
