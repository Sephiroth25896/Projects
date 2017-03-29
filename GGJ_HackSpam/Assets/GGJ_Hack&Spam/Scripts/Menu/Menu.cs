using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class Menu : MonoBehaviour {

	private CanvasGroup _canvas;
	private Animator _animator;

	public bool IsOpen
	{
		get { return _animator.GetBool("IsOpen");}
		set { _animator.SetBool ("IsOpen", value);}
	}

	// Use this for initialization
	protected void Awake()
	{
		_canvas = GetComponent<CanvasGroup> ();
		_animator = GetComponent<Animator> ();
		CloseMenu ();
	}

	public void CloseMenu()
	{
		if (_canvas.alpha != 0)
		{
			OnMenuClosed();
		}
		_canvas.alpha = 0;
		_canvas.blocksRaycasts = _canvas.interactable = false;
	}

	public void OpenMenu()
	{
		if (_canvas.alpha != 1)
		{
			OnMenuOpened();
		}
		_canvas.alpha = 1;
		_canvas.blocksRaycasts = _canvas.interactable = true;
	}
	
	public UnityEvent MenuOpenedEvent;
	public void OnMenuOpened()
	{
		if (MenuOpenedEvent != null) MenuOpenedEvent.Invoke();
	}
	
	public UnityEvent MenuCloseEvent;
	public void OnMenuClosed()
	{
		if (MenuCloseEvent != null) MenuCloseEvent.Invoke();
	}
}
