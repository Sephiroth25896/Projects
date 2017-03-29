using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterSpawner : MonoBehaviour
{
	public class IAPacket
	{
		public IA IA;
		public string Name;
	}

	public static List<IAPacket> monsters = null;
	private	MobSpawner[] spawners;

	void Awake()
	{
		spawners = this.GetComponentsInChildren<MobSpawner>();
		if (monsters != null && monsters.Count > 0)
		{
			InvokeRepeating ("Spawn", 1, 1);
		}
	}
	
	void Spawn()
	{
		if (monsters.Count > 0 && monsters != null)
		{
			IAPacket current = monsters[0];
			monsters.RemoveAt (0);
			int rand = Random.Range (0, spawners.GetLength (0));
			int i = 0;
			foreach (MobSpawner spawner in spawners)
			{
				if (i == rand)
				{
					spawner.SendMessage ("InstantiateIA", current);
					break;
				}
				i++;
			}
		}
		if (monsters.Count <= 0 || monsters == null)
		{
			CancelInvoke ();
		}
	}
}
