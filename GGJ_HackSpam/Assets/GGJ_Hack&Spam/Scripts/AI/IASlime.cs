using UnityEngine;
using System.Collections;

public class IASlime : IAWalking {


	void Start()
	{
		base.Start ();
		InvokeRepeating ("Move", 0, 0.5f);
	}


}
