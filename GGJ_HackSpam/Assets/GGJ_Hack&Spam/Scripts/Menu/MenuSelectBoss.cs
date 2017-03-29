using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class MenuSelectBoss : MenuSelect
{
	public class MobKey
	{
		public string Key;
		public int RevealedCount;
		
		public string ExposedKey { get { return string.Concat(Key.Substring(0, RevealedCount), new string('?', Key.Length - RevealedCount)); } }
	}

	public GameObject BossUI = null;

	private MobKey m_CurrentKey = new MobKey();
	private MenuManager.MobPackage ExposedBoss = null;
	private Queue<int> m_PendingUIUpdates = new Queue<int>();
	private Queue<int> m_PendingReplaces = new Queue<int>();
	private int m_UpgradeLevel = 0;

	public override void StartTimer()
	{
		base.StartTimer();

		m_UpgradeLevel = 0;
		MenuManager.MobPackage bossPrefab = MenuManager.Bosses[Random.Range(0, MenuManager.Bosses.Count)];
		ExposedBoss = bossPrefab;
		GenerateKey();
		UpdateUIElement();
	}
	
	protected new void Update()
	{
		base.Update();
		
		while (m_PendingUIUpdates.Count > 0)
		{
			m_PendingUIUpdates.Dequeue();
			UpdateUIElement();
		}
		
		while (m_PendingReplaces.Count > 0)
		{
			m_PendingReplaces.Dequeue();
			GenerateKey();
			UpdateUIElement();
		}
	}
	
	protected override void HandleMessageRecievedEvent(MessageIRC message)
	{
		base.HandleMessageRecievedEvent(message);
		
		string word = message.Parameters[1];
		word = word.Substring(0, word.Length - 2);

		int matched = WordComparator.Comparator(word, m_CurrentKey.Key);
		if (matched > m_CurrentKey.RevealedCount)
		{
			m_CurrentKey.RevealedCount = matched;
			m_PendingUIUpdates.Enqueue(0);
			if (m_CurrentKey.RevealedCount == m_CurrentKey.Key.Length)
			{
				m_PendingReplaces.Enqueue(0);
				m_UpgradeLevel++;
				if (m_UpgradeLevel == 3)
				{
					m_CurrentSelectionTime = 0;
				}
			}
		}
	}

	protected override void HandleSelectionTimerOver()
	{
		base.HandleSelectionTimerOver();
		
		List<MasterSpawner.IAPacket> aiList = new List<MasterSpawner.IAPacket>();
		IABoss bossAi = ExposedBoss.Prefab.GetComponent<IABoss>();
		if (m_UpgradeLevel >= 0) bossAi.doubleHP = true;
		if (m_UpgradeLevel >= 1) bossAi.moreFireBalls = true;
		if (m_UpgradeLevel >= 2) bossAi.doubleSpeed = true;
		if (m_UpgradeLevel >= 3)
		{
			int rndIdx = Random.Range(0, MenuManager.Mobs.Count);
			aiList.Add(new MasterSpawner.IAPacket() { IA = MenuManager.Mobs[rndIdx].Prefab.GetComponent<IABoss>(), Name = "The viewers" });
		}
		aiList.Add(new MasterSpawner.IAPacket() { IA = bossAi, Name = "The viewers" });
		MasterSpawner.monsters = aiList;
		MenuSelectMaps msm = FindObjectOfType<MenuSelectMaps>();
		msm.SelectedMapName = "BossRoom";
	}

	private void GenerateKey()
	{
		m_CurrentKey.Key = WordGenerator.Generate(ExposedBoss.KeyLength);
		m_CurrentKey.RevealedCount = 0;
	}
	
	protected void UpdateUIElement()
	{
		float progress = (1.0f / 4.0f) * m_UpgradeLevel;

		BossUI.transform.FindChild("Panel/Text").GetComponent<Text>().text = ExposedBoss.Name;
		BossUI.transform.FindChild("Panel/Text_key").GetComponent<Text>().text = m_CurrentKey.ExposedKey;
		BossUI.transform.FindChild("Panel/Image").GetComponent<Image>().sprite = ExposedBoss.Thumbnail;
		BossUI.transform.FindChild("Slider").GetComponent<Slider>().value = progress;
	}
}
