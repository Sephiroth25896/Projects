using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IABoss : IA {
	
	private Transform _spawner;
	public float rotationSpeed = 20;
	public float shootDelay = 0.75f;
	public bool moreFireBalls = false;
	public bool doubleSpeed = false;
	public bool doubleHP = false;
	public bool spawners = false;
	public Sprite hitSprite = null;
	private List<Transform> _list;
	private SpriteRenderer _renderer;
	private Sprite _regularSprite;

	void Start()
	{
		_spawner = transform.Find("spawners");
		_list = new List<Transform> ();
		_renderer = GetComponent<SpriteRenderer>();
		_regularSprite = _renderer.sprite;
		Init ();
	}

	void FixedUpdate()
	{
		if (!doubleSpeed)
			_spawner.Rotate (new Vector3 (0, 0, rotationSpeed * Time.deltaTime));
		else
			_spawner.Rotate (new Vector3 (0, 0, rotationSpeed * 2 * Time.deltaTime));

		if (invincible)
		{
			if (_renderer.sprite != hitSprite)
			{
				_renderer.sprite = hitSprite;
			}
		}
		else
		{
			if (_renderer.sprite != _regularSprite)
			{
				_renderer.sprite = _regularSprite;
			}
		}
	}

	void Init()
	{
		_list.Add(_spawner.Find ("spawner_1"));
		_list.Add(_spawner.Find ("spawner_2"));
		_list.Add(_spawner.Find ("spawner_3"));
		_list.Add(_spawner.Find ("spawner_4"));
		if (moreFireBalls) 
		{
			_list.Add(_spawner.Find ("spawner_5"));
			_list.Add(_spawner.Find ("spawner_6"));
			_list.Add(_spawner.Find ("spawner_7"));
			_list.Add(_spawner.Find ("spawner_8"));
		}
		if (doubleHP)
			hitpoint *= 2;
		if (spawners)
			Spawn ();
		if (doubleSpeed)
			InvokeRepeating ("Shoot", 2, shootDelay * 0.5f);
		else
			InvokeRepeating ("Shoot", 2, shootDelay);
}

	void Spawn()
	{

	}

	void Shoot()
	{
		foreach (Transform spawn in _list) 
		{
			GameObject g = Resources.Load ("Prefabs/FireBall", typeof(GameObject)) as GameObject;
			GameObject s = Instantiate(g, spawn.position, spawn.rotation) as GameObject;
			s.GetComponent<IAFireBall>().BossShoot ();
		}
	}
	
	public new void OnDamaged(int damage)
	{
		base.OnDamaged(damage);

		if (hitpoint <= 0)
		{
			hitpoint = 0;
			MenuInGame mig = FindObjectOfType<MenuInGame>();
			mig.OnGameFinished();
		}
	}
}
