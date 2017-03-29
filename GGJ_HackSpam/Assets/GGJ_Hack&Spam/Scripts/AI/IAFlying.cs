using UnityEngine;
using System.Collections;

public abstract class IAFlying : IA {

	public float Xmax = 10;
	public float Ymax = 5;
	protected Vector2 currentPos;
	protected Vector2 startPos;
	public int speed;
	public bool height = true;

	public void Start ()
	{
		startPos = new Vector2 (transform.position.x, transform.position.y);
		currentPos = new Vector2(0,0);
	}

	public void FixedUpdate()
	{
		currentPos = new Vector2(transform.position.x, transform.position.y) - startPos;
	}
	
	public void Move()
	{
		if (currentPos.x > Xmax && !reversed) 
			Flip ();
		else if (currentPos.x < 0 && reversed)
			Flip ();
		if (currentPos.y > Ymax && height) 
			height = false;
		else if (currentPos.y < 0 && !height)
			height = true;
		if (!reversed) 
		{
			if (height)
				_body.velocity = new Vector2 (speed, speed);
			else
				_body.velocity = new Vector2 (speed, -speed);
		} 
		else 
		{
			if (height)
				_body.velocity = new Vector2 (-speed, speed);
			else
				_body.velocity = new Vector2 (-speed, -speed);
		}
	}
}
