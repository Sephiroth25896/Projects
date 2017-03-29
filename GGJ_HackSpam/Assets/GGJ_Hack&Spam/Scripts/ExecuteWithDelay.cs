using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ExecuteWithDelay : MonoBehaviour
{
	public int Delay;
	public UnityEvent DelayFinished;

	protected void Start()
	{
		StartCoroutine(coroutine_StartWithDelay());
	}

	private IEnumerator coroutine_StartWithDelay()
	{
		yield return new WaitForSeconds(Delay);

		if (DelayFinished != null) DelayFinished.Invoke();
	}
}
