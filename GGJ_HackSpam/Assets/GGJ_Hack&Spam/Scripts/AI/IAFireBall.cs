using UnityEngine;
using System.Collections;

public class IAFireBall : IA {

	public void Launch(bool reversed)
	{
		Debug.Log (_body);
		if (!reversed)
			_body.AddRelativeForce (new Vector2 (5, 0), ForceMode2D.Impulse);
		else
			_body.AddRelativeForce (new Vector2 (-5, 0), ForceMode2D.Impulse);
	}

	void FixedUpdate()
	{
		if( Mathf.Abs(_body.velocity.x) < 0.3f && Mathf.Abs(_body.velocity.y) < 0.3f)
		   Destroy(this.gameObject);
	}

	public void GoForIt()
	{
		_body.AddRelativeForce (new Vector2 (0, -5), ForceMode2D.Impulse);
	}

	public void BossShoot()
	{
		_body.AddRelativeForce (new Vector2 (5, 0), ForceMode2D.Impulse);
	}
}
