using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VolumeSound : MonoBehaviour
{
	private Text _text;
	private Slider _slider;

	// Use this for initialization
	void Start()
	{
		_slider = GetComponent<Slider>();
		foreach (Text  t in transform.parent.GetComponentsInChildren<Text> ())
		{
			if (t.name == "Volume")
				_text = t;
		}
		if (PlayerPrefs.HasKey("Volume"))
		{
			_slider.value = PlayerPrefs.GetFloat("Volume") * 100.0f;
			AudioListener.volume = _slider.value / 100.0f;
		}
		_text.text = _slider.value.ToString();
	}

	public void SetValue()
	{
		_text.text = _slider.value.ToString();
		AudioListener.volume = _slider.value / 100.0f;
		PlayerPrefs.SetFloat("Volume", AudioListener.volume);
		PlayerPrefs.Save();
	}
}
