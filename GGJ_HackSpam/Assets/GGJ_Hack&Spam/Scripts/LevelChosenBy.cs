using UnityEngine;
using System.Collections;

public class LevelChosenBy : MonoBehaviour {

	private TextMesh _name;

	// Use this for initialization
	void Start () {
		_name = GetComponent<TextMesh>();
		SetName(MenuSelectMaps.Instigator);
	}

	public void SetName(string name)
	{
		_name.text = "Level chosen by :\n" + name;
	}
}
