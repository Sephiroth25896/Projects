using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeImage : MonoBehaviour
{
	public float Delay;
	public float Duration;
	public float From = 1.0f;
	public float To = 0.0f;

	private RawImage m_Image = null;

	public void Start()
	{
		m_Image = GetComponent<RawImage>();
		Color color = m_Image.color;
		color.a = From;
		m_Image.color = color;
		StartCoroutine(coroutine_Fade());
	}

	private IEnumerator coroutine_Fade()
	{
		yield return new WaitForSeconds(Delay);
		Color color;
		while ((To > From && m_Image.color.a < To) || (To < From && m_Image.color.a > To))
		{
			color = m_Image.color;
			if (To > From)
			{
				color.a += Time.deltaTime / Duration;
			}
			if (From > To)
			{
				color.a -= Time.deltaTime / Duration;
			}
			m_Image.color = color;
			yield return new WaitForEndOfFrame();
		}
		color = m_Image.color;
		color.a = To;
		m_Image.color = color;
		if (To == 0)
		{
			m_Image.enabled = false;
		}
		if (To == 1)
		{
			m_Image.enabled = true;
		}
	}
}
