using UnityEngine;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
	public class LevelPackage
	{
		public string Name;
		public float Weight;
		public Sprite Thumbnail;
	}

	public class MobPackage
	{
		public string Name;
		public GameObject Prefab;
		public Sprite Thumbnail;
		public int KeyLength;
	}
	
	public static List<LevelPackage> Levels = new List<LevelPackage>();
	public static List<MobPackage> Mobs = new List<MobPackage>();
	public static List<MobPackage> Bosses = new List<MobPackage>();

	public Menu _currentMenu;

	protected void Awake()
	{
		Levels = new List<LevelPackage>() {
			new LevelPackage() { Name = "Level1", Weight = 1.0f, Thumbnail = Resources.Load("Sprites/LevelThumbnails/Level1") as Sprite },
			new LevelPackage() { Name = "Level2", Weight = 1.0f, Thumbnail = Resources.Load("Sprites/LevelThumbnails/Level2") as Sprite },
			new LevelPackage() { Name = "Level3", Weight = 1.0f, Thumbnail = Resources.Load("Sprites/LevelThumbnails/Level3") as Sprite },
			new LevelPackage() { Name = "Level4", Weight = 1.0f, Thumbnail = Resources.Load("Sprites/LevelThumbnails/Level4") as Sprite },
			new LevelPackage() { Name = "Level5", Weight = 1.0f, Thumbnail = Resources.Load("Sprites/LevelThumbnails/Level5") as Sprite },
			new LevelPackage() { Name = "Level6", Weight = 1.0f, Thumbnail = Resources.Load("Sprites/LevelThumbnails/Level6") as Sprite },
			new LevelPackage() { Name = "Level7", Weight = 1.0f, Thumbnail = Resources.Load("Sprites/LevelThumbnails/Level7") as Sprite },
			new LevelPackage() { Name = "Level8", Weight = 1.0f, Thumbnail = Resources.Load("Sprites/LevelThumbnails/Level8") as Sprite },
		};

		Mobs = new List<MobPackage>() {
			new MobPackage() { Name = "Bat", KeyLength = 4, Prefab = Resources.Load("Prefabs/Monsters/Bat") as GameObject, Thumbnail = Resources.Load("Sprites/Batman") as Sprite },
			new MobPackage() { Name = "Gargoyle", KeyLength = 5, Prefab = Resources.Load("Prefabs/Monsters/Gargoyle") as GameObject, Thumbnail = Resources.Load("Sprites/Gargoyle3") as Sprite },
			new MobPackage() { Name = "SlimeBlue", KeyLength = 3, Prefab = Resources.Load("Prefabs/Monsters/SlimeBlue") as GameObject, Thumbnail = Resources.Load("Sprites/SlimeBlue") as Sprite },
			new MobPackage() { Name = "SlimeGreen", KeyLength = 3, Prefab = Resources.Load("Prefabs/Monsters/SlimeGreen") as GameObject, Thumbnail = Resources.Load("Sprites/SlimeGreen") as Sprite },
			new MobPackage() { Name = "SlimeRed", KeyLength = 3, Prefab = Resources.Load("Prefabs/Monsters/SlimeRed") as GameObject, Thumbnail = Resources.Load("Sprites/SlimeRed") as Sprite },
			new MobPackage() { Name = "Wizard", KeyLength = 4, Prefab = Resources.Load("Prefabs/Monsters/Wizard") as GameObject, Thumbnail = Resources.Load("Sprites/Wizard") as Sprite },
			new MobPackage() { Name = "Zombie0", KeyLength = 4, Prefab = Resources.Load("Prefabs/Monsters/ZombieFemale") as GameObject, Thumbnail = Resources.Load("Sprites/Zombie2") as Sprite },
			new MobPackage() { Name = "Zombie1", KeyLength = 4, Prefab = Resources.Load("Prefabs/Monsters/ZombieMale") as GameObject, Thumbnail = Resources.Load("Sprites/Zombie1") as Sprite },
		};

		Bosses = new List<MobPackage>() {
			new MobPackage() { Name = "Demon", KeyLength = 10, Prefab = Resources.Load("Prefabs/Monsters/BossDemon") as GameObject, Thumbnail = Resources.Load("Sprites/demon_boss1") as Sprite },
			new MobPackage() { Name = "Cthulhu", KeyLength = 10, Prefab = Resources.Load("Prefabs/Monsters/BossCthulhu") as GameObject, Thumbnail = Resources.Load("Sprites/CthulhuBoss") as Sprite },
		};
	}
	
	// Use this for initialization
	protected void Start()
	{
		ShowMenu(_currentMenu);
	}
	
	public void ShowMenu(Menu menu)
	{
		if (_currentMenu != null)
		{
			_currentMenu.CloseMenu();
		}
		_currentMenu = menu;
		if (_currentMenu != null)
		{
			_currentMenu.OpenMenu();
		}
	}
	
	public void LaunchTutorialLevel()
	{
		Application.LoadLevel("TrainingRoom");
	}
	
	public void LaunchMainMenu()
	{
		Application.LoadLevel("MainMenu");
	}
	
	public void LaunchEndGameMenu()
	{
		Application.LoadLevel("EndGame");
	}
	
	public void Quit()
	{
		Application.Quit();
	}
}
