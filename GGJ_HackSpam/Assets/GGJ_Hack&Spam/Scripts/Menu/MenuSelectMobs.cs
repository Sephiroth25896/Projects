using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class MenuSelectMobs : MenuSelect
{
	public class MobKey
	{
		public string Key;
		public int RevealedCount;

		public string ExposedKey { get { return string.Concat(Key.Substring(0, RevealedCount), new string('?', Key.Length - RevealedCount)); } }
	}

	public List<GameObject> MobsUI = new List<GameObject>();
	public MenuManager.MobPackage[] ExposedMobs = null;
	public MobKey[] Keys = null;
	
	private Queue<int> m_PendingUIUpdates = new Queue<int>();
	private Queue<int> m_PendingReplaces = new Queue<int>();
	private List<MenuManager.MobPackage> m_BufferedMobs = new List<MenuManager.MobPackage>();
	private List<string> m_BufferedMobsName = new List<string>();

	public override void StartTimer()
	{
		base.StartTimer();

		ExposedMobs = new MenuManager.MobPackage[MobsUI.Count];
		Keys = new MobKey[MobsUI.Count];
		m_BufferedMobs.Clear();
		m_BufferedMobsName.Clear();
		for (int i = 0; i < MobsUI.Count; i++)
		{
			PickRandomMob(i);
		}
	}

	protected new void Update()
	{
		base.Update();
		
		while (m_PendingUIUpdates.Count > 0)
		{
			int idx = m_PendingUIUpdates.Dequeue();
			UpdateUIElement(idx);
		}

		while (m_PendingReplaces.Count > 0)
		{
			int idx = m_PendingReplaces.Dequeue();
			PickRandomMob(idx);
		}
	}
	
	protected override void HandleMessageRecievedEvent(MessageIRC message)
	{
		base.HandleMessageRecievedEvent(message);

		string word = message.Parameters[1];
		word = word.Substring(0, word.Length - 2);

		for (int i = 0; i < Keys.Length; i++)
		{
			int matched = WordComparator.Comparator(word, Keys[i].Key);
			if (matched > Keys[i].RevealedCount)
			{
				Keys[i].RevealedCount = matched;
				m_PendingUIUpdates.Enqueue(i);
				if (Keys[i].RevealedCount == Keys[i].Key.Length)
				{
					m_BufferedMobs.Add(ExposedMobs[i]);
					m_BufferedMobs.Add(ExposedMobs[i]);
					m_BufferedMobs.Add(ExposedMobs[i]);
					m_BufferedMobs.Add(ExposedMobs[i]);
					m_BufferedMobsName.Add(message.Parameters[0]);
					m_BufferedMobsName.Add(message.Parameters[0]);
					m_BufferedMobsName.Add(message.Parameters[0]);
					m_BufferedMobsName.Add(message.Parameters[0]);
					m_PendingReplaces.Enqueue(i);
				}
			}
		}
	}
	
	protected override void HandleSelectionTimerOver()
	{
		base.HandleSelectionTimerOver();

		List<MasterSpawner.IAPacket> aiList = new List<MasterSpawner.IAPacket>();
		for (int i = 0; i < m_BufferedMobs.Count; i++)
		{
			IA ai = m_BufferedMobs[i].Prefab.GetComponent<IA>();
			string name = m_BufferedMobsName[i];
			aiList.Add(new MasterSpawner.IAPacket() { IA = ai, Name = name });
		}
		MasterSpawner.monsters = aiList;
	}

	protected void PickRandomMob(int index)
	{
		MenuManager.MobPackage mobPrefab = MenuManager.Mobs[Random.Range(0, MenuManager.Mobs.Count)];
		ExposedMobs[index] = mobPrefab;
		Keys[index] = new MobKey() { Key = WordGenerator.Generate(mobPrefab.KeyLength), RevealedCount = 0 };
		UpdateUIElement(index);
	}

	protected void UpdateUIElement(int index)
	{
		MobsUI[index].transform.FindChild("Text").GetComponent<Text>().text = ExposedMobs[index].Name;
		MobsUI[index].transform.FindChild("Text_key").GetComponent<Text>().text = Keys[index].ExposedKey;
		MobsUI[index].transform.FindChild("Image").GetComponent<Image>().sprite = ExposedMobs[index].Thumbnail;
	}
}
